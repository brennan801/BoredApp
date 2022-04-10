using BoredWebApp.Models;
using BoredWebApp.Services;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System;
using System.Linq;
using System.Security.Claims;
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
        }
        public string Name { get; private set; }
        public string ID { get; private set; }
        public string ProfileImage { get; private set; }
        public string Message { get; set; }
        public async Task OnGet()
        {

            Name = User.Claims.FirstOrDefault(c => c.Type == "name")?.Value;
            ID = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value;
            dBService.AddUser(ID);
        }
        public IActionResult OnPost()
        {
            return Redirect("/account/logout");
        }

        public void OnPostSave()
        {
            var id = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value;
            var name = Request.Form["name"];
            var picture = Request.Form["image"];
            dBService.SaveNameAndPhoto(id, name, picture);
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
