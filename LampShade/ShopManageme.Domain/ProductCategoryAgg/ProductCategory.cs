using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Threading.Tasks;
using _01_Framework.Domain;
using ShopManagement.Domain.ProductAgg;


namespace ShopManagement.Domain.ProductCategoryAgg
{
    public class ProductCategory:EntityBase

    {
        public string Name { get; private set; }
        public string Picture { get; private set; }
        public string PictureAlt { get; private set; }
        public string PictureTitle { get; private set; }
        public string Description { get; private set; }
        public string Keywords { get; private set; }
        public string MetaDescription { get; private set; }
        public string Slug { get;private set; }

        public List<Product> Products { get; private set; }

        public ProductCategory(List<Product> products)
        {
            Products = products;
        }

        public ProductCategory(string name, string picture, string pictureAlt,
            string pictureTitle, string description, string keywords,
            string metaDescription, string slug)
        {
            Name = name;
            Picture = picture;
            PictureAlt = pictureAlt;
            PictureTitle = pictureTitle;
            Description = description;
            Keywords = keywords;
            MetaDescription = metaDescription;
            Slug = slug;
        }

        

        public void Edit(string name, string picture, string pictureAlt,
            string pictureTitle, string description, string keywords,
            string metaDescription,string slug)
        {
            Name = name;
            if (!string.IsNullOrWhiteSpace(picture))
            {
                Picture = picture;
            }
           
            PictureAlt = pictureAlt;
            PictureTitle = pictureTitle;
            Description = description;
            Keywords = keywords;
            MetaDescription = metaDescription;
            Slug = slug;
        }
    }
}
