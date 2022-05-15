using System;
using System.Collections.Generic;
using System.Text;

namespace StudyHub.Payment.DomainServices
{
   public class Licence
    {
        public Guid Id { get; set; }
        public Guid? MerchantIdId { get; set; }
        public bool IsActive { get; set; }
   
        public byte[] ActivationKey { get; set; }
        public byte[] ActivationIV { get; set; }
        public byte[] Token { get; set; }
        public string LicenceKey { get; set; }

    }
}
