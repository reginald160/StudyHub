using StudyHub.Payment.Interfaces;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using System.Text.Json.Serialization;

namespace StudyHub.Payment.DomainServices.PayPal
{
    public class Item :BaseModel, IPaypalConnector
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public Guid InvoiceId { get; set; }
        [ForeignKey("InvoiceId")]
        [JsonIgnore]
        public Invoice Invoice { get; set; }
    }
}
