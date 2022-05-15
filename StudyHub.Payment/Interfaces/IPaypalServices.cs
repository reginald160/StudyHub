using StudyHub.Payment.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace StudyHub.Payment.Interfaces
{
    public interface IPaypalServices
    {
        string GenerateInvoiceNumber(string token);
        bool OrderResponse(PayPalPaymentCreatedResponse order);




    }
}
