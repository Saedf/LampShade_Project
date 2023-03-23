using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShopManagement.Infrastructure.Core.Permissions
{
    public static class ShopPermissions
    {
        //Product
        public const int ListProducts = 10;
        public const int SearchProducts = 11;
        public const int CreateProduct = 12;
        public const int EditProduct = 13;

        //ProductCategories
        public const int ListProductCategories = 20;
        public const int SearchProductCategories = 21;
        public const int CreateProductCategories = 22;
        public const int EditProductCategories = 203;

    }
}
