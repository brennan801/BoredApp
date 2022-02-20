using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace BoredWebApp.Pages
{
    public class SecureModel : PageModel
    {
        public string UserName { get; set; }
        public string Message { get; set; }
        public SecureModel(string userName)
        {
            UserName = userName;
        }
        public void OnGet()
        {
            Message = $"Welcome {UserName}";
        }
    }
}
