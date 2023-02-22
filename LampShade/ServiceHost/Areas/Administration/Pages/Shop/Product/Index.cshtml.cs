using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using ShopManagement.Application.Contract.Product;
using ShopManagement.Application.Contract.ProductCategory;

namespace ServiceHost.Areas.Administration.Pages.Shop.Product
{
    public class IndexModel : PageModel
    {
        [TempData]
        public string Message { get; set; }

        private readonly IProductApplication _productApplication;
        private readonly IProductCategoryApplication _categoryApplication;
        public List<ProductViewModel> _productViewModels;
        public ProductSearchModel SearchModel;

        public SelectList productCategories;
        public IndexModel(IProductApplication productApplication, IProductCategoryApplication categoryApplication)
        {
            _productApplication = productApplication;
            _categoryApplication = categoryApplication;
        }


        public void OnGet(ProductSearchModel searchModel)
        {
           _productViewModels=_productApplication.Search(searchModel);
           productCategories = new SelectList(_categoryApplication.getProductCategories(),"Id","Name");
        }
        public PartialViewResult OnGetCreate()
        {
            var command = new CreateProduct
            {
                ProductCategories = _categoryApplication.getProductCategories()
            };
           return Partial("./Create",command);
        }

      
        public JsonResult OnPostCreate(CreateProduct command)
        {
            var result = _productApplication.Create(command);
            return new JsonResult(result);
        }
        public PartialViewResult OnGetEdit(long id)
        {
            var product = _productApplication.GetDetails(id);
            product.ProductCategories = _categoryApplication.getProductCategories();
            return Partial("Edit", product);
        }
        public JsonResult OnPostEdit(EditProduct command)
        {
            var result = _productApplication.Edit(command);
            return new JsonResult(result);
        }
        public IActionResult OnGetNotInStock(long id)
        {
            var result=_productApplication.NotInStock(id);
            if (result.IsSucceeded)
            {
                return RedirectToPage("./Index");
            }
            Message=result.Message;
            return RedirectToPage("./Index");

        }
        public IActionResult OnGetIsInStock(long id)
        {
            var result = _productApplication.IsInStock(id);
            if (result.IsSucceeded)
            {
                return RedirectToPage("./Index");
            }
            Message = result.Message;
            return RedirectToPage("./Index");

        }
    }
}
