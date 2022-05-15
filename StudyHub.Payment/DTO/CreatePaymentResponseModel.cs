using System;

namespace StudyHub.Payment.DTO
{
    public class CreatePaymentResponseModel
    {
        public bool WasPaymentSuccessful { get; set; }
        public Guid PaymentId { get; set; }
    }
}