using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using _01_Framework.Application;
using _02_LampShadeQuery.Contracts.Article;
using _02_LampShadeQuery.Contracts.ArticleCategory;
using BlogManagement.Infrastructure.EfCore;
using Microsoft.EntityFrameworkCore;

namespace _02_LampShadeQuery.Query
{
    public class ArticleCategoryQuery : IArticleCategoryQuery
    {
        private readonly BlogContext _blogContext;

        public ArticleCategoryQuery(BlogContext blogContext)
        {
            _blogContext = blogContext;
        }

        public List<ArticleCategoryQueryModel> GetArticleCategories()
        {
            return _blogContext.ArticleCategories.Include(x => x.Articles)
                .Select(x => new ArticleCategoryQueryModel
                {
                    Name = x.Name,
                    Picture = x.Picture,
                    ArticleCount = x.Articles.Count,
                    PictureTitle = x.PictureTitle,
                    PictureAlt = x.PictureAlt,
                    Slug = x.Slug,



                }).ToList();
        }

        public ArticleCategoryQueryModel GetArticleCategory(string slug)
        {
            var articleCategory = _blogContext.ArticleCategories.Include(x => x.Articles)
                .Select(x => new ArticleCategoryQueryModel
                {
                    Slug = x.Slug,
                    Name = x.Name,
                    Description = x.Description,
                    Picture = x.Picture,
                    PictureAlt = x.PictureAlt,
                    PictureTitle = x.PictureTitle,
                    Keywords = x.KeyWords,
                    MetaDecription = x.MetaDescription,
                    CanonicalAddress = x.CanonicalAddress,
                    ArticleCount = x.Articles.Count,
                    ArticleQueries = MapArticles(x.Articles)
                }).FirstOrDefault(x => x.Slug == slug);
            if (!string.IsNullOrWhiteSpace(articleCategory.Keywords))
            {
                articleCategory.KeywordList = articleCategory.Keywords.Split(",").ToList();
            }

            return articleCategory;
        }

        private static List<ArticleQueryModel> MapArticles(List<BlogManagement.Domain.ArticleAgg.Article> articles)
        {
            return articles.Select(x => new ArticleQueryModel
            {
                Slug = x.Slug,
                Picture = x.Picture,
                PictureAlt = x.PictureAlt,
                PictureTitle = x.PictureTitle,
                ShortDescription = x.ShortDescription,
                PublishDate = x.PublishDate.ToFarsi()

            }).ToList();
        }
    }
}
