using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BoredWebAppAdmin.Models
{
    public class ClientInformation
    {
        public int ID { get; set; }
        public string ClientName { get; set; }
        public string IpAddress { get; set; }
        public string DateAdded { get; set; }
        public string AllowedIpRange { get; set; }
        public string ClientPublicKey { get; set; }
        public string ClientPrivateKey { get; set; }
    }
}
