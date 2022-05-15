using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using System.Text.Json.Serialization;

namespace StudyHub.Payment.DomainServices.PayPal
{
    public class BaseModel
    {
        public Guid Id { get; set; }
        public DateTime CreatedDate { get; set; }
        public Guid CreatedById { get; set; }
        [ForeignKey("CreatedById")]
        public User CreatedBy { get; set; }
        public DateTime EditedDate { get; set; }
        public Guid EditedById { get; set; }
        [ForeignKey("EditedById")]
        public User EditedBy { get; set; }
        [JsonIgnore]
        public byte[] RowVersion { get; set; }

    }
}
