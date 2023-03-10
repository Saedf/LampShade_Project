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
            if (_inventoryRepository.Exists(x=>x.ProductId==command.ProductId))
            {
                return operation.Faild(ApplicationMessage.DuplicatedRecord);
            }

            var inventory = new Inventory(command.ProductId, command.UnitPrice);
            _inventoryRepository.Create(inventory);
            _inventoryRepository.SaveChanges();
            return operation.Succeeded();
        }

        public OperationResult Edit(EditInventory command)
        {
            var operation = new OperationResult();
            var inventory = _inventoryRepository.GetBy(command.Id);
            if (inventory==null)
            {
                return operation.Faild(ApplicationMessage.RecordNotFound);
            }

            if (_inventoryRepository.Exists(x=>x.ProductId==command.ProductId && x.Id!=command.Id))
            {
                return operation.Faild(ApplicationMessage.DuplicatedRecord);
            }
            inventory.Edit(command.ProductId,command.UnitPrice);
            _inventoryRepository.SaveChanges();
            return operation.Succeeded();
        }

        public List<InventoryViewModel> Search(InventorySearchModel searchModel)
        {
            return _inventoryRepository.Search(searchModel);
        }

        public EditInventory GetDetails(long id)
        {
            return _inventoryRepository.GetDetails(id);
        }

        public OperationResult Increase(IncreaseInventory command)
        {
            var operation = new OperationResult();
            var inventory = _inventoryRepository.GetBy(command.InventoryId);
            if (inventory==null)
            {
                return operation.Faild(ApplicationMessage.RecordNotFound);
            }

            const long operatorId = 1;
            inventory.Increase(command.Count,operatorId,command.Description);
            _inventoryRepository.SaveChanges();
            return operation.Succeeded();
        }

        public OperationResult Reduce(ReduceInventory command)
        {
            var operation = new OperationResult();
            var inventory = _inventoryRepository.GetBy(command.InventoryId);
            if (inventory == null)
            {
                return operation.Faild(ApplicationMessage.RecordNotFound);
            }

            const long operatorId = 1;
            inventory.Reduce(command.Count, operatorId, command.Description,0);
            _inventoryRepository.SaveChanges();
            return operation.Succeeded();
        }

        public OperationResult Reduce(List<ReduceInventory> command)
        {
            var operation = new OperationResult();
            const long operatorId = 1;

            foreach (var item in command)
            {
                var inventory = _inventoryRepository.GetBy(item.InventoryId);
                inventory.Reduce(item.Count,operatorId,item.Description,item.OrderId);
            }
            _inventoryRepository.SaveChanges();
            return operation.Succeeded();
        }

        public List<InventoryOperationViewModel> GerOperationLog(long inventoryId)
        {
            return _inventoryRepository.GerOperationLog(inventoryId);
        }
    }
}
