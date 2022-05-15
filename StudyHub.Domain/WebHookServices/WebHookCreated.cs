using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudyHub.Domain.WebHookServices
{
    /// <summary>
    /// WebHookCreated
    /// </summary>
    public class WebHookCreated : DomainEvent
    {

        public WebHookCreated() { }

        public long WebHookId { get; set; }

        // Add any custom props hire...
    }
}
