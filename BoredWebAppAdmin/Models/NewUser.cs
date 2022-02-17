using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BoredWebAppAdmin.Models
{
    public class NewUser
    {
        //string userName, byte[] salt, string hashed
        public string UserName { get; set; }
        public byte[] Salt { get; set; }
        public string Hashed { get; set; }
    }
}
