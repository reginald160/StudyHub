//using AutoMapper;
//using MediatR;
//using Microsoft.AspNetCore.Authorization;
//using Microsoft.AspNetCore.Hosting;
//using Microsoft.AspNetCore.Http;
//using Microsoft.AspNetCore.Mvc;
//using Microsoft.Extensions.Logging;
//using StudyHub.Application.Cryptography;
//using StudyHub.Payment.DomainServices;
//using StudyHub.Payment.DTO;
//using StudyHub.Payment.Helper;
//using StudyHub.Payment.Interfaces;
//using Swashbuckle.AspNetCore.Annotations;
//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;

//namespace StudyHub.API.Controllers
//{
//    [Route("api/[controller]")]
//    [ApiController]
//    [AllowAnonymous]
//    public class PaymentsController : BaseController
//    {
       
//        private readonly ILogger<PaymentsController> _logger;
//        private readonly IPaymentRepository _paymentRepository;
//        private readonly IMerchantRepository _merchantRepository;
//        private readonly IBankConnectorDomainService _bankConnectorDS;

//        public PaymentsController(IMediator mediator, IMapper mapper, IWebHostEnvironment hostEnvironment, ICryptographyService cryptographyServices, ILogger<PaymentsController> logger, IPaymentRepository paymentRepository, IMerchantRepository merchantRepository, IBankConnectorDomainService bankConnectorDS) : base(mediator, mapper, hostEnvironment, cryptographyServices)
//        {
//            _logger = logger;
//            _paymentRepository = paymentRepository;
//            _merchantRepository = merchantRepository;
//            _bankConnectorDS = bankConnectorDS;
//        }

//        [HttpGet("{paymentId}", Name ="GetPayment") ]
//        [SwaggerOperation("Retrieve the details of a previously made payment")]
//        [SwaggerResponse(200, "The payment was found", typeof(GetPaymentResponseModel))]
//        [SwaggerResponse(400, "The request data is invalid", typeof(ErrorResponseModel))]
//        public async Task<ActionResult> Get([FromRoute] Guid paymentId)
//        {
//            if (!EnsureMerchantValidity(out var merchant))
//                return BadRequest(new ErrorResponseModel("missing or invalid merchant header"));

//            _logger.LogInformation(paymentId.ToString());

//            var payment = await _paymentRepository.GetByPaymentIdAsync(paymentId);

//            if (payment == null)
//                return NotFound();

//            // Protect against access of payment data from other merchants
//            if (payment.MerchantId != merchant.MerchantId)
//                return BadRequest(new ErrorResponseModel("Merchant mismatch"));

//            var responseModel = new GetPaymentResponseModel
//            {
//                Payment = new ModelMapper().MapPayment(payment)
//            };

//            return Ok(responseModel);
//        }

//        [HttpPost("MakePayment")]
//        [SwaggerOperation("Process a payment through the payment gateway and receive either a successful or unsuccessful response")]
//        [SwaggerResponse(202, "The payment was created", typeof(CreatePaymentResponseModel))]
//        [SwaggerResponse(400, "The request data is invalid", typeof(ErrorResponseModel))]
//        public async Task<ActionResult> MakePayment([FromBody] CreatePaymentRequestModel requestModel)
//        {
//            if (!EnsureMerchantValidity(out var merchant))
//                return BadRequest(new ErrorResponseModel("missing or invalid merchant header"));

//            var creditCardInformation = new CardInformation(requestModel.CreditCardNumber,
//                new ExpiryDate(requestModel.CreditCardExpiryMonth, requestModel.CreditCardExpiryYear),
//                requestModel.CreditCardCcv);

//            var paymentAmount = new PaymentAmount(requestModel.Amount, requestModel.CurrencyCode);
//            var payment = PaymentModel.Create(paymentAmount, creditCardInformation, merchant.MerchantId, requestModel.ExternalShopperIdentifier);

//            bool wasPaymentSuccessful = await payment.AttemptPayment(merchant.CreditCardInformation, _bankConnectorDS);

//            _paymentRepository.Add(payment);
//            await _paymentRepository.PersistAsync();

//            _logger.LogInformation($"New payment was created with PaymentId: {payment.PaymentId}");

//            var responseModel = new CreatePaymentResponseModel
//            {
//                WasPaymentSuccessful = wasPaymentSuccessful,
//                PaymentId = payment.PaymentId
//            };

//            return Accepted(responseModel);
//        }

//        /// <summary>
//        /// Handle authentication (via Merchant ID) and authorization (via API key).
//        /// MerchantId is delivered as a header since it will (probably) be necessary for
//        /// every call and should not pollute all request-specific models.
//        /// 
//        /// Improvement Idea: Allow granular error messages to return to the caller,
//        /// though this can in some instances be a slight security risk.
//        /// Also: Introduce caching for enhanced performance and scalability.
//        /// </summary>
//        /// <param name="merchant">The Merchant based on the Merchant ID that was read from the headers</param>
//        /// <returns></returns>
//        /// 

//        [NonAction]
//        private bool EnsureMerchantValidity(out Merchant merchant)
//        {
//            merchant = null;

//            // check merchant ID
//            if (!Request.Headers.ContainsKey(PaymentGatewayConstants.MerchantHeaderName))
//                return false;

//            var rawMerchantId = Request.Headers[PaymentGatewayConstants.MerchantHeaderName].First();
//            if (!Guid.TryParse(rawMerchantId, out var parsedMerchantId))
//                return false;

//            // check if merchant with the merchant ID exists and is enabled
//            merchant = _merchantRepository.GetByMerchantId(parsedMerchantId);
//            if (merchant == null)
//                return false;

//            if (!merchant.IsEnabled)
//                return false;

//            // check api key
//            if (!Request.Headers.ContainsKey(PaymentGatewayConstants.ApiKeyHeaderName))
//                return false;

//            var rawApiKey = Request.Headers[PaymentGatewayConstants.ApiKeyHeaderName].First();
//            var apiKey = Encoding.UTF8.GetString(Convert.FromBase64String(rawApiKey));

//            if (apiKey != merchant.ApiKey)
//                return false;

//            return true;
//        }
//    }
//}
