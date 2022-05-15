using StudyHub.Payment.Interfaces;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using System.Text.Json.Serialization;

namespace StudyHub.Payment.DomainServices.PayPal
{
   public  class CardPaymentModel : IPaypalConnector
    {
        public Guid Id { get; set; }
        /// <summary>
        /// The card holder's name as it appears on the card.
        /// </summary>
       [MaxLength(300)]
        public string Name { get; set; }
        [Required, MinLength(13, ErrorMessage = "Card can be below 13 digits"), MaxLength(19, ErrorMessage ="Card can not exceed 19 digits")]
        public string Number { get; set; }
        [Required, RegularExpression("^[0-9]{4}-(0[1-9]|1[0-2])$")]
        public string Expiry { get; set; }
        /// <summary>
        /// The three- or four-digit security code of the card. Also known as the CVV, CVC, CVN, CVE, or CID
        /// </summary>
        [Required,RegularExpression("[0-9]{3,4}") ]
        public string Security_code { get; set; }
        [Required, RegularExpression("[0-9]{2,}")]
        public string last_digits { get; set; }
        public CardType Card_type { get; set; }
        public PaymentType Type { get; set; }
        public CardBrand Brand { get; set; }
        [Required]
        public string Country_code { get; set; }
        public BillingAddress Billing_address { get; set; }
        [JsonIgnore]
        public Guid UserId { get; set; }
    }
}
