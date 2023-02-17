using System.Linq.Expressions;
using ShopManagement.Application.Contract.ProductCategory;

namespace ShopManagement.Domain.ProductCategoryAgg
{
    public interface IProductCategoryRepository
    {
        void Create(ProductCategory category);
        ProductCategory GetBy(long id);
        List<ProductCategory> GetAll();
        bool Exists(Expression<Func<ProductCategory,bool>> expression);
        void SaveChanges();
        EditProductCategory GetDetailsBy(long id);
        List<ViewModelProductCategory> Search(SearchModelProductCategory searchModel);

    }
}
