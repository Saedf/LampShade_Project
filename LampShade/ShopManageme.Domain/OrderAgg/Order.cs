using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using _01_Framework.Domain;

namespace ShopManagement.Domain.OrderAgg
{
    public class Order:EntityBase
    {
        public long AccountId { get; private set; }
        public int PaymentMethod { get; private set; }
        public decimal TotalAmount { get; private set; }
        public decimal DiscountAmount { get; private set; }
        public decimal PayAmount { get; private set; }
        public bool IsPayed { get; private set; }
        public bool IsCanceled { get; private set; }
        public string IssueTrackingNo { get; private set; }
        public long RefId { get; private set; }
        public List<OrderItem> OrderItems { get; private set; }

        public Order(long accountId, int paymentMethod, decimal totalAmount, decimal discountAmount, decimal payAmount)
        {
            AccountId = accountId;
            PaymentMethod = paymentMethod;
            TotalAmount = totalAmount;
            DiscountAmount = discountAmount;
            PayAmount = payAmount;
            IsCanceled = false;
            IsPayed = false;
            RefId = 0;
            OrderItems = new List<OrderItem>();
        }

        public void PaymentSucceeded(long refid)
        {
            IsPayed = true;
            if (refid!=0)
            {
                RefId=refid;
            }
        }

        public void Cancel()
        {
            IsCanceled = true;
        }

        public void SetIssueTrackingNo(string number)
        {
            IssueTrackingNo=number;
        }

        public void AddItem(OrderItem item)
        {
            OrderItems.Add(item);
        }
    }
}
