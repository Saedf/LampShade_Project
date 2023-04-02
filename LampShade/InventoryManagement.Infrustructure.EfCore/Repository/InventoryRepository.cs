using _01_Framework.Application;
using _01_Framework.Infrastructure;
using AccountManagement.Infrastructure.EfCore;
using InventoryManagement.Application.Contract.Inventory;
using InventoryManagement.Domain.InventoryAgg;
using ShopManagement.Infrastructure.EfCore;

namespace InventoryManagement.Infrustructure.EfCore.Repository
{
    public class InventoryRepository:RepositoryBase<long,Inventory>,IInventoryRepository
    {
        private readonly InventoryContext _inventoryContext;
        private readonly ShopContext _shopContext;
        private readonly AccountContext _accountContext; 
        public InventoryRepository(InventoryContext inventoryContext,
            ShopContext shopContext, AccountContext accountContext):base(inventoryContext)
        {
            _inventoryContext = inventoryContext;
            _shopContext = shopContext;
            _accountContext = accountContext;
        }

      

   

        public EditInventory GetDetails(long id)
        {
            return _inventoryContext.Inventory.Select(x => new EditInventory
            {
                Id = x.Id,
                ProductId = x.ProductId,
                UnitPrice = x.UnitPrice,
            }).FirstOrDefault(x=>x.Id==id);
        }

        public List<InventoryViewModel> Search(InventorySearchModel searchModel)
        {
            var products = _shopContext.Products.Select(x => new {x.Name,x.Id}).ToList();
            var query = _inventoryContext.Inventory.Select(x => new InventoryViewModel
            {
                Id = x.Id,
                UnitPrice = x.UnitPrice,
                InStock = x.InStuck,
                ProductId = x.ProductId,
                CreationDate = x.CreationDate.ToFarsi(),
                CurrentCount = x.CalculateCurrentCount()
            });
            if (searchModel.ProductId>0)
            {
                query = query.Where(x => x.ProductId == searchModel.ProductId);
            }

            if (searchModel.InStock)
            {
                query = query.Where(x => !x.InStock);
            }

            var inventory = query.OrderByDescending(x => x.Id).ToList();
            inventory.ForEach
                (item=>item.Product=products.FirstOrDefault(x=>x.Id==item.ProductId)?.Name);
            return inventory;

        }

        public Inventory GetBy(long productId)
        {
            return _inventoryContext.Inventory.FirstOrDefault(x => x.ProductId == productId);
        }

        public List<InventoryOperationViewModel> GerOperationLog(long inventoryId)
        {
            var accounts=_accountContext.Accounts.
                Select(x=>new {x.Id,x.FullName}).ToList();

            var inventory=_inventoryContext.Inventory.FirstOrDefault(x=>x.Id==inventoryId);
            var operations= inventory.InventoryOperations
                .Select(x=>new InventoryOperationViewModel
                {
                    Id = x.Id,
                    Count = x.Count,
                    Description = x.Description,
                    OrderId = x.OrderId,
                    OperationDate = x.OperationDate.ToFarsi(),
                    Operation = x.Operation,
                    OperatorId = x.OperatorId,
                    CurrentCount = x.CurrentCount,
                    
                })
                .OrderByDescending(x=>x.Id)
                .ToList();
            foreach (var operation in operations)
            {
                operation.Operator = accounts.FirstOrDefault(x => x.Id == operation.OperatorId)?.FullName;
            }
            return operations;
        }
    }
}
