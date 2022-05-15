using StudyHub.Domain.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using System.Text.Json.Serialization;
using static StudyHub.Domain.Enum.DomainEnum;

namespace StudyHub.Payment.DomainServices
{
    public class PaymentOrderResult
    {

        public PaymentOrderResult(string orderUniqueIdentifier, PaymentOrderResultStatus resultStatus)
        {
            OrderUniqueIdentifier = orderUniqueIdentifier;
            ResultStatus = resultStatus;
        }

        public string OrderUniqueIdentifier { get; }
        public PaymentOrderResultStatus ResultStatus { get; }
    }


    public class PaymentOrder : BaseModel
    {
        public Guid AccountUserId { get; set; }
        [JsonIgnore]
        [ForeignKey("AccountUserId")]
        public AccountUser AccountUser { get; set; }
        public string RefNumber { get; set; }
        public OrderStatus OrderStatus { get; set; }
        public DateTime CompletedDate { get; set; }
        public string CompletionUrl { get; set; }
        public string CancelUrl { get; set; }
        public string RedirectionUrl { get; set; }
        public bool IsNewOrder { get; set; }
       
    }

}
