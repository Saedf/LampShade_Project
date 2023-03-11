using _02_LampShadeQuery.Contracts.Article;
using _02_LampShadeQuery.Contracts.ArticleCategory;
using _02_LampShadeQuery.Contracts.Query;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace ServiceHost.Pages
{
    public class ArticleCategoryModel : PageModel
    {
        public ArticleCategoryQueryModel ArticleCategory;
        public List<ArticleCategoryQueryModel> ArticleCategories;
        public List<ArticleQueryModel> LatestArticles;

        private readonly IArticleCategoryQuery _categoryQuery;
        private readonly IArticleQuery _articleQuery;

        public ArticleCategoryModel(IArticleCategoryQuery categoryQuery, IArticleQuery articleQuery)
        {
            _categoryQuery = categoryQuery;
            _articleQuery = articleQuery;
        }

        public void OnGet(string id)
        {
            LatestArticles = _articleQuery.LatestArticles();
            ArticleCategory = _categoryQuery.GetArticleCategory(id);
            ArticleCategories = _categoryQuery.GetArticleCategories();
        }
    }
}
