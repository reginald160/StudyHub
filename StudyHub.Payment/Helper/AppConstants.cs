using System;
using System.Collections.Generic;
using System.Text;

namespace StudyHub.Payment.Helper
{
    public static class PaymentGatewayConstants
    {
        public const string MerchantHeaderName = "PG-Merchant";
        public const string ApiKeyHeaderName = "PG-ApiKey";
        public const string PayPalBaseUrl = "https://api-m.sandbox.paypal.com/";

        //TODO: change as soon as there is an official DNS name set for a testing/staging/production environment
        public const string DefaultPaymentGatewayBaseAddress = "https://localhost:32768/";
    }
}
