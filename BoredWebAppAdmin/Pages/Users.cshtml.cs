using BoredWebAppAdmin.Models;
using BoredWebAppAdmin.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Collections.Generic;

namespace BoredWebAppAdmin.Pages
{
    public class UsersModel : PageModel
    {
        private readonly IDatabaseService databaseService;
        public List<BoredShared.Models.User> Users;

        public UsersModel(IDatabaseService databaseService)
        {
            this.databaseService = databaseService;
        }
        public void OnGet()
        {
            Users = databaseService.GetUsers();
        }
    }
}
