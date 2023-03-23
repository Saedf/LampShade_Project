using _01_Framework.Infrastructure;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using ShopManagement.Application.Contract.ProductCategory;
using ShopManagement.Infrastructure.Core.Permissions;

namespace ServiceHost.Areas.Administration.Pages.Shop.ProductCategories
{
    //[Authorize(Roles = Roles.Administrator)]
    public class IndexModel : PageModel
    {
        private readonly IProductCategoryApplication _productCategoryApplication;
        public List<ViewModelProductCategory> _productCategories;
        public SearchModelProductCategory SearchModel;
        public IndexModel(IProductCategoryApplication productCategoryApplication)
        {
            _productCategoryApplication = productCategoryApplication;
            //_productCategories=new List<ViewModelProductCategory>();
        }
        [NeedsPermission(ShopPermissions.ListProductCategories)]
        public void OnGet(SearchModelProductCategory searchModel)
        {
           _productCategories=_productCategoryApplication.Search(searchModel);
        }
        public PartialViewResult OnGetCreate()
        {
           return Partial("./Create",new CreateProductCategory());
        }

        [NeedsPermission(ShopPermissions.CreateProductCategories)]
        public JsonResult OnPostCreate(CreateProductCategory commandCategory)
        {
            var result = _productCategoryApplication.Create(commandCategory);
            return new JsonResult(result);
        }
        public PartialViewResult OnGetEdit(long id)
        {
            var productCategory = _productCategoryApplication.GetDetailsBy(id);
            return Partial("Edit", productCategory);
        }
        [NeedsPermission(ShopPermissions.EditProductCategories)]
        public JsonResult OnPostEdit(EditProductCategory commandCategory)
        {
            if (ModelState.IsValid)
            {
                
            }
            var result = _productCategoryApplication.Edit(commandCategory);
            return new JsonResult(result);
        }

    }
}
