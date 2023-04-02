namespace ShopManagement.Application.Contract.Order;

public class OrderViewModel
{
    public long Id { get; set; }
    public long AccountId { get;  set; }
    public string AccountFullName { get; set; }
    public int PaymentMethodId { get;  set; }
    public string PaymentMethod { get;  set; }
    public decimal TotalAmount { get;  set; }
    public decimal DiscountAmount { get;  set; }
    public decimal PayAmount { get;  set; }
    public bool IsPayed { get;  set; }
    public bool IsCanceled { get;  set; }
    public string IssueTrackingNo { get;  set; }
    public long RefId { get;  set; }
    public string CreationDate { get; set; }
}