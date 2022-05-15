using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json.Serialization;

namespace StudyHub.Payment.DomainServices.PayPal
{
   public  class OrderModel
    {
        public OrderModel()
        {
            RequestId = Guid.NewGuid();
            AttributionId = Guid.NewGuid();
            MetadataId = Guid.NewGuid();
        }
        [JsonIgnore]
        public Guid RequestId { get; set; }
        [JsonIgnore]
        public Guid AttributionId { get; set; }
        [JsonIgnore]
        public Guid MetadataId { get; set; }
        public Intent Intent { get; set; }
        public User Payer { get; set; }
        public List<Purchase_unit> Purchase_units { get; set; }
        public ApplicationContext Application_context { get; set; }
    }
}
