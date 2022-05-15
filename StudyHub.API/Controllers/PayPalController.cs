

using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using StudyHub.Application.Settings;
using StudyHub.Payment.DomainServices;
using StudyHub.Payment.DomainServices.PayPal;
using AutoMapper;
using AutoMapper.Configuration;
using MediatR;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using StudyHub.API.Helpers;
using StudyHub.Application.CQRS.StudentCQRS.Command;
using StudyHub.Application.CQRS.StudentCQRS.Query;
using StudyHub.Application.Cryptography;
using StudyHub.Application.DTOs.Common;
using StudyHub.Application.DTOs.Student;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using StudyHub.Payment.Interfaces;
using System.Text;
using System.Net.Http;
using StudyHub.Payment.Models;
using System.Configuration;
using System.Net.Http.Headers;
using StudyHub.Payment.Helper;
using Swashbuckle.AspNetCore.Annotations;
using System.Net;
using PayPal.Api;

namespace StudyHub.API.Controllers
{
	//[System.Web.Http.Route("api/[controller]")]
	[ApiController]
	public class PayPalController : BaseController
	{
		private readonly PayPalSettings _payPalSettings;
		private PayPalResponseModel userCrdential { get; set; }
		public string Token { get; set; }
		private readonly IPaypalServices paypalServices;
		private readonly HttpRequestMessage httpRequest;

		public PayPalController(IOptions<PayPalSettings> payPalSettings, IPaypalServices paypalServices,IMediator mediator, IMapper mapper, IWebHostEnvironment hostEnvironment, ICryptographyService cryptographyServices) : base(mediator, mapper, hostEnvironment, cryptographyServices)
		{
			_payPalSettings = payPalSettings.Value;
			this.paypalServices = paypalServices;
		}


		//var suppliers = allAmounts.Select(x => x.SupplierId).Distinct();
		//var invoices = allAmounts.Select(x => x.InvoiceNumber).Distinct();

		//var result = new List<ProcurementBudgetExpensesBySupplierViewModel>();

		//var bugdgets = suppliers.Zip(invoices, (s, i) => new ProcurementBudgetExpensesGroupedData { SupplierId = s, InvoiceNumber = i });


		[HttpPost("[action]")]
		[SwaggerOperation("Creates a payment order")]
		[SwaggerResponse(200, "The payment was found", typeof(OrderModel))]
		//[SwaggerResponse(400, "The request data is invalid", typeof(OrderModel))]
		public async Task<IActionResult> CreatePaymentOrder([System.Web.Http.FromBody] OrderModel requestModel, CancellationToken cancellationToken)
		{
			if (cryptographyServices.GetAccessToken(out var token))
			{

				var baseUrl = _payPalSettings.BaseAuthUrl;
				var grant_type = _payPalSettings.GrantType;
				var client_credentials = _payPalSettings.ClientCredential;
				var clientId = _payPalSettings.ClientId;
				var clientSecret = _payPalSettings.ClientScrete;
				var coonectionstring = _payPalSettings.ConnectionString;
				Uri baseUri = new Uri(baseUrl + "/v2/checkout/orders");

				using (var client = new HttpClient())
				using (var request = new HttpRequestMessage(HttpMethod.Post, baseUri))
				{
		
					var json = JsonConvert.SerializeObject(requestModel);
					using (var stringContent = new StringContent(json, Encoding.UTF8, "application/json"))
					{
						request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", token);
						request.Headers.Add("PayPal-Request-Id", Guid.NewGuid().ToString());
						request.Headers.Add("PayPal-Partner-Attribution-Id", Guid.NewGuid().ToString());
						request.Headers.Add("PayPal-Client-Metadata-Id", Guid.NewGuid().ToString());
						request.Headers.Add("Prefer", "return=minimal");

						request.Content = stringContent;

						using (var response = await client.SendAsync(request))
						//.SendAsync(request, HttpCompletionOption.ResponseHeadersRead, cancellationToken)
						//.ConfigureAwait(false))
						{
							var readResponse = response.Content.ReadAsStringAsync();
							response.EnsureSuccessStatusCode();
							return Ok();
						}
					}
				}


				return Ok();
			}
			return Unauthorized();
		}


	  
		[HttpGet("[action]")]
		[SwaggerOperation("Retrieve the details of a previously made payment")]
		[SwaggerResponse(200, "The payment was found", typeof(PayPalResponseModel))]
		[SwaggerResponse(400, "The request data is invalid", typeof(PayPalResponseModel))]
		public IActionResult MakePayment()
		{
			if (cryptographyServices.GetAccessToken(out var token))
			{

				var baseUrl = _payPalSettings.BaseAuthUrl;
				var grant_type = _payPalSettings.GrantType;
				var client_credentials = _payPalSettings.ClientCredential;
				var clientId = _payPalSettings.ClientId;
				var clientSecret = _payPalSettings.ClientScrete;
				var coonectionstring = _payPalSettings.ConnectionString;
				Uri baseUri = new Uri("https://api.sandbox.paypal.com/v1/payments/payment/PAYID-MILJHTQ3RK41545EG194802V/execute");


				using (var client = new HttpClient())
				using (var request = new HttpRequestMessage(HttpMethod.Get, baseUri))
				{
					request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", token);
					request.Headers.Add("PayPal-Request-Id", Guid.NewGuid().ToString());
					request.Headers.Add("PayPal-Partner-Attribution-Id", Guid.NewGuid().ToString());
					request.Headers.Add("PayPal-Client-Metadata-Id", Guid.NewGuid().ToString());
					request.Headers.Add("Prefer", "return=minimal");

					request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", token);
					ServicePointManager.Expect100Continue = true;
					ServicePointManager.ServerCertificateValidationCallback +=
					(sender, certificate, chain, sslPolicyErrors) => true;
					ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;
					using (var response = client.SendAsync(request).Result)
					{

						response.EnsureSuccessStatusCode();
						var readResponse = response.Content.ReadAsStringAsync().Result;
						var stringg =  JsonConvert.DeserializeObject<string>(readResponse);

					}
				}


				return Ok();
			}
			return Unauthorized();
		}



		[HttpGet("[action]")]
		[ProducesResponseType(204)]
		[ProducesResponseType(201)]
		[ProducesResponseType(StatusCodes.Status404NotFound)]
		[ProducesResponseType(StatusCodes.Status500InternalServerError)]
		[ProducesDefaultResponseType]
	  
		public dynamic Order()
		{
			


			var token = cryptographyServices.GetAccessToken(out var accesstoken);
			var baseUrl = _payPalSettings.BaseAuthUrl;
			Uri url= new Uri(baseUrl + "/v1/payments/payment");
			System.Net.Http.HttpRequestMessage request = new System.Net.Http.HttpRequestMessage(System.Net.Http.HttpMethod.Post, url);
			request.Headers.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", accesstoken);
			var returnurl = "http://localhost:44308/api/PayPal/Order";
			var payment = JObject.FromObject(new
			{
				intent = "sale",
				redirect_urls = new
				{
					return_url = "http://example.com/your_cancel_url.html",
					cancel_url = "http://example.com/your_cancel_url.html"
				},
				payer = new { payment_method = "paypal" },
				transactions = JArray.FromObject(new[]
				{
					new{
						amount = new
						{
							 total = 7.47,
							  currency = "USD"
						}
					}
				})
			 });

			request.Content = new System.Net.Http.StringContent(JsonConvert.SerializeObject(payment), Encoding.UTF8, "application/json");

			//href = "https://api.sandbox.paypal.com/v1/payments/payment/PAYID-MILJHTQ3RK41545EG194802V/execute"
			var configuration = new System.Web.Http.HttpConfiguration();
			request.Properties[System.Web.Http.Hosting.HttpPropertyKeys.HttpConfigurationKey] = configuration;



			try
			{
				using (var client = new HttpClient())
				{
					ServicePointManager.Expect100Continue = true;
					ServicePointManager.ServerCertificateValidationCallback +=
					(sender, certificate, chain, sslPolicyErrors) => true;
					ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;
					var task = client.SendAsync(request);
					var resp = task.Result;
					HttpStatusCode status = resp.StatusCode;


				   //var ssss =  paypalServices.GenerateInvoiceNumber(accesstoken);
				   if(resp.IsSuccessStatusCode)
					{
						string content = resp.Content.ReadAsStringAsync().Result;
						PayPalPaymentCreatedResponse paypalResponse = JsonConvert.DeserializeObject<PayPalPaymentCreatedResponse>(content);
						paypalServices.OrderResponse(paypalResponse);
						//var order = new PaymentOrder()
						//{
						//    RefNumber = paypalPaymentCreated.id,
						//}

						return Ok(paypalResponse);
					}
					var error = resp.Content.ReadAsAsync<System.Web.Http.HttpError>().Result;
				 
					return request.CreateErrorResponse(status, error.Message);

				}
			}
			catch (Exception ex)
			{
				//new ErrorLog("ValidateCardWithOTP ERROR OCCURRED TREATED RECORD REFID: " + ex);
				return request.CreateErrorResponse(HttpStatusCode.InternalServerError, ex.Message);

			}

		   

		}



		[HttpGet("[action]")]
		
		public async Task<IActionResult> PayRedirect(string paymentId, string token, string PayerID)
        {

			//paymentId = "PAYID-MINJL3I5HF11409PD2517438";
			//PayerID = "DTQS3UWAWLD2Y";

			cryptographyServices.GetAccessToken(out var accessToken);
   //         if (string.IsNullOrEmpty(accessToken))
   //             return Unauthorized();

			var apiContext = new APIContext(accessToken);
			apiContext.Config = ConfigManager.Instance.GetProperties();

			var paymentEx = new PaymentExecution() { payer_id = PayerID };
			var payment = new PayPal.Api.Payment() { id = paymentId };

			var exceutedPayment = await Task.Run(()=> payment.Execute(apiContext, paymentEx)) ;


			return Ok(exceutedPayment);

			//var ssass = "http://example.com/your_cancel_url.html?paymentId=PAYID-MINJL3I5HF11409PD2517438&token=EC-7NF159477P9168334&PayerID=DTQS3UWAWLD2Y";
   //         var baseUrl = _payPalSettings.BaseAuthUrl;
   //         Uri baseUri = new Uri(baseUrl + "/v2/checkout/orders/");

   //         using (var client = new HttpClient())
   //         using (var request = new HttpRequestMessage(HttpMethod.Get, baseUri))
   //         {
   //             request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);
   //             ServicePointManager.Expect100Continue = true;
   //             ServicePointManager.ServerCertificateValidationCallback +=
   //             (sender, certificate, chain, sslPolicyErrors) => true;
   //             ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;
   //             using (var response = client.SendAsync(request).Result)
   //             {

   //                 HttpStatusCode status = response.StatusCode;
   //                 if (response.IsSuccessStatusCode)
   //                 {
   //                     var readResponse = response.Content.ReadAsStringAsync().Result;
   //                     var details = JsonConvert.DeserializeObject<PayPalPaymentCreatedResponse>(readResponse);

   //                     return request.CreateResponse(details);
   //                 }
   //                 var error = response.Content.ReadAsAsync<System.Web.Http.HttpError>().Result;
   //                 return request.CreateErrorResponse(status, error.Message);

   //             }
   //         }




        }

        [HttpGet("[action]")]
		[ActionName("OrderDetails")]
		[SwaggerOperation("Retrieve the details of a previously made payment")]
		[SwaggerResponse(200, "The payment was found", typeof(PayPalResponseModel))]
		[SwaggerResponse(400, "The request data is invalid", typeof(PayPalResponseModel))]
		public dynamic GetOrderDetails()
		{
			string orderId = "sssssss";
			if (string.IsNullOrEmpty(orderId))
				return BadRequest();

			cryptographyServices.GetAccessToken(out var token);
			if (string.IsNullOrEmpty(token))
				return Unauthorized();

			var urle = "https://api.sandbox.paypal.com/v1/payments/payment/PAYID-MINJ72Q5NT43052AH4893136";
			var baseUrl = _payPalSettings.BaseAuthUrl;
			Uri baseUri = new Uri(baseUrl + "/v2/checkout/orders/" + orderId);

			using (var client = new HttpClient())
			using (var request = new HttpRequestMessage(HttpMethod.Get, urle))
			{
				request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", token);
				ServicePointManager.Expect100Continue = true;
				ServicePointManager.ServerCertificateValidationCallback +=
				(sender, certificate, chain, sslPolicyErrors) => true;
				ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;
				using (var response = client.SendAsync(request).Result)
				{

					HttpStatusCode status = response.StatusCode;
					if (response.IsSuccessStatusCode)
					{
						var readResponse = response.Content.ReadAsStringAsync().Result;
						var details = JsonConvert.DeserializeObject<PayPalPaymentCreatedResponse>(readResponse);

						return request.CreateResponse(details);
					}
					var error = response.Content.ReadAsAsync<System.Web.Http.HttpError>().Result;
					return request.CreateErrorResponse(status, error.Message);

				}
			}




		}




	}
}
