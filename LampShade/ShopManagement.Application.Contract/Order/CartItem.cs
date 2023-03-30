using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShopManagement.Application.Contract.Order
{
    public class CartItem
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public string Picture { get; set; }
        public decimal UnitPrice { get; set; }
        public int Count { get; set; }
        public bool IsInStock { get; set; }
        public decimal TotalItemPrice { get; set; }
        public int DiscountRate { get; set; }
        public decimal DiscountAmount { get; set; }
        public decimal ItemPayAmount { get; set; }

        public CartItem()
        {
            TotalItemPrice = UnitPrice * Count;
        }


        public void CalculateTotalPrice()
        {
            TotalItemPrice = UnitPrice * Count;
        }

    }
}
