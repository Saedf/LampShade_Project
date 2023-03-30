using _01_Framework.Domain;

namespace ShopManagement.Domain.OrderAgg;

public class OrderItem : EntityBase
{
    public long ProductId { get; private set; }
    public int Count { get; set; }
    public decimal UnitPrice { get; private set; }
    public int DiscountRate { get; private set; }
    public long OrderId { get; private set; }
    public Order Order { get; private set; }

    public OrderItem(long productId, int count, decimal unitPrice, int discountRate)
    {
        ProductId = productId;
        Count = count;
        UnitPrice = unitPrice;
        DiscountRate = discountRate;
    }
}