using System.ComponentModel.DataAnnotations;
using _01_Framework.Application;
using ShopManagement.Application.Contract.Product;

namespace InventoryManagement.Application.Contract.Inventory;

public class CreateInventory
{
    [Range(1,10000,ErrorMessage = ValidationMessages.IsRequired)]
    public long ProductId { get; set; }
    [Required(ErrorMessage = ValidationMessages.IsRequired)]
    [Range(1,10000000,ErrorMessage = ValidationMessages.IsBetweenValue)]
    public decimal UnitPrice { get; set; }
    public List<ProductViewModel> Products { get; set; }


}