using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using ShopManagement.Application.Contract.ProductCategory;

namespace ServiceHost.Areas.Administration.Pages.Shop.ProductCategories
{
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

        public void OnGet(SearchModelProductCategory searchModel)
        {
           _productCategories=_productCategoryApplication.Search(searchModel);
        }
        public PartialViewResult OnGetCreate()
        {
           return Partial("./Create",new CreateProductCategory());
        }

      
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
