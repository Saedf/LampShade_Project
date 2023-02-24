using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using _01_Framework.Application;
using ShopManagement.Application.Contract.Product;
using ShopManagement.Domain.ProductAgg;

namespace ShopManagement.Application
{
    public class ProductApplication:IProductApplication
    {
        private readonly IProductRepository _productRepository;

        public ProductApplication(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }

        public OperationResult Create(CreateProduct command)
        {

            var operation= new OperationResult();
            if (_productRepository.Exists(x=>x.Name==command.Name))
            {
                return operation.Faild(ApplicationMessage.DuplicatedRecord);
            }
            var slug= command.Slug.SlugiFy();
            var product = new Product(command.Name, command.Code, command.UnitPrice, command.ShortDescription
                , command.Description, command.Picture, command.PictureAlt, command.PictureTitle, slug,
                command.KeyWords,command.MetaDescription,command.CategoryId);
            _productRepository.Create(product);
            _productRepository.SaveChanges();
            return operation.Succeeded();
        }

        public OperationResult Edit(EditProduct command)
        {
            var operation=new OperationResult();
            var product = _productRepository.GetBy(command.Id);
            if (product==null)
            {
                return operation.Faild(ApplicationMessage.RecordNotFound);
            }

            if (_productRepository.Exists(x=>x.Name==command.Name && x.Id!=command.Id))
            {
                return operation.Faild(ApplicationMessage.DuplicatedRecord);
            }

            product.Edit(command.Name,command.Code,command.UnitPrice,command.ShortDescription
            ,command.Description,command.Picture,command.PictureAlt,command.PictureTitle,command.KeyWords
            ,command.MetaDescription,command.CategoryId);
            _productRepository.SaveChanges();
            return operation.Succeeded();
        }

        public List<ProductViewModel> Search(ProductSearchModel searchModel)
        {
            return _productRepository.Search(searchModel);
        }

        public OperationResult IsInStock(long id)
        {
            var operation= new OperationResult();
            var product= _productRepository.GetBy(id);
            if (product==null)
            {
                return operation.Faild(ApplicationMessage.RecordNotFound);
            }
            product.InStock();
            _productRepository.SaveChanges();
            return operation.Succeeded();
        }

        public OperationResult NotInStock(long id)
        { 
            var operation= new OperationResult();
            var product = _productRepository.GetBy(id);
            if (product==null)
            {
                return operation.Faild(ApplicationMessage.RecordNotFound);
            }
            product.NotInStock();
            _productRepository.SaveChanges();
            return operation.Succeeded();
        }

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
