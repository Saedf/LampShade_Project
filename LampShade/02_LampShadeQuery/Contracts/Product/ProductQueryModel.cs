﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using _02_LampShadeQuery.Contracts.Comment;

namespace _02_LampShadeQuery.Contracts.Product
{
    public class ProductQueryModel
    {
        public long Id { get; set; }
        public string Picture { get; set; }
        public string PictureAlt { get; set; }
        public string PictureTitle { get; set; }
        public string Name { get; set; }

        public string Price { get; set; }
        public decimal DecimalPrice { get; set; }
        public string PriceWithDiscount { get; set; }
        public int DiscountRate { get; set; }
        public string Category { get; set; }
        public string CategorySlug { get; set; }
        public string Slug { get; set; }
        public bool HasDiscount { get; set; }
        public string Keywords { get; set; }
        public string DiscountExpireDate { get; set; }
        public string ShortDescription { get; set; }
        public string Code { get; set; }
        public string MetaDescription { get; set; }
        public List<ProductPictureQueryModel> pictureQueryModels { get; set; }
        public bool IsInStock { get; set; }
        public List<CommentQueryModel> commentQueryModels { get; set; }
    }
}
