using BoredShared.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace BoredWebAppAdmin.Models
{
    public class ClientInformation
    {
        public ClientInformation(int id, string clientName, string ipAddress, string dateAdded, string allowedIpRange, string clientPublicKey, string clientPrivateKey)
        {
            ID = new(id);
            ClientName = new(clientName);
            IpAddress = new(ipAddress);
            DateAdded = dateAdded;
            AllowedIpRange = allowedIpRange;
            ClientPublicKey = clientPublicKey;
            ClientPrivateKey = clientPrivateKey;
        }
        public ClientInformation(Id id, UserName clientName, string ipAddress, string dateAdded, string allowedIpRange, string clientPublicKey, string clientPrivateKey)
        {
            ID = id;
            ClientName = clientName;
            IpAddress = new(ipAddress);
            DateAdded = dateAdded;
            AllowedIpRange = allowedIpRange;
            ClientPublicKey = clientPublicKey;
            ClientPrivateKey = clientPrivateKey;
        }
        public Id ID { get; set; }
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
