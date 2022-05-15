using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudyHub.Application.Settings
{
    public class PayPalSettings
    {
        public string ClientId { get; set; }
        public string ClientScrete { get; set; }
        public string GrantType { get; set; }
        public string ClientCredential { get; set; }
        public string BaseAuthUrl { get; set; }
        public string ConnectionString { get; set; }

    }
}
