using System.Linq.Expressions;
using _01_Framework.Domain;
using ShopManagement.Application.Contract.ProductCategory;

namespace ShopManagement.Domain.ProductCategoryAgg
{
    public interface IProductCategoryRepository:IRepository<long,ProductCategory>
    {
       
        EditProductCategory? GetDetailsBy(long id);
        List<ViewModelProductCategory> Search(SearchModelProductCategory searchModel);

    }
}
