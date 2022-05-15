using System;
using System.Collections.Generic;
using System.Text;

namespace StudyHub.Payment.DTO
{
    public class ActivateLicenceDTO
    {
        public Guid? MerchantIdId { get; set; }
        public int Duration { get; set; }
        public DateTime ActivationDate { get; set; }
    
    }
}
