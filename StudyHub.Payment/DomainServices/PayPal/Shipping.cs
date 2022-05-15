using StudyHub.Payment.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace StudyHub.Payment.DomainServices.PayPal
{
   public  class Shipping : BaseModel, IPaypalConnector
    {
        public string Address { get; set; }
        public string Method { get; set; }
    }
}
