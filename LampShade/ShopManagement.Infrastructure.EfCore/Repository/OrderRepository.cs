using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using _01_Framework.Application;
using _01_Framework.Infrastructure;
using AccountManagement.Infrastructure.EfCore;
using Microsoft.EntityFrameworkCore;
using ShopManagement.Application.Contract;
using ShopManagement.Application.Contract.Order;
using ShopManagement.Domain.OrderAgg;

namespace ShopManagement.Infrastructure.EfCore.Repository
{
    public class OrderRepository:RepositoryBase<long,Order>,IOrderRepository
    {
        private readonly ShopContext _shopContext;
        private readonly AccountContext _accountContext;
        public OrderRepository(ShopContext context,
            AccountContext accountContext) : base(context)
        {
            _shopContext = context;
            _accountContext = accountContext;
        }

        public decimal GetAmountBy(long id)
        {
            var order = _shopContext.Orders.
                Select(x=>new {x.PayAmount,x.Id})
                .FirstOrDefault(o => o.Id == id);
            return order?.PayAmount ?? 0;
        }

        public List<OrderViewModel> Search(OrderSearchModel searchModel)
        {
            var accounts = _accountContext.Accounts
                .Select(x => new { x.Id, x.FullName })
                .ToList();
            var query =
                _shopContext.Orders.Select(x => new OrderViewModel
                {
                    Id = x.Id,
                    AccountId = x.AccountId,
                    CreationDate = x.CreationDate.ToFarsi(),
                    IsCanceled = x.IsCanceled,
                    DiscountAmount = x.DiscountAmount,
                    IssueTrackingNo = x.IssueTrackingNo,
                    PayAmount = x.PayAmount,
                    PaymentMethodId = x.PaymentMethod,
                    RefId = x.RefId,
                    IsPayed = x.IsPayed,
                    TotalAmount = x.TotalAmount
                });
            query = query.Where(x => x.IsCanceled == searchModel.IsCanceled);
            if (searchModel.AccountId>0)
            {
                query = query.Where(x => x.AccountId == searchModel.AccountId);
            }

            var orders = query.OrderByDescending(x => x.Id).ToList();
            foreach (var order in orders)
            {
                order.AccountFullName =
                    accounts.FirstOrDefault(x => x.Id == order.AccountId)?.FullName;
                order.PaymentMethod =
                    PaymentMethod.GetBy(order.PaymentMethodId).Name;
            }

            return orders;
        }

        public List<OrderItemViewModel> GetItems(long orderId)
        {
            var products = _shopContext.Products.Select(x => new { x.Id, x.Name }).ToList();
            var order = _shopContext.Orders.FirstOrDefault(x => x.Id == orderId);
            if (order == null)
            {
                return new List<OrderItemViewModel>();
            }

            var items = order.OrderItems.
                Select(x => new OrderItemViewModel
                {
                    Count = x.Count,
                    DiscountRate = x.DiscountRate,
                    Id = x.Id,
                    UnitPrice = x.UnitPrice,
                    OrderId = x.OrderId,
                    ProductId = x.ProductId
                }).ToList();
            foreach (var item in items)
            {
                item.Product = products.FirstOrDefault(x => x.Id == item.ProductId)?.Name;
            }
            return items;

        }
    }
}
