using BoredWebApp.Models;
using BoredWebApp.Services;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System;
using System.IO;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Web.Helpers;

namespace BoredWebApp.Pages
{
    [Authorize]
    public class SecureModel : PageModel
    {
        private readonly IDBService dBService;
        private readonly IMyAPIService myAPIService;

        public SecureModel(IDBService dBService, IMyAPIService myAPIService)
        {
            this.dBService = dBService;
            this.myAPIService = myAPIService;
        }
        public string Name { get; private set; }
        public string ID { get; private set; }
        public string ProfileImage { get; private set; }
        public string Message { get; set; }

        [BindProperty]
        public WebImage Image { get; set; }

        public async Task OnGet()
        {
            ID = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value;
            dBService.AddUser(ID);
        }
        public IActionResult OnPost()
        {
            return Redirect("/account/logout");
        }

        public IActionResult OnPostSave()
        {
            var id = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value;
            var name = Request.Form["name"];

            var fileName = $"{id}_profile";

            var imagePath = @"uploads\" + fileName;

            Image.Save(@"~\" + fileName);

            dBService.SaveNameAndPhoto(id, name, fileName);
            return Redirect("/Secure");
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
