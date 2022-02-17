using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace BoredWebApp.Pages
{
    public class LogInModel : PageModel
    {
        public void OnGet()
        {
        }

        public void OnPost()
        {
            var userName = Request.Form["userName"];
            var password = Request.Form["password"];
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
            Response.Cookies.Append($"{userName} Cookie", "AllYourBaseAreBelongToUs", cookieOptions);
        }
    }
}
