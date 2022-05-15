using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using System.Text.Json.Serialization;
using static StudyHub.Domain.Enum.DomainEnum;

namespace StudyHub.Domain.Models
{
    public class Order : BaseModel
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
       

    }
}
