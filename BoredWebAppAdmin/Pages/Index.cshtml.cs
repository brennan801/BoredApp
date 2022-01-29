using BoredWebAppAdmin.Models;
using BoredWebAppAdmin.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BoredWebAppAdmin.Pages
{
    [IgnoreAntiforgeryToken(Order = 2000)]
    public class IndexModel : PageModel
    {
        private readonly ILogger<IndexModel> _logger;
        private readonly IDatabaseService databaseService;
        private readonly IAdminApiService adminApiService;

        [BindProperty]
        public ClientInformation ClientInformationFormRequest { get; set; }

        public string Result { get; set; }

        public IndexModel(ILogger<IndexModel> logger, IDatabaseService databaseService, IAdminApiService adminApiService)
        {
            _logger = logger;
            this.databaseService = databaseService;
            this.adminApiService = adminApiService;
            Result = "";
        }

        public void OnGet()
        {

        }

        public void OnPost()
        {
            ClientInformation client = new()
            {
                ClientName = Request.Form["name"],
                IPAddress = Request.Form["ip"],
                DateAdded = Request.Form["date"],
                AllowedIPRange = Request.Form["range"],
                ClientPublicKey = Request.Form["public key"],
                ClientPrivateKey = Request.Form["private key"],
            };
            databaseService.SaveClientInformation(client);
        }

        public async Task OnPostWireguard()
        {
            Result = await adminApiService.RestartWireguardAsync();
        }
    }
}
