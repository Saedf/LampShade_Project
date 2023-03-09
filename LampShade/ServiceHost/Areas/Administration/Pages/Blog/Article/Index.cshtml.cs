using BlogManagement.Application.Contracts.Article;
using BlogManagement.Application.Contracts.ArticleCategory;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using ShopManagement.Application.Contract.ProductCategory;

namespace ServiceHost.Areas.Administration.Pages.Blog.Article
{
    public class IndexModel : PageModel
    {
        private readonly IArticleCategoryApplication _articleCategoryApplication;
        private readonly IArticleApplication _articleApplication;
        public List<ArticleViewModel> Articles ;
        public ArticleSearchModel SearchModel;

        public SelectList ArticleCateogris;
        public IndexModel(IArticleCategoryApplication articleCategoryApplication, IArticleApplication articleApplication)
        {
            _articleCategoryApplication = articleCategoryApplication;
            _articleApplication = articleApplication;
        }

        public void OnGet(ArticleSearchModel searchModel)
        {
            ArticleCateogris = new SelectList(_articleCategoryApplication.GetArticleCategories(),
               "Id","Name");
           Articles=_articleApplication.Search(searchModel);
        }
        //public PartialViewResult OnGetCreate()
        //{
        //   return Partial("/Create",new CreateArticle());
        //}

      
     

    }
}
