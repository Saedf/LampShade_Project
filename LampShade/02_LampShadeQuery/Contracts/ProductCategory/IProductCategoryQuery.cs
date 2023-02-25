namespace _02_LampShadeQuery.Contracts.ProductCategory;

public interface IProductCategoryQuery
{
    List<ProductCategoryQueryModel> GetProductCategoryQueryModels();
}