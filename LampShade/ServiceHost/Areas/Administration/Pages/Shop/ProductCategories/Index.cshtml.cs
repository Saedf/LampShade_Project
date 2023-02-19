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
    }
}
