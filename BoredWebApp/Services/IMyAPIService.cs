using Microsoft.AspNetCore.Http;
using System.Threading.Tasks;

namespace BoredWebApp.Services
{
    public interface IMyAPIService
    {
        public Task AddImageToDisk(IFormFile image);
    }
}
