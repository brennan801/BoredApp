using BoredShared.Models;
using System;

namespace BoredWebApp.Models
{
    public class UserFavorites
    {
        public UserFavorites(string userName, string hobbie, int GroupSize, string birthday, string faveAnimal)
        {
            this.UserName = new(userName);
            this.Hobbie = hobbie;
            this.GroupSize = GroupSize;
            this.Birthday = birthday;
            this.FaveAnimal = faveAnimal;
        }
        public UserName UserName { get; private set; }
        public string Hobbie { get; private set; }
        public int GroupSize { get; private set; }
        public string Birthday { get; private set; }
        public string FaveAnimal { get; private set; }
    }
}
