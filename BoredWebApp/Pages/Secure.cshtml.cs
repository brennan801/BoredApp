using BoredWebApp.Models;
using BoredWebApp.Services;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System;
using System.Threading.Tasks;

namespace BoredWebApp.Pages
{
    [Authorize]
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
            
        }
        public IActionResult OnPost()
        {
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
