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
            UserName = "";
        }
        public string UserName { get; set; }
        public string Message { get; set; }

        public void OnGet()
        { 
            this.UserName = RouteData.Values["UserName"].ToString();
            string cookieValue = dBService.GetCookieValue(UserName);
            string actualValue = Request.Cookies[$"{UserName}Cookie"];

           /* if (cookieValue == actualValue)
            {
                return RedirectToPage("LogIn");
            }
            else
            {
                Message = $"Welcome {UserName}";
                return Page();
            }*/
           Message = $"Welcome {UserName}";

        }
    }
}
