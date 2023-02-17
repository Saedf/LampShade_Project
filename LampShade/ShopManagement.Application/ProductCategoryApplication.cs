using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using _01_Framework.Application;
using ShopManagement.Application.Contract.ProductCategory;
using ShopManagement.Domain.ProductCategoryAgg;

namespace ShopManagement.Application
{
    public class ProductCategoryApplication:IProductCategoryApplication
    {
        private readonly IProductCategoryRepository _categoryRepository;

        public ProductCategoryApplication(IProductCategoryRepository categoryRepository)
        {
            _categoryRepository = categoryRepository;
        }

        public OperationResult Create(CreateProductCategory command)
        {
            var operation= new OperationResult();
            if (_categoryRepository.Exists(x=>x.Name==command.Name))
            {
                return operation.Faild("امکان ثبت رکورد تکراری وجود ندارد. لطفا مجددا تلاش بفرمایید");
            }

            var slug = command.Slug.SlugiFy();
            var productCategory=new ProductCategory(command.Name,command.Picture,command.PictureAlt,
                command.PictureTitle,command.Description,command.Keywords,command.MetaDescription,slug);
            _categoryRepository.Create(productCategory);
            _categoryRepository.SaveChanges(); 
            return operation.Succeeded();
        }

        public OperationResult Edit(EditProductCategory command)
        {
            var operationResult=new OperationResult();
            var productCategory = _categoryRepository.GetBy(command.Id);
            if (productCategory == null)
            {
                return operationResult.Faild("مجددا تلاش بفرمایید ");

            }

            if (_categoryRepository.Exists(x=>x.Name==command.Name && x.Id!=command.Id))
            {
                return operationResult.Faild("امکان ثبت رکورد تکراری وجود ندارد ");
            }
            var slug=command.Slug.SlugiFy();
            productCategory.Edit(command.Name,command.Picture,command.PictureAlt,command.PictureTitle,command.Description
                ,command.Keywords,command.MetaDescription,slug);

            _categoryRepository.SaveChanges();
            return operationResult.Succeeded();
        }

        public List<ViewModelProductCategory> Search(SearchModelProductCategory searchModel)
        {
            return _categoryRepository.Search(searchModel);
        }

        public EditProductCategory GetDetailsBy(long id)
        {
            
            return _categoryRepository.GetDetailsBy(id);
        }
    }
}
