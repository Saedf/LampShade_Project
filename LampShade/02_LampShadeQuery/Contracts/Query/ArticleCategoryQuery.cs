using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using _02_LampShadeQuery.Contracts.ArticleCategory;
using BlogManagement.Infrastructure.EfCore;
using Microsoft.EntityFrameworkCore;

namespace _02_LampShadeQuery.Contracts.Query
{
    public class ArticleCategoryQuery:IArticleCategoryQuery
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
    }
}
