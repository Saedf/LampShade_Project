using ShopManagement.Application.Contract.Product;

namespace InventoryManagement.Application.Contract.Inventory;

public class CreateInventory
{
    public long ProductId { get; set; }
    public decimal UnitPrice { get; set; }
    public List<ProductViewModel> Products { get; set; }


}