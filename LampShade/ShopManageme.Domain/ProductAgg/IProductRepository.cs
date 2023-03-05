using _01_Framework.Domain;
using Microsoft.Identity.Client;
using ShopManagement.Application.Contract.Product;

namespace ShopManagement.Domain.ProductAgg;

public interface IProductRepository:IRepository<long,Product>
{
    EditProduct GetDetails(long id);
    List<ProductViewModel> Search(ProductSearchModel searchModel);
    List<ProductViewModel> GetProducts();
    Product GetProductWithCategory(long id);
}