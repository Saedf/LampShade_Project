using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using _01_Framework.Application;
using _01_Framework.Infrastructure;
using BlogManagement.Application.Contracts.ArticleCategory;
using BlogManagement.Domain.ArticleCategoryAgg;
using Microsoft.EntityFrameworkCore;

namespace BlogManagement.Infrastructure.EfCore.Repository
{
    public class ArticleCategoryRepository : RepositoryBase<long,ArticleCategory>, IArticleCategoryRepository
    {

        private readonly BlogContext _blogContext;

        public ArticleCategoryRepository(BlogContext blogContext) : base(blogContext)
        {
            _blogContext = blogContext;
        }

        public List<ArticleCategoryViewModel> GetArticleCategories()
        {
            var query = _blogContext.ArticleCategories
                .Select(x=>new ArticleCategoryViewModel
                {
                    Id = x.Id,
                    Name = x.Name,
                    //Picture = x.Picture,
                    //CreationDate = x.CreationDate.ToFarsi(),
                    //Description = x.Description,
                    //ShowOrder = x.ShowOrder,

                }).OrderByDescending(x=>x.Id).ToList();
            return query;
        }

        public string GetSlugBy(long id)
        {
            var query = _blogContext.ArticleCategories
                .Select(x =>new {x.Id,x.Slug} )
                .FirstOrDefault(x=>x.Id==id).Slug;
            return query;
        }

        public EditArticleCategory GetDetails(long id)
        {
            var query = _blogContext.ArticleCategories
                .Where(x=>x.Id==id)
                .Select(x => new EditArticleCategory
                {
                    Id = x.Id,
                    Name = x.Name,
                    Description = x.Description,
                    ShowOrder = x.ShowOrder,
                    CanonicalAddress = x.CanonicalAddress,
                    KeyWords = x.KeyWords,
                    MetaDescription = x.MetaDescription,
                    PictureAlt = x.PictureAlt,
                    PictureTitle = x.PictureTitle,
                    Slug = x.Slug

                }).FirstOrDefault();
            return query;
        }

        public List<ArticleCategoryViewModel> Search(ArticleCategorySearchModel searchModel)
        {
            var query = _blogContext.ArticleCategories
                .Select(x => new ArticleCategoryViewModel
                {
                    Id = x.Id,
                    Name = x.Name,
                    Description = x.Description,
                    CreationDate = x.CreationDate.ToFarsi(),
                    Picture = x.Picture,
                    ShowOrder = x.ShowOrder,

                });
            if (!string.IsNullOrWhiteSpace(searchModel.Name))
            {
                query = query.Where(x => x.Name.Contains(searchModel.Name));
            }
            return query.OrderByDescending(x=>x.Id).ToList();
        }
    }
}
