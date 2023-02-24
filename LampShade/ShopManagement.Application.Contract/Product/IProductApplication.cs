using _01_Framework.Application;

namespace ShopManagement.Application.Contract.Product;

public interface IProductApplication
{
    OperationResult Create(CreateProduct command);
    OperationResult Edit(EditProduct command);
    List<ProductViewModel> Search(ProductSearchModel searchModel);
    OperationResult IsInStock(long id);
    OperationResult NotInStock(long id);
    EditProduct GetDetails(long id);

    List<ProductViewModel> GetProducts();
}