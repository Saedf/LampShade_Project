

using Microsoft.Extensions.Configuration;
using RestSharp;
using JsonSerializer = RestSharp.Serialization.Json.JsonSerializer;


namespace _01_Framework.Application.ZarinPal
{
    public class ZarinPalFactory : IZarinPalFactory
    {
        private readonly IConfiguration _configuration;

        public string Prefix { get; set; }
        private string MerchantId { get;}

        public ZarinPalFactory(IConfiguration configuration)
        {
            _configuration = configuration;
            Prefix = _configuration.GetSection("payment")["method"];
            MerchantId= _configuration.GetSection("payment")["merchant"];
        }

        public PaymentResponse CreatePaymentRequest(string amount, string mobile, string email, string description,
             long orderId)
        {
            amount = amount.Replace(",", "");
            var finalAmount = int.Parse(amount);
            var siteUrl = _configuration.GetSection("payment")["siteUrl"];
            var client = new RestClient($"https://{Prefix}.zarinpal.com/pg/rest/WebGate/PaymentRequest.json");
          // var client = new RestClient($"https://{Prefix}.zarinpal.com/pg/v4/payment/request.json");
            var request = new RestRequest(Method.POST);

          //  request.AddHeader("accept", "application/json");
            request.AddHeader("Content-Type", "application/json");
            var body = new PaymentRequest
            {
                Mobile = mobile,
                callback_url = $"{siteUrl}/Checkout?handler=CallBack&oId={orderId}",
                description = description,
                Email = email,
                amount = finalAmount,
                merchant_id = MerchantId
            };
            request.AddJsonBody(body);
            var response = client.Execute(request);
            var jsonSerializer = new RestSharp.Serialization.Json.JsonSerializer();
            return jsonSerializer.Deserialize<PaymentResponse>(response);
           
        }

        public VerificationResponse CreateVerificationRequest(string authority, string amount)
        {
            // var client = new RestClient($"https://{Prefix}.zarinpal.com/pg/v4/payment/verify.json");
            var client = new RestClient($"https://{Prefix}.zarinpal.com/pg/rest/WebGate/PaymentVerification.json");
            var request = new RestRequest(Method.POST);
            request.AddHeader("Content-Type", "application/json");

            amount = amount.Replace(",", "");
            var finalAmount = int.Parse(amount);

            request.AddJsonBody(new VerificationRequest
            {
                Amount = finalAmount,
                MerchantID = MerchantId,
                Authority = authority
            });
            var response = client.Execute(request);
            var jsonSerializer = new JsonSerializer();
            return jsonSerializer.Deserialize<VerificationResponse>(response);
            //var verificationresponse = JsonConvert.DeserializeObject<VerificationResponse>(response.Content);
            //return verificationresponse;
        }
    }
}