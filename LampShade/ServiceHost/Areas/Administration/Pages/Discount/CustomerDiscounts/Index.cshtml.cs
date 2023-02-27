using DiscountManagement.Application.Contract.CustomerDiscount;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using ShopManagement.Application.Contract.Product;
using ShopManagement.Application.Contract.ProductCategory;

namespace ServiceHost.Areas.Administration.Pages.Discount.CustomerDiscounts
{
    public class IndexModel : PageModel
    {
        [TempData]
        public string Message { get; set; }

        private readonly ICustomerDiscountApplication _customerDiscountApplication;
        private readonly IProductApplication _productApplication;
        public List<CustomerDiscountViewModel> _customerDiscountViewModels;
        public CustomerDiscountSearchModel SearchModel;

        public SelectList products;
        public IndexModel(IProductApplication productApplication, ICustomerDiscountApplication customerDiscountApplication)
        {
            _productApplication = productApplication;
            _customerDiscountApplication = customerDiscountApplication;

        }


        public void OnGet(CustomerDiscountSearchModel searchModel)
        {
            products = new SelectList(_productApplication.GetProducts(), "Id", "Name");
            _customerDiscountViewModels = _customerDiscountApplication.Search(searchModel);

        }
        public PartialViewResult OnGetCreate()
        {
            var command = new DefineCustomerDiscount()
            {
                Products = _productApplication.GetProducts()
            };
            return Partial("./Create", command);
        }


        public JsonResult OnPostCreate(DefineCustomerDiscount command)
        {
            var result = _customerDiscountApplication.Create(command);
            return new JsonResult(result);
        }
        public PartialViewResult OnGetEdit(long id)
        {
            var customerDiscount = _customerDiscountApplication.GetDetails(id);
            customerDiscount.Products = _productApplication.GetProducts();
            return Partial("Edit", customerDiscount);
        }
        public JsonResult OnPostEdit(EditCustomerDiscount command)
        {
            var result = _customerDiscountApplication.Edit(command);
            return new JsonResult(result);
        }


    }
}
