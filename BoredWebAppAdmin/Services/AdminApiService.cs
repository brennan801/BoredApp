using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace BoredWebAppAdmin.Services
{
    public class AdminApiService : IAdminApiService
    {
        public async Task RestartWireguardAsync()
        {
            /*var uri = $"http://host.docker.internal:5000/api/wireguard";
            var httpClient = new HttpClient();
            await httpClient.PostAsync(uri,"idk");*/
        }

        public async Task<string> ShowWireguardStatusAsync()
        {
            var uri = $"http://host.docker.internal:5000/api/wireguard";
            var httpClient = new HttpClient();
            var result = await httpClient.GetStringAsync(uri);
            return result;

        }
    }
}
