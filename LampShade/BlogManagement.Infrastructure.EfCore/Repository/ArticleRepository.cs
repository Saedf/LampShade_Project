using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using _01_Framework.Application;
using _01_Framework.Infrastructure;
using BlogManagement.Application.Contracts.Article;
using BlogManagement.Domain.ArticleAgg;
using Microsoft.EntityFrameworkCore;

namespace BlogManagement.Infrastructure.EfCore.Repository
{
    public class ArticleRepository:RepositoryBase<long,Article>,IArticleRepository
    {
        private readonly BlogContext _blogContext;

        public ArticleRepository(BlogContext blogContext) : base(blogContext)
        {
            _blogContext = blogContext;
        }

        public EditArticle GetDetailsBy(long id)
        {
            var query = _blogContext.Articles
                .Select(x => new EditArticle
                {
                    CanonicalAddress = x.CanonicalAddress,
                    CategoryId = x.CategoryId,
                    Description = x.Description,
                    Id = x.Id,
                    KeyWords = x.KeyWords,
                    MetaDescription = x.MetaDescription,
                    PictureAlt = x.PictureAlt,
                    PictureTitle = x.PictureTitle,
                    Slug = x.Slug,
                    ShortDescription = x.ShortDescription,
                    Title = x.Title,
                    PublishDate = x.PublishDate.ToFarsi(),
                });
            return query.FirstOrDefault(x => x.Id == id); 
        }

        public List<ArticleViewModel> Search(ArticleSearchModel searchModel)
        {
            var query = _blogContext.Articles.Select(x => new ArticleViewModel
            {
                PublishDate = x.PublishDate.ToFarsi(),
                Title = x.Title,
                Category = x.Category.Name,
                Picture = x.Picture,
                Id = x.Id,
                ShortDescription = x.ShortDescription
                    .Substring(0, Math.Min(x.ShortDescription.Length, 50)) + "...",
                CategoryId = x.CategoryId
            });
            if (!string.IsNullOrWhiteSpace(searchModel.Title))
            {
                query = query.Where(x => x.Title.Contains(searchModel.Title));
            }

            if (searchModel.CategoryId>0)
            {
                query = query.Where(x => x.CategoryId == searchModel.CategoryId );
            }

            return query.OrderByDescending(x => x.Id).ToList();
        }

        public Article GetArticleWithCategoryBy(long id)
        {
            return _blogContext.Articles.Include(x => x.Category)
                .FirstOrDefault(x => x.Id == id);
        }
    }
}
