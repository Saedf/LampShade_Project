using _02_LampShadeQuery.Contracts.Product;
using _02_LampShadeQuery.Contracts.Query;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Nancy.Json;
using ShopManagement.Application.Contract.Order;
using System.Net;
using Microsoft.AspNetCore.Authorization;

namespace ServiceHost.Pages
{
    [Authorize]
    public class CartModel : PageModel
    {
        public List<CartItem> CartItems;
        public const string CookieName = "cart-items";
        private readonly IProductQuery _productQuery;

        public CartModel(IProductQuery productQuery)
        {
            CartItems = new List<CartItem>();
            _productQuery = productQuery;
        }

        public void OnGet()
        {
            var serializer = new JavaScriptSerializer();
            var value = Request.Cookies[CookieName];
            var cartitems=  serializer.Deserialize<List<CartItem>>(value);
            if (cartitems != null)
            {
                foreach (var cartItem in cartitems)
                {
                    cartItem.CalculateTotalPrice();
                }

                CartItems = _productQuery.CheckInventoryStatus(cartitems);
            }

        }

        public IActionResult OnGetRemoveFromCart(long id)
        {
           Response.Cookies.Delete(CookieName);
            var serializer = new JavaScriptSerializer();
            var value = Request.Cookies[CookieName];
            var cartItems = serializer.Deserialize<List<CartItem>>(value);
            var itemToRemove = cartItems.FirstOrDefault(x => x.Id == id);
            if (itemToRemove != null) cartItems.Remove(itemToRemove);
            var options = new CookieOptions { Expires = DateTime.Now.AddDays(2) };
            Response.Cookies.Append(CookieName, serializer.Serialize(cartItems), options);
            // var value2 = Request.Cookies[CookieName];
            //CartItems = serializer.Deserialize<List<CartItem>>(value2);
            return RedirectToPage("/Cart");
        }

        public IActionResult OnGetGoToCheckOut()
        {
            var serializer = new JavaScriptSerializer();
            var value = Request.Cookies[CookieName];
            var cartItems = serializer.Deserialize<List<CartItem>>(value);
            foreach (var item in cartItems)
            {
                item.CalculateTotalPrice();
            }

            CartItems = _productQuery.CheckInventoryStatus(cartItems);
            return RedirectToPage(CartItems.Any(x => !x.IsInStock)?"/Cart":"/CheckOut");
        }
    }
}
