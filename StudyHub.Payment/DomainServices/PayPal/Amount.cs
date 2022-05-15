using StudyHub.Payment.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace StudyHub.Payment.DomainServices.PayPal
{
   public  class Amount : IPaypalConnector
    {
        public Amount()
        {
            Tax_total = Shipping = Handling = Insurance = Shipping_discount = 0;
        }
        public double Item_total { get; set; }
        public double Tax_total { get; set; }
        public double Shipping { get; set; }
        public double Handling { get; set; }
        public double Insurance { get; set; }
        public double Shipping_discount { get; set; }
    }
}
