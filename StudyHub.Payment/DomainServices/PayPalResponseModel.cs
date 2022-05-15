using System;
using System.Collections.Generic;
using System.Text;

namespace StudyHub.Payment.DomainServices
{
   public  class PayPalResponseModel
    {
        public string access_token { get; set; }
        public string token_type { get; set; }
        public string app_id { get; set; }
        public int expires_in { get; set; }
        public string nonce { get; set; }
    }
}
