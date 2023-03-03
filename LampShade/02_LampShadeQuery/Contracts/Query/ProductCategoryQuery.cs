using _02_LampShadeQuery.Contracts.Product;
using _02_LampShadeQuery.Contracts.ProductCategory;
using InventoryManagement.Infrustructure.EfCore;
using Microsoft.EntityFrameworkCore;
using ShopManagement.Infrastructure.EfCore;
using _01_Framework.Application;
using DiscountManagement.Infrastructure.EfCore;

namespace _02_LampShadeQuery.Contracts.Query
{
    public class ProductCategoryQuery : IProductCategoryQuery
    {
        private readonly ShopContext _shopContext;
        private readonly InventoryContext _inventoryContext;
        private readonly DiscountContext _discountContext;
        public ProductCategoryQuery(ShopContext shopContext,
            InventoryContext inventoryContext,
            DiscountContext discountContext)
        {
            _shopContext = shopContext;
            _inventoryContext = inventoryContext;
            _discountContext = discountContext;
        }

        public List<ProductCategoryQueryModel> GetProductCategoryQueryModels()
        {
            return _shopContext.ProductCategories
                .Select(x => new ProductCategoryQueryModel
                {
                    Id = x.Id,
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
            var inventory = _inventoryContext.Inventory
                .Where(x => x.InStuck == true)
                .Select(x =>
                new { x.ProductId, x.UnitPrice, x.InStuck }).ToList();

            var discounts = _discountContext.CustomerDiscounts
                .Where(x => x.StartDate <= DateTime.Now && x.EndDate >= DateTime.Now)
                .Select(x => new { x.DiscountRate, x.ProductId }).ToList();

            var categories = _shopContext.ProductCategories.Include(x => x.Products)
                .ThenInclude(x => x.Category)
                .Select(x => new ProductCategoryQueryModel
                {
                    Id = x.Id,
                    Name = x.Name,
                    ProductQueryModels = MapProducts(x.Products)

                }).ToList();
            foreach (var item in categories)
            {
                foreach (var product in item.ProductQueryModels)
                {
                    var productInventory =
                        inventory.FirstOrDefault(x => x.ProductId == product.Id && x.InStuck);
                    if (productInventory != null)
                    {
                        var price = productInventory.UnitPrice;
                        product.Price = price.ToMoney();
                        var discount =
                            discounts.FirstOrDefault(x => x.ProductId == product.Id);
                        if (discount != null)
                        {
                            int discountrate = discount.DiscountRate;
                            product.DiscountRate = discountrate;
                            product.HasDiscount = discountrate > 0;
                            var discountAmount = Math.Round((price * discountrate) / 100);
                            product.PriceWithDiscount = (price - discountAmount).ToMoney();

                        }
                    }
                }
            }
            return categories;
        }

        public ProductCategoryQueryModel GetProductCategoryWithProductsBy(string slug)
        {
            var inventory = _inventoryContext.Inventory
                .Where(x => x.InStuck == true)
                .Select(x =>
                    new { x.ProductId, x.UnitPrice, x.InStuck }).ToList();

            var discounts = _discountContext.CustomerDiscounts
                .Where(x => x.StartDate <= DateTime.Now && x.EndDate >= DateTime.Now)
                .Select(x => new { x.DiscountRate, x.ProductId,x.EndDate}).ToList();

            var category = _shopContext.ProductCategories
                .Include(x => x.Products)
                .ThenInclude(x => x.Category)
                .Select(x => new ProductCategoryQueryModel
                {
                    Id = x.Id,
                    Name = x.Name,
                    Description = x.Description,
                    KeyWords = x.Keywords,
                    MetaDescription = x.MetaDescription,
                    Slug = x.Slug,
                    ProductQueryModels = MapProducts(x.Products),
                    

                }).AsNoTracking().FirstOrDefault(x => x.Slug == slug);

            foreach (var product in category.ProductQueryModels)
            {
                var productInventory =
                    inventory.FirstOrDefault(x => x.ProductId == product.Id && x.InStuck);
                if (productInventory != null)
                {
                    var price = productInventory.UnitPrice;
                    product.Price = price.ToMoney();
                    var discount =
                        discounts.FirstOrDefault(x => x.ProductId == product.Id);
                    if (discount != null)
                    {
                        int discountrate = discount.DiscountRate;
                        product.DiscountRate = discountrate;
                        product.HasDiscount = discountrate > 0;
                        product.DiscountExpireDate = discount.EndDate.ToDiscountFormat();
                        var discountAmount = Math.Round((price * discountrate) / 100);
                        product.PriceWithDiscount = (price - discountAmount).ToMoney();

                    }
                }

            }

            return category;
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
                Slug = x.Slug,
                

            }).ToList();
        }
    }
}
