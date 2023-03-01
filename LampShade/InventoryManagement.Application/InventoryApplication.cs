using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using _01_Framework.Application;
using InventoryManagement.Application.Contract.Inventory;
using InventoryManagement.Domain.InventoryAgg;

namespace InventoryManagement.Application
{
    public class InventoryApplication:IInventoryApplication
    {
        private readonly IInventoryRepository _inventoryRepository;

        public InventoryApplication(IInventoryRepository inventoryRepository)
        {
            _inventoryRepository = inventoryRepository;
        }

        public OperationResult Create(CreateInventory command)
        {
            var operation=new OperationResult();
        }

        public OperationResult Edit(EditInventory command)
        {
            throw new NotImplementedException();
        }

        public List<InventoryViewModel> Search(InventorySearchModel searchModel)
        {
            throw new NotImplementedException();
        }

        public EditInventory GetDetails(long id)
        {
            throw new NotImplementedException();
        }

        public OperationResult Increase(IncreaseInventory command)
        {
            throw new NotImplementedException();
        }

        public OperationResult Reduce(ReduceInventory command)
        {
            throw new NotImplementedException();
        }

        public OperationResult Reduce(List<ReduceInventory> command)
        {
            throw new NotImplementedException();
        }
    }
}
