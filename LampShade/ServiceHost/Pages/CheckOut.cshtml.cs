using System.Globalization;
using _01_Framework.Application.ZarinPal;
using _02_LampShadeQuery.Contracts;
using _02_LampShadeQuery.Contracts.Product;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Nancy.Json;
using ShopManagement.Application.Contract.Order;

namespace ServiceHost.Pages
{
    [Authorize]
    public class CheckOutModel : PageModel
    {
        public Cart Cart;
        public const string CookieName = "cart-items";
        private readonly ICartCalculatorService _cartCalculatorService;
        private readonly ICartService _cartService;
        private readonly IProductQuery _productQuery;
        private readonly IOrderApplication _orderApplication;
        private readonly IZarinPalFactory _zarinPalFactory;
        public CheckOutModel( ICartCalculatorService cartCalculatorService,
            ICartService cartService, IProductQuery productQuery, IOrderApplication orderApplication, IZarinPalFactory zarinPalFactory)
        {
            Cart = new Cart();
            _cartCalculatorService = cartCalculatorService;
            _cartService = cartService;
            _productQuery = productQuery;
            _orderApplication = orderApplication;
            _zarinPalFactory = zarinPalFactory;
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

        public IActionResult OnPostPay(int paymentMethod)
        {
            var cart = _cartService.Get();
            cart.SetPaymentMethod(paymentMethod);
            var result = _productQuery.CheckInventoryStatus(cart.CartItems);
            
            if (result.Any(x=>!x.IsInStock))
            {
                return RedirectToPage("/Cart");
            }

            var orderId = _orderApplication.PlaceOrder(cart);
            if (paymentMethod==1)
            {
                var paymentResponse = _zarinPalFactory.CreatePaymentRequest(
                    cart.PayAmount.ToString(CultureInfo.InvariantCulture), "", "",
                    "خرید از لوازم خانگی", orderId);
                return Redirect(
                    $"https://{_zarinPalFactory.Prefix}.zarinpal.com/pg/StartPay/{paymentResponse.Authority}");
            }

            var paymentResult = new PaymentResult();
            return RedirectToPage("/PaymentResult",
                paymentResult.Succeeded("سفارش شما با موفقیت ثبت شد",null));

        }

        public IActionResult OnGetCallBack([FromQuery] string authority,
            [FromQuery] string status,[FromQuery]long oId)
        {
            var orderAmount = _orderApplication.GetAmountBy(oId);
            var verificationResponse =
                _zarinPalFactory.CreateVerificationRequest(authority, 
                    orderAmount.ToString(CultureInfo.InvariantCulture));
            var result = new PaymentResult();
            if (status=="OK" && verificationResponse.Status>=100)
            {
                var issueTrackingNo = _orderApplication
                    .PaymentSucceeded(oId, verificationResponse.RefID);
                Response.Cookies.Delete("cart-items");
                result = result.Succeeded("پرداخت با موفقیت انجام شد",issueTrackingNo);
                return RedirectToPage("/PaymentResult", result);
            }

            result = result.Failed("پرداخت با موفقیت انجام نشد"+
                                   "در صورت کسر وجه از حساب، مبلغ تا 24 ساعت دیگر به حساب شما بازگردانده خواهد شد");
            return RedirectToPage("/PaymentResult", result);
        }
    }
}
