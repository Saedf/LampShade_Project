using _02_LampShadeQuery.Contracts.Product;
using _02_LampShadeQuery.Contracts.ProductCategory;
using InventoryManagement.Infrustructure.EfCore;
using Microsoft.EntityFrameworkCore;
using ShopManagement.Infrastructure.EfCore;
using _01_Framework.Application;
namespace _02_LampShadeQuery.Contracts.Query
{
    public class ProductCategoryQuery:IProductCategoryQuery
    {
        private readonly ShopContext _shopContext;
        private readonly InventoryContext _inventoryContext;

        public ProductCategoryQuery(ShopContext shopContext, InventoryContext inventoryContext)
        {
            _shopContext = shopContext;
            _inventoryContext = inventoryContext;
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
            var inventory=_inventoryContext.Inventory.Select(x=>
                new {x.ProductId,x.UnitPrice}).ToList();
            var categories= _shopContext.ProductCategories.Include(x => x.Products)
                .ThenInclude(x=>x.Category)
                .Select(x => new ProductCategoryQueryModel
                {
                    Id = x.Id,
                    Name = x.Name,
                    ProductQueryModels = MapProducts(x.Products)

                }).ToList();
            foreach (var item in categories)
            {
                foreach (var products in item.ProductQueryModels)
                {
                    var productInventory =
                        inventory.FirstOrDefault(x => x.ProductId == products.Id);
                    if (productInventory!=null)
                    {
                        var price = productInventory.UnitPrice;
                        products.Price = price.ToMoney();
                    }
                }
            }
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
