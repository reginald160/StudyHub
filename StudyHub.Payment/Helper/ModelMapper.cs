﻿
using StudyHub.Payment.DomainServices;
using StudyHub.Payment.DTO;
using System;
using System.Collections.Generic;
using System.Text;


namespace StudyHub.Payment.Helper
{
    public class ModelMapper
    {
        public PaymentModelDTO MapPayment(PaymentModel payment)
        {
            var paymentModel = new PaymentModelDTO
            {
                PaymentId = payment.PaymentId,
                StatusCode = MapPaymentState(payment.CurrentState),
                MerchantId = payment.MerchantId,
                ExternalShopperIdentifier = payment.ExternalShopperIdentifier,
                CreditCard = MapAndMaskCreditCard(payment.CreditCardInformation),
                CreateDate = payment.CreateDate,
                Amount = payment.Amount.Amount,
                CurrencyCode = payment.Amount.CurrencyCode
            };

            // mask credit card

            return paymentModel;
        }

        public PaymentStatusCode MapPaymentState(PaymentState paymentState)
        {
            switch (paymentState)
            {
                case PaymentState.PaymentFailed:
                    return PaymentStatusCode.PaymentFailure;
                case PaymentState.PaymentSuccessful:
                    return PaymentStatusCode.PaymentSuccess;

                case PaymentState.Created:
                    throw new InvalidOperationException("Created state is an in-between state and should never result in a customer-accessible state");
                default:
                    throw new ArgumentOutOfRangeException(nameof(paymentState), paymentState, null);
            }
        }

        /// <summary>
        /// Very simple masking function, follows the standard of showing only 4 unmasked credit card digits
        /// </summary>
        /// <param name="cardNumber"></param>
        /// <returns></returns>
        public string MaskCreditCardNumber(string cardNumber)
        {
            if (cardNumber.Length < 4)
                throw new InvalidOperationException("credit card number has invalid length");

            char maskCharacter = '*';
            var maskLength = cardNumber.Length - 4;

            var maskedPart = new string(maskCharacter, maskLength);
            var unmaskedPart = cardNumber.Substring(maskLength);
            return maskedPart + unmaskedPart;
        }

        public CreditCardModel MapAndMaskCreditCard(CardInformation creditCardInformation)
        {
            return new CreditCardModel
            {
                CardNumber = MaskCreditCardNumber(creditCardInformation.CardNumber),
                ExpiryDateYear = creditCardInformation.ExpiryDate.Year,
                ExpiryDateMonth = creditCardInformation.ExpiryDate.Month,
                Ccv = creditCardInformation.Ccv
            };
        }
    }
}
