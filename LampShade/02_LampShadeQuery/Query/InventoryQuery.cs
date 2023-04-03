using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using _02_LampShadeQuery.Inventory;
using InventoryManagement.Infrustructure.EfCore;
using ShopManagement.Infrastructure.EfCore;

namespace _02_LampShadeQuery.Query
{
    public class InventoryQuery:IInventoryQuery
    {
        private readonly ShopContext _shopContext;
        private readonly InventoryContext _inventoryContext;

        public InventoryQuery(InventoryContext inventoryContext, ShopContext shopContext)
        {
            _inventoryContext = inventoryContext;
            _shopContext = shopContext;
        }

        public StockStatus CheckStock(IsInStock command)
        {
            var inventory = _inventoryContext.Inventory
                .FirstOrDefault(x => x.ProductId == command.ProductId);
            if (inventory==null || inventory.CalculateCurrentCount()<command.Count)
            {
                var product = _shopContext.Products.Where(x => x.Id == command.ProductId)
                    .Select(x => new { x.Id, x.Name })
                    .FirstOrDefault();
                return new StockStatus
                {
                    IsStock = false,
                    ProductName = product?.Name
                };
            }

            return new StockStatus
            {
                IsStock = true
            };
        }
    }
}
