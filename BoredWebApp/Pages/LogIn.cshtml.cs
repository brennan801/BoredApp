using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BoredWebApp.Models;
using BoredWebApp.Services;
using Microsoft.AspNetCore.Cryptography.KeyDerivation;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace BoredWebApp.Pages
{
    public class LogInModel : PageModel
    {
        private readonly IDBService dBService;
        public string Message { get; set; }

        public LogInModel(IDBService dBService)
        {
            this.dBService = dBService;
            Message = "";
        }
        public void OnGet()
        {
        }

        public IActionResult OnPost()
        {
            var userName = Request.Form["userName"];
            var password = Request.Form["password"];
            byte[] salt = Array.Empty<byte>();
            string oldHash = "";

            try
            {
                salt = dBService.GetSalt(userName);
                oldHash = dBService.GetHash(userName);
            }
            catch(InvalidOperationException)
            {
                Message = "Incorrect Credentials! Please Try Again!";
            }

            // derive a 256-bit subkey (use HMACSHA256 with 100,000 iterations)
            string newHashed = Convert.ToBase64String(KeyDerivation.Pbkdf2(
                password: password,
                salt: salt,
                prf: KeyDerivationPrf.HMACSHA256,
                iterationCount: 100000,
                numBytesRequested: 256 / 8));

            if (oldHash == newHashed)
            {
                Message = "You have been logged in!";
                var cookieOptions = new CookieOptions
                {
                    // Set the secure flag, which Chrome's changes will require for SameSite none.
                    // Note this will also require you to be running on HTTPS.
                    Secure = true,

                    // Set the cookie to HTTP only which is good practice unless you really do need
                    // to access it client side in scripts.
                    HttpOnly = true,

                    // Add the SameSite attribute, this will emit the attribute with a value of none.
                    // To not emit the attribute at all set
                    // SameSite = (SameSiteMode)(-1)
                    SameSite = SameSiteMode.None,

                    Expires = DateTimeOffset.Now.AddHours(1)
                };

                // Add the cookie to the response cookie collection
                Response.Cookies.Append($"{userName}Cookie", $"{userName}Value", cookieOptions);
                UserCookie userCookie = new UserCookie(userName, $"{userName}Value");
                dBService.SaveCookie(userCookie);
                return RedirectToPage("Secure", new {UserName = userName });
            }
            else
            {
                return Page();
            }
        }
    }
}
