using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudyHub.Domain.WebHookServices
{
    public class DomainEvent
    {
        public long ID { get; set; }
#nullable enable
        public Guid? ActorID { get; set; }
#nullable disable
        public DateTime TimeStamp { get; set; }

        public EventType EventType { get; set; }
    }
}
