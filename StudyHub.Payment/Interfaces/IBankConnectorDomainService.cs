using StudyHub.Payment.DomainServices;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace StudyHub.Payment.Interfaces
{
    public interface IBankConnectorDomainService
    {
        /// <summary>
        /// Sends the payment order to the Bank to do the actual retrieval of money from the Shopper's card and payout to the Merchant.
        /// </summary>
        /// <param name="amount"></param>
        /// <param name="currencyCode"></param>
        /// <param name="sourceCreditCardInformation"></param>
        /// <param name="targetCreditCardInformation"></param>
        /// <returns></returns>
        Task<PaymentOrderResult> TransmitPaymentOrderAsync(decimal amount, string currencyCode, CardInformation sourceCreditCardInformation,
                                                           CardInformation targetCreditCardInformation);
    }
}
