using BoredShared.Models;

namespace BoredWebApp.Models
{
    public class UserCookie
    {
        public readonly UserName UserName;
        public readonly UserCookieValue Value;

        public UserCookie(string userName, string value)
        {
            this.UserName = new(userName);
            this.Value = new(value);
        }

        public string GetUserName()
        {
            return this.UserName.Value;
        }
        public string GetCookie()
        {
            return this.Value.Value;
        }
    }
}
