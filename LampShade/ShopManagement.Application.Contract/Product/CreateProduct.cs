using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using _01_Framework.Application;
using ShopManagement.Application.Contract.ProductCategory;

namespace ShopManagement.Application.Contract.Product
{
    public class CreateProduct
    {
        [Required(ErrorMessage = ValidationMessages.IsRequired)]
        public string Name { get; set; }
        [Required(ErrorMessage = ValidationMessages.IsRequired)]
        public string Code { get; set; }
        //[Range(1,100000,ErrorMessage = ValidationMessages.IsRequired)]
        //public double UnitPrice { get; set; }
        [Required(ErrorMessage = ValidationMessages.IsRequired)]
        public string ShortDescription { get; set; }
        [Required(ErrorMessage = ValidationMessages.IsRequired)]
        public string Description { get; set; }
        [Required(ErrorMessage = ValidationMessages.IsRequired)]
        public string Picture { get; set; }
        [Required(ErrorMessage = ValidationMessages.IsRequired)]
        public string PictureAlt { get; set; }
        [Required(ErrorMessage = ValidationMessages.IsRequired)]
        public string PictureTitle { get; set; }
        
        public long CategoryId { get; set; }

        public string Slug { get; set; }
        public string KeyWords { get; set; }
        public string MetaDescription { get; set; }
        [Required(ErrorMessage = ValidationMessages.IsRequired)]
        public List<ViewModelProductCategory> ProductCategories { get; set; }
    }
}
