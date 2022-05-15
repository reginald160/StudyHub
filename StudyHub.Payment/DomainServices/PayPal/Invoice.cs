using StudyHub.Payment.Interfaces;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using System.Text.Json.Serialization;

namespace StudyHub.Payment.DomainServices.PayPal
{
    public class Invoice : BaseModel, IPaypalConnector
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public string InvoiceId { get; set; }

        public Guid PayeeId { get; set; }
        [ForeignKey("InvoiceId")]
        [JsonIgnore]
        public Payee Payee { get; set; }

        public Guid PayerId { get; set; }
        [ForeignKey("PayerId")]
        [JsonIgnore]
        public User Payer { get; set; }
    }
}
