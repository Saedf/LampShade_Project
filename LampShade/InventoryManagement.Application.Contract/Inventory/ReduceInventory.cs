using _01_Framework.Application;
using System.ComponentModel.DataAnnotations;

namespace InventoryManagement.Application.Contract.Inventory;

public class ReduceInventory
{
    [Range(1, 1000, ErrorMessage = ValidationMessages.IsRequired)]
    public long InventoryId { get; set; }
    public long ProductId { get; set; }
    [Range(1, 1000, ErrorMessage = ValidationMessages.IsRequired)]
    public long Count { get; set; }
    [Required(ErrorMessage = ValidationMessages.IsRequired)]
    public string Description { get; set; }
    public long OrderId { get; set; }

    public ReduceInventory()
    {
            
    }

    public ReduceInventory(long productId, long count, string description, long orderId)
    {
        ProductId = productId;
        Count = count;
        Description = description;
        OrderId = orderId;
    }
}