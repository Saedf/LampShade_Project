using _01_Framework.Application;

namespace ShopManagement.Application.Contract.ProductCategory
{
    public interface IProductCategoryApplication
    {
        OperationResult Create(CreateProductCategory command);
        OperationResult Edit(EditProductCategory command);
        List<ViewModelProductCategory> Search(SearchModelProductCategory searchModel);
        EditProductCategory GetDetailsBy(long id);
        List<ViewModelProductCategory> getProductCategories();
    }
}
