using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace BoredWebApp.Services
{
    public class MyAPIService : IMyAPIService
    {
        public async Task AddImageToDisk(IFormFile image)
        {
            var uri = $"http://host.docker.internal:5000/api/webapp";
            var httpClient = new HttpClient();
            var json = JsonConvert.SerializeObject(image);
            var stringContent = new StringContent(json, UnicodeEncoding.UTF8, "application/json");
            var result = await httpClient.PostAsync(uri, stringContent);
        }
    }
}
