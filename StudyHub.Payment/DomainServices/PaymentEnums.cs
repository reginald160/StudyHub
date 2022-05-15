using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace StudyHub.Payment.DomainServices
{
    public enum PaymentOrderResultStatus
    {
        Successful,
        FailedDueToUnknownReason,
        FailedDueToRejectedCreditCard
    }

    public enum PaymentState
    {
        Created,
        PaymentFailed,
        PaymentSuccessful
    }

    public enum PaymentStatusCode
    {
        PaymentSuccess,
        PaymentFailure
    }
    public enum CardType
    {
        #region Values
        [Display(Name = "Visa card")]
        VISA,
        [Display(Name = "Mastecard card.")]
        MASTERCARD,
        [Display(Name = "Discover card")]
        DISCOVER,
        [Display(Name = "American Express card")]
        AMEX,
        [Display(Name = "Solo debit card")]
        SOLO,
        [Display(Name = "Japan Credit Bureau card")]
        JCB,
        [Display(Name = "Military Star card.")]
        STAR,
        [Display(Name = "Delta Airlines card.")]
        DELTA,
        [Display(Name = "Switch credit card.")]
        SWITCH,
        [Display(Name = "Maestro credit card.")]
        MAESTRO,
        [Display(Name = "Carte Bancaire (CB) credit card.")]
        CB_NATIONALE,
        [Display(Name = "Configoga credit card.")]
        CONFIGOGA,
        [Display(Name = " Confidis credit card.")]
        CONFIDIS,
        [Display(Name = "Visa Electron credit card.")]
        ELECTRON,
        [Display(Name = "Cetelem credit card.")]
        CETELEM,
        [Display(Name = "China union pay credit card.")]
        CHINA_UNION_PAY
        #endregion
    }

    public enum PaymentType
    {

        [Display(Name = "A credit card.")]
        CREDIT,
        [Display(Name = "A debit card.")]
        DEBIT, 
        [Display(Name = "A Prepaid card.")]
        PREPAID,
        [Display(Name = "A store card.")]
        STORE,
        [Display(Name = "Card type cannot be determined")]
        UNKNOWN
    }

    public enum CardBrand
    {
        Visa,
    }
    public enum Intent
    {/// <summary>
     /// The merchant intends to capture payment immediately after the customer makes a payment.
     /// </summary>
        CAPTURE,
        /// <summary>
        /// The merchant intends to authorize a payment and place funds on hold after the customer makes a payment
        /// </summary>
        AUTHORIZE
    }
}
