using BoredWebApp.Models;
using BoredWebApp.Services;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System;
using System.Threading.Tasks;

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

        public async Task OnGet()
        {
            /*this.UserName = RouteData.Values["UserName"].ToString();
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
            }*/
            /*if (!User.Identity.IsAuthenticated)
            {
                await LogIn();
            }*/
        }
        public IActionResult OnPost()
        {
            /*try
            {
                this.UserName = RouteData.Values["UserName"].ToString();
                string cookieValue = dBService.GetCookieValue(UserName);
                Response.Cookies.Delete(cookieValue);
                dBService.RemoveCookie(UserName);
                return RedirectToPage("LoggedOut");
            }

            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return RedirectToPage("LogIn");
            }*/
            return Redirect("/account/logout");
        }

        public void OnPostSave()
        {
            this.UserName = RouteData.Values["UserName"].ToString();
            UserFavorites userFavorites = new UserFavorites(
                this.UserName, Request.Form["hobbie"], Int32.Parse(Request.Form["groupSize"].ToString()), 
                Request.Form["birthday"], Request.Form["animal"]
                ) ;
            dBService.SaveFavorites(userFavorites);
        }

        public async Task LogIn(string reuturnUrl = "/")
        {
            await HttpContext.ChallengeAsync("Auth0", new AuthenticationProperties { RedirectUri = reuturnUrl } );
        }

        public async Task LogOut()
        {
            await HttpContext.SignOutAsync("Auth0", new AuthenticationProperties()
            {
                RedirectUri = Url.Action("Index", "Home")
            });
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
        }
    }
}
