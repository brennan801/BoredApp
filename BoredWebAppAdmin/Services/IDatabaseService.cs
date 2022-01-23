using BoredWebAppAdmin.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BoredWebAppAdmin.Services
{
    public interface IDatabaseService
    {
        public void SaveClientInformation(ClientInformation clientInformation);
    }
}
