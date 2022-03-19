using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace BoredWebAppAdmin.Models
{
    public class ClientInformation
    {
        public int ID { get; set; }
        public UserName ClientName { get; set; }
        public IPAddress IpAddress { get; set; }
        public string DateAdded { get; set; }
        public string AllowedIpRange { get; set; }
        public string ClientPublicKey { get; set; }
        public string ClientPrivateKey { get; set; }
    }

    public class IPAddress
    {
        private readonly Regex validChars = new Regex("(([0-9]|[1-9][0-9]|1[0-9][0-9]|2[0-4][0-9]|25[0-5])\\.){3}([0-9]|[1-9][0-9]|1[0-9][0-9]|2[0-4][0-9]|25[0-5])");
        public string Value { get; private set; }
        public IPAddress(string value)
        {
            if (!validChars.IsMatch(value))
            {
                throw new ArgumentException("Invalid IPAddress");
            }
            else Value = value;
        }
    }
}
