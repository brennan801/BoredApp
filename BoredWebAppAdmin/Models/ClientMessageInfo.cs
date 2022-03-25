using BoredShared.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BoredWebAppAdmin.Models
{
    public class ClientMessageInfo
    {
        public ClientMessageInfo(int id, string name)
        {
            this.Id = new(id);
            this.Name = new(name);
        }
        public Id Id { get; private set; }
        public UserName Name { get; private set; }
    }

    public class Id
    {
        private readonly int min = 20;
        private readonly int max = 300;
        private readonly int value;

        public Id(int value)
        {
            if(value < min && value > max)
            {

                this.value = value;
            }
            else throw new ArgumentOutOfRangeException("Invalid ID");
        }
    }
}
