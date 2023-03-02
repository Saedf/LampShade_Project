using _02_LampShadeQuery.Contracts.Product;

namespace _02_LampShadeQuery.Contracts.ProductCategory;

public interface IProductCategoryQuery
{
    List<ProductCategoryQueryModel> GetProductCategoryQueryModels();
    List<ProductCategoryQueryModel> GetProductCategoriesWithProducts();
}