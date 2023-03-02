using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using _02_LampShadeQuery.Contracts.Product;
using _02_LampShadeQuery.Contracts.ProductCategory;
using Microsoft.EntityFrameworkCore;
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

        public List<ProductCategoryQueryModel> GetProductCategoriesWithProducts()
        {
            var categories= _shopContext.ProductCategories.Include(x => x.Products)
                .ThenInclude(x=>x.Category)
                .Select(x => new ProductCategoryQueryModel
                {
                    Id = x.Id,
                    Name = x.Name,
                    ProductQueryModels = MapProducts(x.Products)

                }).AsNoTracking().ToList();
            return categories;
        }

        private static List<ProductQueryModel> MapProducts(List<ShopManagement.Domain.ProductAgg.Product> products)
        {
            return products.Select(x => new ProductQueryModel
            {
                Id = x.Id,
                Name = x.Name,
                Category = x.Category.Name,
                Picture = x.Picture,
                PictureAlt = x.PictureAlt,
                PictureTitle = x.PictureTitle,
                Slug = x.Slug

            }).ToList();
        }
    }
}
