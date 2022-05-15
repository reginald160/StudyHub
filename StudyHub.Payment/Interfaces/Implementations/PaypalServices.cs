using Newtonsoft.Json;
using StudyHub.Payment.DomainServices;
using StudyHub.Payment.Helper;
using StudyHub.Payment.Models;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace StudyHub.Payment.Interfaces.Implementations
{
    public class PaypalServices : IPaypalServices
    {
        private string baseUrl = PaymentGatewayConstants.PayPalBaseUrl;
        private readonly IPaymentRepository paymentRepository;

        public PaypalServices(IPaymentRepository paymentRepository)
        {
            this.paymentRepository = paymentRepository;
        }

        public  string GenerateInvoiceNumber(string token)
        {
         
            Uri baseUri = new Uri(baseUrl + "/v2/invoicing/generate-next-invoice-number");
            using (var client = new HttpClient())
            using (var request = new HttpRequestMessage(HttpMethod.Post, baseUri))
            {
                request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", token);
                ServicePointManager.Expect100Continue = true;
                ServicePointManager.ServerCertificateValidationCallback +=
                (sender, certificate, chain, sslPolicyErrors) => true;
                ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;
                using (var response = client.SendAsync(request).Result)              
                {
                             
                    response.EnsureSuccessStatusCode();
                    var readResponse = response.Content.ReadAsStringAsync().Result;
                    return JsonConvert.DeserializeObject<string>(readResponse);

                }

            }   
        }

        public bool OrderResponse(PayPalPaymentCreatedResponse order)
        {
            var redirectUrl = "";
            foreach (var item in order.links)
            {
                if (item.method == "REDIRECT")
                    redirectUrl = item.href;

            }
            var model = new PaymentOrder()
            {
                RefNumber = order.id,
                CreatedDate = DateTime.Now,
                OrderStatus = Domain.Enum.DomainEnum.OrderStatus.Initiated,
                IsNewOrder = false,
                RedirectionUrl = redirectUrl
            };
            return paymentRepository.UpsetOrder(model);
        }
        
    }
}
