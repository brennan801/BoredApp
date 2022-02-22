using BoredWebApp.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System;

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

        public IActionResult OnGet()
        { 
            this.UserName = RouteData.Values["UserName"].ToString();
            string cookieValue = dBService.GetCookieValue(UserName);
            string actualValue = Request.Cookies[$"{UserName}Cookie"];
            System.Console.WriteLine(cookieValue);
            System.Console.WriteLine(actualValue);

            if (cookieValue != actualValue)
            {
                return RedirectToPage("LogIn");
            }
            else
            {
                Message = $"Welcome {UserName}";
                return Page();
            }

        }
        public IActionResult OnPost()
        {
                string cookieValue = dBService.GetCookieValue(UserName);
                Response.Cookies.Delete(cookieValue);
                dBService.RemoveCookie(UserName);
                return RedirectToPage("Index");
            
           /* catch(Exception e)
            {
                Console.WriteLine(e.Message);
                return RedirectToPage("LogIn");
            }*/
        }
    }
}
