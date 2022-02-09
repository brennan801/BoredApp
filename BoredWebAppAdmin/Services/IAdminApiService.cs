using BoredWebAppAdmin.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BoredWebAppAdmin.Services
{
    public interface IAdminApiService
    {
        public Task RestartWireguardAsync();
        public Task<String> ShowWireguardStatusAsync();
        public Task<ClientInformation> AddWireguardClientAsync(ClientMessageInfo cmi);
    }
}
