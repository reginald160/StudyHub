using System;
using System.Collections.Generic;
using System.Text;

namespace StudyHub.Payment.DomainServices
{
    public class PayPalAuthModel
    {
        public PayPalAuthModel()
        {
            grant_type = "client_credentials";
        }
        public string grant_type { get; set; }
    }
}
