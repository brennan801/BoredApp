using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BoredWebAppAdmin.Services
{
    public interface IAdminApiService
    {
        public Task RestartWireguardAsync();
    }
}
