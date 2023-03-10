using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using _02_LampShadeQuery.Contracts.Product;

namespace _02_LampShadeQuery.Contracts.ProductCategory
{
    public class ProductCategoryQueryModel
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public string Picture { get; set; }
        public string PictureAlt { get; set; }
        public string PictureTitle { get; set; }
        public string Slug { get; set; }
        public string KeyWords { get; set; }
        public string MetaDescription { get; set; }
        public string Description { get; set; }
        public List<ProductQueryModel> ProductQueryModels { get; set; }


    }
}
