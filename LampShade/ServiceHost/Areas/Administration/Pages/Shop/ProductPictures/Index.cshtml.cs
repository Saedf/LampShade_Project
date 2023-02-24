using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using ShopManagement.Application.Contract.Product;
using ShopManagement.Application.Contract.ProductCategory;
using ShopManagement.Application.Contract.ProductPicture;

namespace ServiceHost.Areas.Administration.Pages.Shop.ProductPictures
{
    public class IndexModel : PageModel
    {
        [TempData]
        public string Message { get; set; }

        private readonly IProductPictureApplication _pictureApplication;
        private readonly IProductApplication _productApplication;
        public List<ProductPictureViewModel> _ProductPictureViewModels;
        public ProductPictureSearchModel SearchModel;

        public SelectList products;

        public IndexModel(IProductPictureApplication pictureApplication, IProductApplication productApplication)
        {
            _pictureApplication = pictureApplication;
            _productApplication = productApplication;
        }


        public void OnGet(ProductPictureSearchModel searchModel)
        {
           _ProductPictureViewModels=_pictureApplication.Search(searchModel);
           products = new SelectList(_productApplication.GetProducts(),"Id","Name");
        }
        public PartialViewResult OnGetCreate()
        {
            var command = new CreateProductPicture
            { 
                Products= _productApplication.GetProducts(),
            };
           return Partial("./Create",command);
        }

      
        public JsonResult OnPostCreate(CreateProductPicture command)
        {
            var result = _pictureApplication.Create(command);
            return new JsonResult(result);
        }
        public PartialViewResult OnGetEdit(long id)
        {
            var productPicture = _pictureApplication.GetDetails(id);
            productPicture.Products = _productApplication.GetProducts();
            return Partial("Edit", productPicture);
        }
        public JsonResult OnPostEdit(EditProductPicture command)
        {
            var result = _pictureApplication.Edit(command);
            return new JsonResult(result);
        }
        public IActionResult OnGetRemove(long id)
        {
            var result = _pictureApplication.Remove(id);
            if (result.IsSucceeded)
            {
                return RedirectToPage("./Index");
            }
            Message=result.Message;
            return RedirectToPage("./Index");

        }
        public IActionResult OnGetRestore(long id)
        {
            var result = _pictureApplication.Restore(id);
            if (result.IsSucceeded)
            {
                return RedirectToPage("./Index");
            }
            Message = result.Message;
            return RedirectToPage("./Index");

        }
    }
}
