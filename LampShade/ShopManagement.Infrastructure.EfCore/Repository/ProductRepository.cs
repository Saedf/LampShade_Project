using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using _01_Framework.Application;
using _01_Framework.Infrastructure;
using Microsoft.EntityFrameworkCore;
using ShopManagement.Application.Contract.Product;
using ShopManagement.Domain.ProductAgg;

namespace ShopManagement.Infrastructure.EfCore.Repository
{
    public class ProductRepository:RepositoryBase<long,Product>,IProductRepository
    {
        private readonly ShopContext _shopContext;

        public ProductRepository(ShopContext shopContext) : base(shopContext)
        {
            _shopContext = shopContext;
        }

        public EditProduct GetDetails(long id)
        {
            return _shopContext.Products.Select(x=>new EditProduct
                {
                    Id = x.Id,
                    CategoryId = x.CategoryId,
                    Code = x.Code,
                    Description = x.Description,
                    KeyWords = x.KeyWords,
                    MetaDescription = x.MetaDescription,
                    Name = x.Name,
                    // Picture = x.Picture,
                    PictureAlt = x.PictureAlt,
                    PictureTitle = x.PictureTitle,
                    Slug = x.Slug,
                    ShortDescription = x.ShortDescription,
                    //UnitPrice = x.UnitPrice
                })
                .FirstOrDefault(x=>x.Id==id);
        }

        public List<ProductViewModel> Search(ProductSearchModel searchModel)
        {
            var query = _shopContext.Products.Include(x => x.Category)
                .Select(x => new ProductViewModel
                {
                    Id = x.Id,
                    Code = x.Code,
                    Name = x.Name,
                    CategoryId =x.CategoryId, 
                    Category = x.Category.Name,
                    Picture = x.Picture,
                    //UnitPrice = x.UnitPrice,
                    //IsInstock = x.IsInStock,
                    CreationDate = x.CreationDate.ToFarsi()
                });
            if (!string.IsNullOrWhiteSpace(searchModel.Name))
            {
                query = query.Where(x => x.Name.Contains(searchModel.Name));
            }

            if (!string.IsNullOrWhiteSpace(searchModel.Code))
            {
                query = query.Where(x => x.Code.Contains(searchModel.Code));
            }

            if (searchModel.CategoryId!=0)
            {
                query = query.Where(x => x.CategoryId == searchModel.CategoryId);
            }

            return query.OrderByDescending(x => x.Id).ToList();

        }

        public List<ProductViewModel> GetProducts()
        {
            return _shopContext.Products.Select(x => new ProductViewModel
            {
                Id = x.Id,
                Name = x.Name

            }).ToList();
        }

        public Product GetProductWithCategory(long id)
        {
            return _shopContext.Products.Include(x => x.Category)
                .FirstOrDefault(x => x.Id == id);
        }
    }

}
