using _02_LampShadeQuery.Contracts.ProductCategory;
using Microsoft.AspNetCore.Mvc;

namespace ServiceHost.ViewComponents
{
    public class ProductCategoryViewComponent:ViewComponent
    {
        private readonly IProductCategoryQuery _categoryQuery;

        public ProductCategoryViewComponent(IProductCategoryQuery categoryQuery)
        {
            _categoryQuery = categoryQuery;
        }

        public IViewComponentResult Invoke()
        {
            var productcategories = _categoryQuery.GetProductCategoryQueryModels();
            return View(productcategories);
        }

    }
}
