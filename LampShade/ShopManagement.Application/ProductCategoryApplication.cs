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
        private readonly IFileUploader _fileUploader;
        public ProductCategoryApplication(IProductCategoryRepository categoryRepository, IFileUploader fileUploader)
        {
            _categoryRepository = categoryRepository;
            _fileUploader = fileUploader;
        }

        public OperationResult Create(CreateProductCategory command)
        {
            var operation= new OperationResult();
            if (_categoryRepository.Exists(x=>x.Name==command.Name))
            {
                return operation.Faild(ApplicationMessage.DuplicatedRecord);
            }

          //  var slug = command.Slug.SlugiFy();
            var picturePath = $"{command.Slug}";
            var fileName = _fileUploader.Upload(command.Picture, picturePath);
            var productCategory=new ProductCategory(command.Name, fileName, command.PictureAlt,
                command.PictureTitle,command.Description,command.Keywords,command.MetaDescription,command.Slug);
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
                return operationResult.Faild(ApplicationMessage.RecordNotFound);

            }

            if (_categoryRepository.Exists(x=>x.Name==command.Name && x.Id!=command.Id))
            {
                return operationResult.Faild(ApplicationMessage.DuplicatedRecord);
            }
            var slug = command.Slug.SlugiFy();
            var picturePath = $"{command.Slug}";
            var fileName = _fileUploader.Upload(command.Picture, picturePath);
            productCategory.Edit(command.Name, fileName, command.PictureAlt,command.PictureTitle,command.Description
                ,command.Keywords,command.MetaDescription, command.Slug);

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

        public List<ViewModelProductCategory> getProductCategories()
        {
            return _categoryRepository.getProductGateCategories();
        }
    }
}
