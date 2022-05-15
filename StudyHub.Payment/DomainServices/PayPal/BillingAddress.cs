using StudyHub.Payment.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace StudyHub.Payment.DomainServices.PayPal
{
    public class BillingAddress : BaseModel, IPaypalConnector
    {
        public string Address_line_1 { get; set; }
        public string Address_line_2 { get; set; }
        public string Admin_area_1 { get; set; }
        public string Admin_area_2 { get; set; }
        public string Postal_code { get; set; }
        public string Country_code { get; set; }



    }
}
