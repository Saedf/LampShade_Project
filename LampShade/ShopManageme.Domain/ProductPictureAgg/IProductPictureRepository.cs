using _01_Framework.Domain;
using ShopManagement.Application.Contract.ProductPicture;
using ShopManagement.Domain.ProductPictureAgg;

public interface IProductPictureRepository:IRepository<long,ProductPicture>
{
    EditProductPicture GetDetails(long id);
    List<ProductPictureViewModel> searchModel(ProductPictureSearchModel searchModel);

}