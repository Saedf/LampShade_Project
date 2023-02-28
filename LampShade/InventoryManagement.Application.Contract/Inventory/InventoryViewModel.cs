namespace InventoryManagement.Application.Contract.Inventory;

public class InventoryViewModel
{
    public long Id { get; set; }
    public string Product { get; set; }
    public long ProductId { get; set; }
    public decimal UnitPrice { get; set; }
    public bool InStock { get; set; }
    public long CurrentCount { get; set; }
    public string CreationDate { get; set; }

}