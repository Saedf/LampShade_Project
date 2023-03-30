using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using _01_Framework.Application;
using _01_Framework.Infrastructure;
using _02_LampShadeQuery.Contracts;
using DiscountManagement.Infrastructure.EfCore;
using ShopManagement.Application.Contract.Order;

namespace _02_LampShadeQuery.Query
{
    public class CartCalculateService:ICartCalculatorService
    {
        private readonly IAuthHelper _authHelper;
        private readonly DiscountContext _discountContext;

        public CartCalculateService(IAuthHelper authHelper, DiscountContext discountContext)
        {
            _authHelper = authHelper;
            _discountContext = discountContext;
        }

        public Cart ComputeCart(List<CartItem> cartItems)
        {
            var cart=new Cart();
            var colleagueDiscounts = _discountContext.ColleagueDiscounts
                .Where(x => !x.IsRemoved)
                .Select(x =>new {x.DiscountRate,x.ProductId})
                .ToList();

            var customerDiscounts = _discountContext.CustomerDiscounts
                .Where(x => x.StartDate < DateTime.Now && x.EndDate > DateTime.Now)
                .Select(x => new { x.ProductId, x.DiscountRate })
                .ToList();
            var currentAccountRole = _authHelper.CurrentAccountRole();
            foreach (var cartItem in cartItems)
            {
                if (currentAccountRole==Roles.ColleagueUser)
                {
                    var colleagueDiscount = colleagueDiscounts.FirstOrDefault(
                        x => x.ProductId == cartItem.Id);
                    if (colleagueDiscount!=null)
                    {
                        cartItem.DiscountRate = colleagueDiscount.DiscountRate;
                    }
                }
                else
                {
                    var customerDiscount = customerDiscounts
                        .FirstOrDefault(x => x.ProductId == cartItem.Id);
                    if (customerDiscount!=null) { cartItem.DiscountRate= customerDiscount.DiscountRate; }
                }

                cartItem.DiscountAmount = ((cartItem.TotalItemPrice * cartItem.DiscountRate) / 100);
                cartItem.ItemPayAmount = cartItem.TotalItemPrice - cartItem.DiscountAmount;
                cart.Add(cartItem);
            }

            return cart;
        }
        
    }
}
