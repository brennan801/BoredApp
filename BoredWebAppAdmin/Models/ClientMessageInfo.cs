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
}
