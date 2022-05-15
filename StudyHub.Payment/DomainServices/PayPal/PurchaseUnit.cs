using StudyHub.Payment.Helper;
using StudyHub.Payment.Interfaces;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using System.Text.Json.Serialization;

namespace StudyHub.Payment.DomainServices.PayPal
{
    public class Purchase_unit : IPaypalConnector
    {
        public Purchase_unit()
        {
            Reference_id = RandomClass.GenerateRandomAlphanumericString(8);
           
        }
        public string Reference_id { get; set; }
        public Amount Amount { get; set; }
        public Payee Payee { get; set; }
        public PaymentInstruction Payment_instruction { get; set; }
        public string Description { get; set; }
        [MaxLength(127)]
        public string Custom_id { get; set; }
      

        public Guid Invoice_id { get; set; }
        [JsonIgnore]
        public Invoice Invoice { get; set; }
        /// <summary>
        /// The soft descriptor is the dynamic text used to construct the statement descriptor that appears on a payer's card statement.
        /// </summary>

        public string Id { get; set; }
        public string Soft_descriptor { get; set; }
        public List<Item> Items { get; set; }
        public Shipping Shipping { get; set; }
        public  Payments Payments { get; set; }
    }
}
