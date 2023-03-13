using _02_LampShadeQuery.Contracts.Article;
using _02_LampShadeQuery.Contracts.ArticleCategory;
using CommentManagement.Application.Contracts.Comment;
using CommentManagement.Infrastructure.EfCore;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace ServiceHost.Pages
{
    public class ArticleModel : PageModel
    {
        private readonly IArticleQuery _articleQuery;
        private  readonly IArticleCategoryQuery _categoryQuery;
        private readonly ICommentApplication _commentApplication;
        public ArticleQueryModel ArticleQueryModel;
        public List<ArticleQueryModel> LatestArticles;
        public List<ArticleCategoryQueryModel> ArticleCategoryQueryModels;

        public ArticleModel(IArticleQuery articleQuery, IArticleCategoryQuery categoryQuery, ICommentApplication commentApplication)
        {
            _articleQuery = articleQuery;
            _categoryQuery = categoryQuery;
            _commentApplication = commentApplication;
        }

        public void OnGet(string id)
        {
            ArticleQueryModel = _articleQuery.GetArticleDetails(id);
            LatestArticles = _articleQuery.LatestArticles();
            ArticleCategoryQueryModels = _categoryQuery.GetArticleCategories();
        }

        public IActionResult OnPost(AddComment command, string articleSlug)
        {
            command.Type=CommentType.Article;
            var result = _commentApplication.Add(command);
            return RedirectToPage("/Article", new { Id = articleSlug });
        }
    }
}
