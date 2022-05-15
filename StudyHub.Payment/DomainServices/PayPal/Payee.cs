using StudyHub.Payment.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace StudyHub.Payment.DomainServices.PayPal
{
    public class Payee : IPaypalConnector
    {
        public string ClientId { get; set; }
        
    }
}
