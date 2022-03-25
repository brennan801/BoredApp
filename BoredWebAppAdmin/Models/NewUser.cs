using BoredShared.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace BoredWebAppAdmin.Models
{
    public class NewUser
    {
        public readonly Id ID;
        public UserName UserName { get; private set; }
        public byte[] Salt { get; private set; }
        public string Hash { get; private set; }

        public NewUser(int id, string userName, byte[] salt, string hash)
        {
            ID = new(id);
            UserName = new(userName);
            Salt = salt;
            Hash = hash;
        }

        public void ChangeUserName(UserName newUserName)
        {
            UserName = newUserName;
        }
        public void ChangePassword(byte[] newSalt, string newHash)
        {
            Salt = newSalt;
            Hash = newHash;
        }
    } 
}