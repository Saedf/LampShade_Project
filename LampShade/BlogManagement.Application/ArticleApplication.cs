using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using _01_Framework.Application;
using BlogManagement.Application.Contracts.Article;
using BlogManagement.Application.Contracts.ArticleCategory;
using BlogManagement.Domain.ArticleAgg;
using BlogManagement.Domain.ArticleCategoryAgg;

namespace BlogManagement.Application
{
    public class ArticleApplication:IArticleApplication
    {
        private readonly IArticleRepository _articleRepository;
        private readonly IArticleCategoryRepository _articleCategoryRepository;
        private readonly IFileUploader _fileUploader;

        public ArticleApplication(IArticleRepository articleRepository, IFileUploader fileUploader, IArticleCategoryRepository articleCategoryRepository)
        {
            _articleRepository = articleRepository;
            _fileUploader = fileUploader;
            _articleCategoryRepository = articleCategoryRepository;
        }

        public OperationResult Create(CreateArticle command)
        {
            var operation=new OperationResult();
            if (_articleRepository.Exists(x=>x.Title==command.Title))
            {
                return operation.Faild(ApplicationMessage.DuplicatedRecord);
            }

            var categorySlug = _articleCategoryRepository.GetSlugBy(command.CategoryId);
            var path = $"{categorySlug}//{command.Slug}";
            var pictureName = _fileUploader.Upload(command.Picture, path);
            var publishDate = command.PublishDate.ToGeorgianDateTime();

            var article = new Article(command.Title,
                command.ShortDescription, pictureName,
                command.PictureAlt, command.PictureTitle,
                publishDate, command.Slug, command.KeyWords,
                command.MetaDescription, command.CanonicalAddress, command.CategoryId
                , command.Description);
            _articleRepository.Create(article);
            _articleRepository.SaveChanges();
            return operation.Succeeded();
        }

        public OperationResult Edit(EditArticle command)
        {
            var operation = new OperationResult();
            var article = _articleRepository.GetBy(command.Id);
            if (article==null)
            {
                return operation.Faild(ApplicationMessage.RecordNotFound);
            }

            if (_articleRepository.Exists(x => x.Title == command.Title && x.Id!=command.Id))
            {
                return operation.Faild(ApplicationMessage.DuplicatedRecord);
            }

            var categorySlug = _articleCategoryRepository.GetSlugBy(command.CategoryId);
            var path = $"{categorySlug}//{command.Slug}";
            var pictureName = _fileUploader.Upload(command.Picture, path);
            var publishDate = command.PublishDate.ToGeorgianDateTime();

            article.Edit(command.Title,
                command.ShortDescription, pictureName,
                command.PictureAlt, command.PictureTitle,
                publishDate, command.Slug, command.KeyWords,
                command.MetaDescription, command.CanonicalAddress, command.CategoryId
                , command.Description);

            _articleRepository.SaveChanges();
            return operation.Succeeded();
        }

        public List<ArticleViewModel> Search(ArticleSearchModel command)
        {
            return _articleRepository.Search(command);

        }

        public EditArticle GetDetails(long id)
        {
            return _articleRepository.GetDetailsBy(id);
        }
    }
}
