using BoredWebApp.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace BoredWebApp.Pages
{
    public class SecureModel : PageModel
    {
        private readonly IDBService dBService;

        public SecureModel(IDBService dBService)
        {
            this.dBService = dBService;
        }
        public string UserName { get; set; }
        public string Message { get; set; }
        public void OnGet(string UserName)
        {
            Message = $"Welcome {UserName}";
        }
    }
}
