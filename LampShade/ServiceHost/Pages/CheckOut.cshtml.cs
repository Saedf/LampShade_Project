using _02_LampShadeQuery.Contracts;
using _02_LampShadeQuery.Contracts.Product;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Nancy.Json;
using ShopManagement.Application.Contract.Order;

namespace ServiceHost.Pages
{
    public class CheckOutModel : PageModel
    {
        public Cart Cart;
        public const string CookieName = "cart-items";
        private readonly ICartCalculatorService _cartCalculatorService;
        private readonly ICartService _cartService;
        private readonly IProductQuery _productQuery;
        public CheckOutModel( ICartCalculatorService cartCalculatorService,
            ICartService cartService, IProductQuery productQuery)
        {
            Cart = new Cart();
            _cartCalculatorService = cartCalculatorService;
            _cartService = cartService;
            _productQuery = productQuery;
        }

        public void OnGet()
        {
            var serializer = new JavaScriptSerializer();
            var value = Request.Cookies[CookieName];
            var cartitems = serializer.Deserialize<List<CartItem>>(value);
            foreach (var cartItem in cartitems)
            {
                cartItem.CalculateTotalPrice();
            }

            Cart = _cartCalculatorService.ComputeCart(cartitems);
            _cartService.Set(Cart);
        }

        public IActionResult OnPostPay()
        {
            var cart = _cartService.Get();
            var result = _productQuery.CheckInventoryStatus(cart.CartItems);
            if (result.Any(x=>!x.IsInStock))
            {
                return RedirectToPage("/Cart");
            }
            return RedirectToPage("/CheckOut");
        }
    }
}
