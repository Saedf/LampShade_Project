namespace _02_LampShadeQuery.Contracts.Product;

public interface IProductQuery
{
    List<ProductQueryModel> GetLatestArrivals();
}