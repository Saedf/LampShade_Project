using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using _01_Framework.Application;
using ShopManagement.Application.Contract.Product;
using ShopManagement.Domain.ProductAgg;
using ShopManagement.Domain.ProductCategoryAgg;

namespace ShopManagement.Application
{
    public class ProductApplication:IProductApplication
    {
        private readonly IProductRepository _productRepository;
        private readonly IFileUploader _fileUploader;
        private readonly IProductCategoryRepository _categoryRepository;
        public ProductApplication(IProductRepository productRepository,
            IFileUploader fileUploader, IProductCategoryRepository categoryRepository)
        {
            _productRepository = productRepository;
            _fileUploader = fileUploader;
            _categoryRepository = categoryRepository;
        }

        public OperationResult Create(CreateProduct command)
        {

            var operation= new OperationResult();
            if (_productRepository.Exists(x=>x.Name==command.Name))
            {
                return operation.Faild(ApplicationMessage.DuplicatedRecord);
            }
           // var slug= command.Slug.SlugiFy();
            var categorySlug = _categoryRepository.GetSlugBy(command.CategoryId);
            var path = $"{categorySlug}//{command.Slug}";
            var picturePath = _fileUploader.Upload(command.Picture, path);
            var product = new Product(command.Name, command.Code, command.ShortDescription
                , command.Description, picturePath, command.PictureAlt, command.PictureTitle, command.Slug,
                command.KeyWords,command.MetaDescription,command.CategoryId);
            _productRepository.Create(product);
            _productRepository.SaveChanges();
            return operation.Succeeded();
        }

        public OperationResult Edit(EditProduct command)
        {
            var operation=new OperationResult();
            var product = _productRepository.GetProductWithCategory(command.Id);
            if (product==null)
            {
                return operation.Faild(ApplicationMessage.RecordNotFound);
            }

            if (_productRepository.Exists(x=>x.Name==command.Name && x.Id!=command.Id))
            {
                return operation.Faild(ApplicationMessage.DuplicatedRecord);
            }

            var path = $"{product.Category.Slug}/{command.Slug}";
            var picturePath = _fileUploader.Upload(command.Picture, path);

            product.Edit(command.Name,command.Code,command.ShortDescription
            ,command.Description, picturePath, command.PictureAlt,command.PictureTitle,command.KeyWords
            ,command.MetaDescription,command.CategoryId);
            _productRepository.SaveChanges();
            return operation.Succeeded();
        }

        public List<ProductViewModel> Search(ProductSearchModel searchModel)
        {
            return _productRepository.Search(searchModel);
        }

        //public OperationResult IsInStock(long id)
        //{
        //    var operation= new OperationResult();
        //    var product= _productRepository.GetBy(id);
        //    if (product==null)
        //    {
        //        return operation.Faild(ApplicationMessage.RecordNotFound);
        //    }
        //    product.InStock();
        //    _productRepository.SaveChanges();
        //    return operation.Succeeded();
        //}

        //public OperationResult NotInStock(long id)
        //{ 
        //    var operation= new OperationResult();
        //    var product = _productRepository.GetBy(id);
        //    if (product==null)
        //    {
        //        return operation.Faild(ApplicationMessage.RecordNotFound);
        //    }
        //    product.NotInStock();
        //    _productRepository.SaveChanges();
        //    return operation.Succeeded();
        //}

        public EditProduct GetDetails(long id)
        {
            return _productRepository.GetDetails(id);
        }

        public List<ProductViewModel> GetProducts()
        {
            return _productRepository.GetProducts();
        }
    }
}
