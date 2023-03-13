using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using _01_Framework.Application;
using _02_LampShadeQuery.Contracts.Article;
using _02_LampShadeQuery.Contracts.Comment;
using BlogManagement.Infrastructure.EfCore;
using CommentManagement.Infrastructure.EfCore;
using Microsoft.EntityFrameworkCore;

namespace _02_LampShadeQuery.Contracts.Query
{
    public class ArticleQuery:IArticleQuery
    {
        private readonly BlogContext _blogContext;
        private readonly CommentContext _commentContext;

        public ArticleQuery(BlogContext blogContext, CommentContext commentContext)
        {
            _blogContext = blogContext;
            _commentContext = commentContext;
        }

        public List<Article.ArticleQueryModel> LatestArticles()
        {
            return _blogContext.Articles
                .Include(x => x.Category)
                .Where(x => x.PublishDate <= DateTime.Now)
                .Select(x=>new Article.ArticleQueryModel
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

        public Article.ArticleQueryModel GetArticleDetails(string slug)
        {
            var article=_blogContext.Articles
                .Include(x => x.Category)
                .Where(x => x.PublishDate <= DateTime.Now)
                .Select(x => new Article.ArticleQueryModel
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

            var comments = _commentContext.Comments
                .Where(x => !x.IsCanceled)
                .Where(x => x.IsConfirmed)
                .Where(x => x.Type == CommentType.Article)
                .Where(x => x.OwnerRecordId == article.Id)
                .Select(x => new CommentQueryModel
                {
                    Id = x.Id,
                    Message = x.Message,
                    Name = x.Name,
                    ParentId = x.ParentId,
                    CreationDate = x.CreationDate.ToFarsi(),
                }).OrderByDescending(x => x.Id).ToList();
            foreach (var comment in comments)
            {
                if (comment.ParentId>0)
                {
                    comment.parentName = comments.FirstOrDefault(x => x.Id == comment.ParentId)
                        ?.Name;
                }
            }

            article.Comments = comments;
            return article;
        }

       
    }
}
