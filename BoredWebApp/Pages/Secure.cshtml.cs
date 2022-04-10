using BoredWebApp.Models;
using BoredWebApp.Services;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Hosting.Internal;
using System;
using System.IO;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace BoredWebApp.Pages
{
    [Authorize]
    public class SecureModel : PageModel
    {
        private readonly IDBService dBService;
        private readonly IWebHostEnvironment hostEnvironment;

        public SecureModel(IDBService dBService, IWebHostEnvironment hostEnvironment)
        {
            this.dBService = dBService;
            this.hostEnvironment = hostEnvironment;
        }
        public string Name { get; private set; }
        public string ID { get; private set; }
        public string ProfileImage { get; private set; }
        public string Message { get; set; }

        [BindProperty]
        public IFormFile Image { get; set; }

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
            var uploads = Path.Combine(hostEnvironment.WebRootPath, "uploads");
            var filePath = Path.Combine(uploads, fileName);
            this.Image.CopyTo(new FileStream(filePath, FileMode.Create));

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
