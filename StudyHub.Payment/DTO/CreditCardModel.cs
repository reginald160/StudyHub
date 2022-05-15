using System;

namespace StudyHub.Payment.DTO
{
    public class CreditCardModel
    {
        public string CardNumber { get; set; }
        public int ExpiryDateMonth { get; set; }
        public int ExpiryDateYear { get; set; }
        public int Ccv { get; set; }
    }
}