using _02_LampShadeQuery.Contracts.Article;
using _02_LampShadeQuery.Contracts.ArticleCategory;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace ServiceHost.Pages
{
    public class ArticleModel : PageModel
    {
        private readonly IArticleQuery _articleQuery;
        private  readonly IArticleCategoryQuery _categoryQuery;
        public ArticleQueryModel ArticleQueryModel;
        public List<ArticleQueryModel> LatestArticles;
        public List<ArticleCategoryQueryModel> ArticleCategoryQueryModels;
        public ArticleModel(IArticleQuery articleQuery, IArticleCategoryQuery categoryQuery)
        {
            _articleQuery = articleQuery;
            _categoryQuery = categoryQuery;
        }

        public void OnGet(string id)
        {
            ArticleQueryModel = _articleQuery.GetArticleDetails(id);
            LatestArticles = _articleQuery.LatestArticles();
            ArticleCategoryQueryModels = _categoryQuery.GetArticleCategories();
        }
    }
}
