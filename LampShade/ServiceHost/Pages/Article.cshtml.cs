using _02_LampShadeQuery.Contracts.Article;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace ServiceHost.Pages
{
    public class ArticleModel : PageModel
    {
        private readonly IArticleQuery _articleQuery;
        public ArticleQueryModel ArticleQueryModel;
        public List<ArticleQueryModel> LatestArticles;
        public ArticleModel(IArticleQuery articleQuery)
        {
            _articleQuery = articleQuery;
        }

        public void OnGet(string id)
        {
            ArticleQueryModel = _articleQuery.GetArticleDetails(id);
            LatestArticles = _articleQuery.LatestArticles();
        }
    }
}
