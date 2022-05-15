using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json.Serialization;

namespace StudyHub.Domain.Models
{
    public class ResponseRequestModel
    {
        public Guid Id { get; set; }
        public string RefId { get; set; }
        public DateTime ResponseTime { get; set; }
        public string FullResponse { get; set; }
        public string FullRequest { get; set; }

        public Guid? UseId { get; set; }
        [JsonIgnore]
        public virtual AccountUser AccountUser { get; set; }
    }
}
