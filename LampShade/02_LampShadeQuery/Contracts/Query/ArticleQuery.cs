using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using _01_Framework.Application;
using _02_LampShadeQuery.Contracts.Article;
using BlogManagement.Infrastructure.EfCore;
using Microsoft.EntityFrameworkCore;

namespace _02_LampShadeQuery.Contracts.Query
{
    public class ArticleQuery:IArticleQuery
    {
        private readonly BlogContext _blogContext;

        public ArticleQuery(BlogContext blogContext)
        {
            _blogContext = blogContext;
        }

        public List<ArticleQueryModel> LatestArticles()
        {
            return _blogContext.Articles
                .Include(x => x.Category)
                .Where(x => x.PublishDate <= DateTime.Now)
                .Select(x=>new ArticleQueryModel
                {
                    Title = x.Title,
                    Slug = x.Slug,
                    Picture = x.Picture,
                    PictureAlt = x.PictureAlt,
                    PictureTitle = x.PictureTitle,
                    ShortDescription = x.ShortDescription,
                    PublishDate = x.PublishDate.ToFarsi(),
                    MetaDescription = x.MetaDescription
                })
                .ToList();
        }

        public ArticleQueryModel GetArticleDetails(string slug)
        {
            var article=_blogContext.Articles
                .Include(x => x.Category)
                .Where(x => x.PublishDate <= DateTime.Now)
                .Select(x => new ArticleQueryModel
                {
                    Id = x.Id,
                    Title = x.Title,
                    Slug = x.Slug,
                    Picture = x.Picture,
                    PictureAlt = x.PictureAlt,
                    PictureTitle = x.PictureTitle,
                    PublishDate = x.PublishDate.ToFarsi(),
                    MetaDescription = x.MetaDescription,
                    CategoryName = x.Category.Name,
                    CategorySlug = x.Category.Slug,
                    CanonicalAddress = x.CanonicalAddress,
                    KeyWords = x.KeyWords,
                    ShortDescription = x.ShortDescription,
                    Description = x.Description

                }).FirstOrDefault(x => x.Slug == slug);
            if (!string.IsNullOrWhiteSpace(article.KeyWords))
            {
                article.KeyWordList = article.KeyWords.Split(",").ToList();
            }

            return article;
        }

       
    }
}
