using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;

namespace BoredShared.Models
{
    public class DomainPrimitives
    {
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
    public class UserCookieValue
    {
        private readonly int minLength = 4;
        private readonly int maxLength = 80;
        public string Value { get; private set; }
        public UserCookieValue(string value)
        {
            if (value.Length >= maxLength && value.Length <= minLength && value.Contains(" "))
            {
                throw new ArgumentException("Invalid Username");
            }
            else Value = value;
        }
    }
}
