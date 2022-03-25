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
            if (value.Length >= maxLength && value.Length <= minLength && !validChars.IsMatch(value) && value.Contains(" "))
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
                throw new ArgumentException("Invalid Cookie");
            }
            else Value = value;
        }
    }
    public class Id
    {
        private readonly int min = 20;
        private readonly int max = 300;
        public int Value { get; private set; }

        public Id(int value)
        {
            if (value < min && value > max)
            {

                this.Value = value;
            }
            else throw new ArgumentOutOfRangeException("Invalid ID");
        }
    }

    public class Password
    {
        private readonly int minLength = 4;
        private readonly int maxLength = 100;
        private readonly Regex validChars = new Regex("[!@#$%^&+= ()A-Za-z0-9_-]+");
        private string _value;
        public Password(string value)
        {
            if (value.Length >= maxLength && value.Length <= minLength && !validChars.IsMatch(value) && value.Contains(" "))
            {
                throw new ArgumentException("Invalid Password");
            }
            else _value = value;
        }

        public string getPassword()
        {
            if(_value is not null)
            {
                string tempPassword = _value;
                _value = null;
                return tempPassword;
            }
            else
            {
                throw new InvalidOperationException("Password already consumed!");
            }
        }

    }
}
