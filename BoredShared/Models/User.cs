using System;
using System.Collections.Generic;
using System.Text;

namespace BoredShared.Models
{
    public enum UserStatus { New, Requested, Accepted, Denied}
    public class User
    {
        public User(string id, string userName, string photo)
        {
            this.UserName = new(userName);
            this.Status = UserStatus.New;
            Id = id;
            Photo = photo;
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
