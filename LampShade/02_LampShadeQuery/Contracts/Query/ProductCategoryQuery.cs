using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using _02_LampShadeQuery.Contracts.ProductCategory;
using ShopManagement.Infrastructure.EfCore;

namespace _02_LampShadeQuery.Contracts.Query
{
    public class ProductCategoryQuery:IProductCategoryQuery
    {
        private readonly ShopContext _shopContext;

        public ProductCategoryQuery(ShopContext shopContext)
        {
            _shopContext = shopContext;
        }

        public List<ProductCategoryQueryModel> GetProductCategoryQueryModels()
        {
            return _shopContext.ProductCategories
                .Select(x=>new ProductCategoryQueryModel
                {
                    Id = x.Id ,
                    Picture = x.Picture,
                    Name = x.Name,
                    PictureAlt = x.PictureAlt,
                    PictureTitle = x.PictureTitle,
                    Slug = x.Slug
                })
                .ToList();
        }
    }
}
