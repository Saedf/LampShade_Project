using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using _01_Framework.Application;
using _01_Framework.Infrastructure;
using Microsoft.IdentityModel.Tokens;
using ShopManagement.Application.Contract.ProductCategory;
using ShopManagement.Domain.ProductCategoryAgg;

namespace ShopManagement.Infrastructure.EfCore.Repository
{
    public class ProductCategoryRepository:RepositoryBase<long,ProductCategory>,IProductCategoryRepository
    {
        private readonly ShopContext _context;

        public ProductCategoryRepository(ShopContext context):base(context)
        {
            _context = context;
        }

        //public void Create(ProductCategory category)
        //{
        //    _context.ProductCategories.Add(category);
        //}

        //public ProductCategory GetBy(long id)
        //{
        //    return _context.ProductCategories.Find(id);
        //}

        //public List<ProductCategory> GetAll()
        //{
        //    return _context.ProductCategories.ToList();
        //}

        //public bool Exists(Expression<Func<ProductCategory, bool>> expression)
        //{
        //    return _context.ProductCategories.Any(expression);
        //}

        //public void SaveChanges()
        //{
        //    _context.SaveChanges();
        //}

        public EditProductCategory? GetDetailsBy(long id)
        {
            return _context.ProductCategories.Select(x=>
                new EditProductCategory()
                {
                    Id = x.Id,
                    Description = x.Description,
                    Keywords = x.Keywords,
                    MetaDescription = x.MetaDescription,
                    Name = x.Name,
                    //Picture = x.Picture.,
                    PictureAlt = x.PictureAlt,
                    PictureTitle = x.PictureTitle,
                    Slug = x.Slug
                }).FirstOrDefault(x => x.Id == id);
        }

        public List<ViewModelProductCategory> Search(SearchModelProductCategory searchModel)
        {
            var query = _context.ProductCategories.Select(x => new ViewModelProductCategory()
            {
                Id = x.Id,
                CreationDate = x.CreationDate.ToFarsi(),
                Name = x.Name,
                Picture = x.Picture,
                
                
            });
            if (!string.IsNullOrEmpty(searchModel.Name))
            {
                query = query.Where(x => x.Name.Contains(searchModel.Name));
            }

            return query.OrderByDescending(x => x.Id).ToList();
        }

        public List<ViewModelProductCategory> getProductGateCategories()
        {
            return _context.ProductCategories.Select(x => new ViewModelProductCategory
            {
                Name = x.Name,
                Id = x.Id
            }).ToList();
        }
    }
}
