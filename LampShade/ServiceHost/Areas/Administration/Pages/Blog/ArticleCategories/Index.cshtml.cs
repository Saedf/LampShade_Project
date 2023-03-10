using BlogManagement.Application.Contracts.ArticleCategory;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using ShopManagement.Application.Contract.ProductCategory;

namespace ServiceHost.Areas.Administration.Pages.Blog.ArticleCategories
{
    public class IndexModel : PageModel
    {
        private readonly IArticleCategoryApplication _articleCategoryApplication;
        public List<ArticleCategoryViewModel> _articleCategoryViewModels;
        public ArticleCategorySearchModel SearchModel;

        public IndexModel(IArticleCategoryApplication articleCategoryApplication)
        {
            _articleCategoryApplication = articleCategoryApplication;
        }

        public void OnGet(ArticleCategorySearchModel searchModel)
        {
           _articleCategoryViewModels=_articleCategoryApplication.Search(searchModel);
        }
        public PartialViewResult OnGetCreate()
        {
           return Partial("./Create",new CreateArticleCategory());
        }

      
        public JsonResult OnPostCreate(CreateArticleCategory command)
        {
            var result = _articleCategoryApplication.Create(command);
            return new JsonResult(result);
        }
        public PartialViewResult OnGetEdit(long id)
        {
            var articleCategory = _articleCategoryApplication.GetDetails(id);
            return Partial("Edit", articleCategory);
        }
        public JsonResult OnPostEdit(EditArticleCategory command)
        {
            
            var result = _articleCategoryApplication.Edit(command);
            return new JsonResult(result);
        }

    }
}
