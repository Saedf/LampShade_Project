namespace _01_Framework.Application.ZarinPal
{
    public class PaymentRequest
    {
        public string Mobile { get; set; }
        public string Email { get; set; }
        public string callback_url { get; set; }
        public string description { get; set; }
        public int amount { get; set; }
        public string merchant_id { get; set; }

        //public PaymentRequest(string callbackUrl, string description, int amount, string merchantId)
        //{
        //    CallbackURL = callbackUrl;
        //    Description = description;
        //    Amount = amount;
        //    MerchantID = merchantId;
        //}
    }
}
