using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using _01_Framework.Infrastructure;
using Microsoft.EntityFrameworkCore;
using ShopManagement.Domain.OrderAgg;

namespace ShopManagement.Infrastructure.EfCore.Repository
{
    public class OrderRepository:RepositoryBase<long,Order>,IOrderRepository
    {
        private readonly ShopContext _shopContext;
        public OrderRepository(ShopContext context) : base(context)
        {
            _shopContext = context;
        }

        public decimal GetAmountBy(long id)
        {
            var order = _shopContext.Orders.
                Select(x=>new {x.PayAmount,x.Id})
                .FirstOrDefault(o => o.Id == id);
            return order?.PayAmount ?? 0;
        }
    }
}
