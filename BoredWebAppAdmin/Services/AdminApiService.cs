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
        public async Task<string> RestartWireguardAsync()
        {
            var uri = $"localhost:5000/api/wireguard";
            var httpClient = new HttpClient();
            var result = await httpClient.GetStringAsync(uri);
            var data = JsonConvert.DeserializeObject<string>(result);
            return data;
        }
    }
}
