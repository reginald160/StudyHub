using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using PayPal.Api;
using StudyHub.Application.Cryptography;
using StudyHub.Application.Settings;
using StudyHub.Payment.DomainServices;
using StudyHub.Payment.Models;
using Swashbuckle.AspNetCore.Annotations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace StudyHub.API.Controllers
{
    public class ApplicationHookController : BaseController
    {
		private readonly PayPalSettings _payPalSettings;
		public ApplicationHookController(IOptions<PayPalSettings> payPalSettings, IMediator mediator, IMapper mapper, IWebHostEnvironment hostEnvironment, ICryptographyService cryptographyServices) : base(mediator, mapper, hostEnvironment, cryptographyServices)
        {
			_payPalSettings = payPalSettings.Value;
		}

		[HttpGet("[action]")]
		[ActionName("OrderDetails")]
		[SwaggerOperation("Retrieve the details of a previously made payment")]
		[SwaggerResponse(200, "The payment was found", typeof(PayPalResponseModel))]
		[SwaggerResponse(400, "The request data is invalid", typeof(PayPalResponseModel))]
		public dynamic WebHooks()
		{
			string orderId = "sssssss";
			if (string.IsNullOrEmpty(orderId))
				return BadRequest();

			cryptographyServices.GetAccessToken(out var token);
			if (string.IsNullOrEmpty(token))
				return Unauthorized();

			var urle = "https://api.sandbox.paypal.com/v1/payments/payment/PAYID-MINJ72Q5NT43052AH4893136";
			var baseUrl = _payPalSettings.BaseAuthUrl;
			Uri baseUri = new Uri(baseUrl + "/v1/notifications/webhooks");

			var apiContext = new APIContext(token);
			apiContext.Config = ConfigManager.Instance.GetProperties();

			var payment = new PaymentExecution();



			var webhook = PayPal.Api.Webhook.GetAll(apiContext);

			apiContext.Config["connectionTimeout"] = "1000"; // Quick timeout for testing purposes

		


			using (var client = new HttpClient())
			using (var request = new HttpRequestMessage(HttpMethod.Get, urle))
			{
				request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", token);
				//ServicePointManager.Expect100Continue = true;
				//ServicePointManager.ServerCertificateValidationCallback +=
				//(sender, certificate, chain, sslPolicyErrors) => true;
				//ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;
				using (var response = client.SendAsync(request).Result)
				{

					HttpStatusCode status = response.StatusCode;
					if (response.IsSuccessStatusCode)
					{
						var readResponse = response.Content.ReadAsStringAsync().Result;
						var details = JsonConvert.DeserializeObject<JsonObjectAttribute>(readResponse);

						return request.CreateResponse(details);
					}
					var error = response.Content.ReadAsAsync<System.Web.Http.HttpError>().Result;
					return request.CreateErrorResponse(status, error.Message);

				}
			}




		}

	}
}
