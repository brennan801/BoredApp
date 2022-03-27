using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Threading.Tasks;
using BoredWebAppAdmin.Models;
using BoredWebAppAdmin.Services;
using Microsoft.AspNetCore.Cryptography.KeyDerivation;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace BoredWebAppAdmin.Pages
{
    public class AddUserModel : PageModel
    {
        private readonly IDatabaseService databaseService;

        public AddUserModel(IDatabaseService databaseService)
        {
            this.databaseService = databaseService;
        }
        public void OnGet()
        {
        }
        /*public void OnPost()
        {
            var userName = Request.Form["userName"];
            var password = Request.Form["password"];
            byte[] salt = new byte[128 / 8];
            using (var rngCsp = new RNGCryptoServiceProvider())
            {
                rngCsp.GetNonZeroBytes(salt);
            }

            // derive a 256-bit subkey (use HMACSHA256 with 100,000 iterations)
            string hash = Convert.ToBase64String(KeyDerivation.Pbkdf2(
                password: password,
                salt: salt,
                prf: KeyDerivationPrf.HMACSHA256,
                iterationCount: 100000,
                numBytesRequested: 256 / 8));
            NewUser user = new(userName, salt, hash);
            databaseService.SaveNewUser(user);
        }*/
    }
}
