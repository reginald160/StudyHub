using StudyHub.Payment.Helper;
using System;
using System.Collections.Generic;
using System.Text;

namespace StudyHub.Payment.DomainServices
{
    public class Merchant
    {
        private Merchant()
        {
            IsEnabled = true;
        }

        public Guid MerchantId { get; private set; }
        public string Name { get; private set; }
        public string ApiKey { get; private set; } 
        public bool IsEnabled { get; private set; }
        

        public CardInformation CreditCardInformation { get; private set; }

        public byte[] RowVersion { get; set; }

        public static Merchant RegisterNewMerchant(string name, CardInformation creditCardInformation)
        {
            var newMerchant = new Merchant
            {
                Name = name,
                CreditCardInformation = creditCardInformation
            };

            newMerchant.GenerateApiKey();

            return newMerchant;
        }

        private void GenerateApiKey()
        {
            const int apiKeyLength = 22;
            ApiKey = RandomClass.GenerateRandomAlphanumericString(apiKeyLength);
        }

        public void Enable()
        {
            IsEnabled = true;
        }

        public void Disable()
        {
            IsEnabled = false;
        }
    }
}
