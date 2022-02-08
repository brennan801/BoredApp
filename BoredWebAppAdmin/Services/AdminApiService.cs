using BoredWebAppAdmin.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace BoredWebAppAdmin.Services
{
    public class AdminApiService : IAdminApiService
    {
        public async Task<ClientInformation> AddWireguardClientAsync(int id, string name)
        {
            var uri = $"http://host.docker.internal:5000/api/wireguard";
            var httpClient = new HttpClient();
            var json = JsonConvert.SerializeObject((id, name));
            var stringContent = new StringContent(json, UnicodeEncoding.UTF8, "application/json");
            var result = await httpClient.PostAsync(uri, stringContent);
            ClientInformation newClient = JsonConvert.DeserializeObject<ClientInformation>(await result.Content.ReadAsStringAsync());
            return newClient;
            
        }

        public async Task RestartWireguardAsync()
        {
            var uri = $"http://host.docker.internal:5000/api/wireguard/restart";
            var httpClient = new HttpClient();
            await httpClient.GetAsync(uri);
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
