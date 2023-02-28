using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using _01_Framework.Domain;

namespace InventoryManagement.Domain.InventoryAgg
{
    public class Inventory:EntityBase
    {
        public long ProductId { get; private set; }
        public bool InStuck { get; private set; }
        public decimal UnitPrice { get; private set; }
        public List<InventoryOperation> InventoryOperations { get; private set; }

        public Inventory(long productId, decimal unitPrice)
        {
            ProductId = productId;
            UnitPrice = unitPrice;
            InStuck=false;
        }
        public void Edit(long productId, decimal unitPrice)
        {
            ProductId = productId;
            UnitPrice = unitPrice;
            
        }

        public long CalculateCurrentCount()
        {
            var plus = InventoryOperations
                .Where(x => x.Operation).Sum(x => x.Count);
            var minus = InventoryOperations.Where(x => !x.Operation)
                .Sum(x => x.Count);
            return plus - minus;
        }

        public void Increase(long count, long operatorId, string description)
        {
            var currentCount = CalculateCurrentCount()+count;
            var operation = new InventoryOperation(true,count,operatorId,currentCount,description,0,Id);
            InventoryOperations.Add(operation);

            InStuck = currentCount > 0;

        }

        public void Reduce(long count, long operatorId,string description,long orderId)
        {
            var currentCount = CalculateCurrentCount() - count;
            var operation = new InventoryOperation(false, count, operatorId, currentCount, description, orderId, Id);
            InventoryOperations.Add(operation);
            InStuck=currentCount > 0;
        }

    }
}
