using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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

        public LogInModel(IDBService dBService)
        {
            this.dBService = dBService;
        }
        public void OnGet()
        {
        }

        public void OnPost()
        {
            var userName = Request.Form["userName"];
            var password = Request.Form["password"];

            var salt = dBService.GetSalt(userName);
            var oldHashed = dBService.GetHash(userName);

            // derive a 256-bit subkey (use HMACSHA256 with 100,000 iterations)
            string newHashed = Convert.ToBase64String(KeyDerivation.Pbkdf2(
                password: password,
                salt: salt,
                prf: KeyDerivationPrf.HMACSHA256,
                iterationCount: 100000,
                numBytesRequested: 256 / 8));

            if (oldHashed == newHashed)
            {
                Console.WriteLine("correct!");
            }
            else
            {
                Console.WriteLine("nope!");
                Console.WriteLine($"newHash: {newHashed}, oldHash: {oldHashed}");
            }

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
                SameSite = SameSiteMode.None
            };

            // Add the cookie to the response cookie collection
            Response.Cookies.Append($"Cookie", "AllYourBaseAreBelongToUs", cookieOptions);
        }
    }
}
