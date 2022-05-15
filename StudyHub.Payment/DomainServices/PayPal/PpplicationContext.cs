using StudyHub.Payment.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace StudyHub.Payment.DomainServices.PayPal
{
   public  class ApplicationContext : IPaypalConnector
    {
        public int Rating { get; set; }
    }
}
