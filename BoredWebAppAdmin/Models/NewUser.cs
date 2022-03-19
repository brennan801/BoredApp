using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace BoredWebAppAdmin.Models
{
    public class NewUser
    {
        //string userName, byte[] salt, string hashed
        public UserName UserName { get; set; }
        public byte[] Salt { get; set; }
        public string Hashed { get; set; }
    }

    public class UserName
    {
        private readonly int minLength = 4;
        private readonly int maxLength = 80;
        private readonly Regex validChars = new Regex("[A-Za-z0-9_-]+");
        public string Value { get; private set; }
        public UserName(string value)
        {
            if (!validChars.IsMatch(value) && value.Length >= maxLength && value.Length <= minLength && value.Contains(" "))
            {
                throw new ArgumentException("Invalid Username");
            }
            else Value = value;
        }
    }
}
