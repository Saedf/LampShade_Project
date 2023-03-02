using _01_Framework.Application;

namespace InventoryManagement.Application.Contract.Inventory;

public interface IInventoryApplication
{
    OperationResult Create(CreateInventory command);
    OperationResult Edit(EditInventory command);
    List<InventoryViewModel> Search(InventorySearchModel searchModel);
    EditInventory GetDetails(long id);
    OperationResult Increase(IncreaseInventory command);
    OperationResult Reduce(ReduceInventory command);
    OperationResult Reduce(List<ReduceInventory> command);
    List<InventoryOperationViewModel> GerOperationLog(long inventoryId);


}