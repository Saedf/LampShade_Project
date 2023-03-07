namespace _02_LampShadeQuery.Contracts.Product;

public interface IProductQuery
{
    List<ProductQueryModel> GetLatestArrivals();
    List<ProductQueryModel> Search(string value);
    ProductQueryModel GetProductDetails(string slug);
}