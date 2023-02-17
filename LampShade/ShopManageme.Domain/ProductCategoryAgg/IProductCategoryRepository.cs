using System.Linq.Expressions;
using ShopManagement.Application.Contract.ProductCategory;

namespace ShopManagement.Domain.ProductCategoryAgg
{
    public interface IProductCategoryRepository
    {
       
        EditProductCategory? GetDetailsBy(long id);
        List<ViewModelProductCategory> Search(SearchModelProductCategory searchModel);

    }
}
