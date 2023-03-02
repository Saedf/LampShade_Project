using System.Globalization;
using _01_Framework.Application;
using _01_Framework.Infrastructure;
using Microsoft.EntityFrameworkCore;
using ShopManagement.Application.Contract.ProductPicture;
using ShopManagement.Domain.ProductPictureAgg;

namespace ShopManagement.Infrastructure.EfCore.Repository;

public class ProductPictureRepository:RepositoryBase<long,ProductPicture>,IProductPictureRepository
{
    private readonly ShopContext _shopContext;
    public ProductPictureRepository(ShopContext context) : base(context)
    {
        _shopContext = context;
    }

    public EditProductPicture GetDetails(long id)
    {
        return _shopContext.ProductPictures.Include(x => x.Product)
            .Select(x=> new EditProductPicture
            {
                Id = x.Id,
                Picture = x.Picture,
                ProductId = x.ProductId,
                PictureAlt = x.PictureAlt,
                PictureTitle = x.PictureTitle,
            }).FirstOrDefault(x=>x.Id==id);
    }

    public List<ProductPictureViewModel> searchModel(ProductPictureSearchModel searchModel)
    {
        var query = _shopContext.ProductPictures
            .Include(x => x.Product)
            .Select(x => new ProductPictureViewModel
            {
                Id = x.Id,
                CreationDate = x.CreationDate.ToFarsi(),
                Picture = x.Picture,
                Product = x.Product.Name,
                ProductId = x.ProductId,
                IsRemoved = x.IsRemoved,
                PictureAlt = x.PictureAlt,
                PictureTitle = x.PictureTitle,
            });
        if (searchModel.ProductId!=0)
        {
           
           query= query.Where(x => x.ProductId == searchModel.ProductId);
        }

        return query.OrderByDescending(x => x.Id).ToList();
    }
}