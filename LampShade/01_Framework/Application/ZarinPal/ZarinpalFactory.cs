

using Microsoft.Extensions.Configuration;
using RestSharp;
using RestSharp.Serializers.Json;
using System.IO;
using Newtonsoft.Json;
using System.Security.Policy;
using Nancy.Json;


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
            //URLs url = new URLs(this.IsSandBox);
            //_HttpCore.URL = url.GetPaymentRequestURL();
            //_HttpCore.Method = Method.POST;
            //_HttpCore.Raw = PaymentRequest;
            //this.PaymentRequest = PaymentRequest;
            //String response = _HttpCore.Get();

            //JavaScriptSerializer j = new JavaScriptSerializer();
            //PaymentResponse _Response = j.Deserialize<PaymentResponse>(response);
            //_Response.PaymentURL = url.GetPaymenGatewayURL(_Response.Authority);

            //return _Response;
            amount = amount.Replace(",", "");
            var finalAmount = int.Parse(amount);
            var siteUrl = _configuration.GetSection("payment")["siteUrl"];

            var client = new RestClient($"https://{Prefix}.zarinpal.com/pg/rest/WebGate/PaymentRequest.json");
            var request = new RestRequest(Method.Post.ToString());
            request.AddHeader("Content-Type", "application/json");
            var body = new PaymentRequest
            {
                Mobile = mobile,
                CallbackURL = $"{siteUrl}/Checkout?handler=CallBack&oId={orderId}",
                Description = description,
                Email = email,
                Amount = finalAmount,
                MerchantID = MerchantId
            };
            request.AddJsonBody(body);
            var response = client.Execute(request);
            JavaScriptSerializer jsonSerializer = new JavaScriptSerializer();
            //var jsonSerializer = new JsonSerializer();
            return jsonSerializer.Deserialize<PaymentResponse>(response.Content);
            //var paymentresponse = JsonConvert.DeserializeObject<PaymentResponse>(response.Content);
            //return paymentresponse;
        }

        public VerificationResponse CreateVerificationRequest(string authority, string amount)
        {
            var client = new RestClient($"https://{Prefix}.zarinpal.com/pg/rest/WebGate/PaymentVerification.json");
            var request = new RestRequest(Method.Post.ToString());
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
            //var jsonSerializer = new JsonSerializer();
           
            //return jsonSerializer.Deserialize<VerificationResponse>(response);
            var verificationresponse = JsonConvert.DeserializeObject<VerificationResponse>(response.Content);
            return verificationresponse;
        }
    }
}