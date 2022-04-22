using BoredWebAppAdmin.Models;
using Microsoft.Extensions.Primitives;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BoredWebAppAdmin.Services
{
    public interface IDatabaseService
    {
        public void SaveClientInformation(ClientInformation clientInformation);
        public int GetLargestId();
        public void SaveNewUser(User newUser);
        ClientInformation GetClient(StringValues clientID);
        List<BoredShared.Models.User> GetUsers();
    }
}
