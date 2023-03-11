using _02_LampShadeQuery;
using _02_LampShadeQuery.Contracts.ArticleCategory;
using _02_LampShadeQuery.Contracts.ProductCategory;
using Microsoft.AspNetCore.Mvc;

namespace ServiceHost.ViewComponents
{
    public class MenuViewComponent:ViewComponent
    {
        private readonly IProductCategoryQuery _productCategoryQuery;
        private readonly IArticleCategoryQuery _articleCategoryQuery;

        public MenuViewComponent(IProductCategoryQuery productCategoryQuery, IArticleCategoryQuery articleCategoryQuery)
        {
            _productCategoryQuery = productCategoryQuery;
            _articleCategoryQuery = articleCategoryQuery;
        }

        public IViewComponentResult Invoke()
        {
            var result = new MenuModel
            {
                ArticleCategoryQueries = _articleCategoryQuery.GetArticleCategories(),
                ProductCategoryQueris = _productCategoryQuery.GetProductCategoryQueryModels()
            };
            return View(result);
        }
    }
}
