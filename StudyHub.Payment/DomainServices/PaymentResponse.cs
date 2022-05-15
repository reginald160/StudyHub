using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;

namespace StudyHub.Payment.DomainServices
{
    
    public class PaymentResponse : HttpResponseMessage
    {
        public PaymentResponse()
        {
            

        }
        public string Requestid => GetResonseId();
        public int ResponseCode { get; set; }
        public string ResponseMessage { get; set; }
        public string Status { get; set; }
        public object Data { get; set; }


        public static string GetResonseId()
        {
            var year = DateTime.Now.Month.ToString();
            var day = DateTime.Now.Day.ToString();
            return DateTime.Now.Year.ToString() + year + day + DateTime.Now.ToString("ddmmyyhhmmss");
        }
    }
}
