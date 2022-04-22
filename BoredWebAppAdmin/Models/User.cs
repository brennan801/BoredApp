using BoredShared.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace BoredWebAppAdmin.Models
{
    public class User
    {
        public enum UserStatus { New, Requested, Accepted, Denied }
        public User(string id, string userName, string photo)
        {
            this.UserName = new(userName);
            this.Status = UserStatus.New;
            Id = id;
            Photo = photo;
        }
        public User(string id, string name, string photo, string status)
        {
            Id = id;
            UserName = new(name);
            photo = photo;
            switch (status)
            {
                case "New":
                    Status = UserStatus.New;
                    break;
                case "Requested":
                    Status = UserStatus.Requested;
                    break;
                case "Accepted":
                    Status = UserStatus.Accepted;
                    break;
                case "Denied":
                    Status = UserStatus.Denied;
                    break;
                default: Status = UserStatus.New;
                    break;
            }
        }
        public UserName UserName { get; private set; }
        public UserStatus Status { get; private set; }
        public string Id { get; }
        public string Photo { get; }

        public void Request()
        {
            Status = UserStatus.Requested;
        }
        public void Accept()
        {
            Status = UserStatus.Accepted;
        }
        public void Deny()
        {
            Status = UserStatus.Denied;
        }
    }
}
