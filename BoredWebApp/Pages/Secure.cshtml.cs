using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace BoredWebApp.Pages
{
    public class SecureModel : PageModel
    {
        public string UserName { get; set; }
        public string Message { get; set; }
        public void OnGet(string UserName)
        {
            Message = $"Welcome {UserName}";
        }
    }
}
