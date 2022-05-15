using StudyHub.Payment.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace StudyHub.Payment.DomainServices.PayPal
{
    public class PaymentInstruction : IPaypalConnector
    {
        public Guid Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
    }
}
