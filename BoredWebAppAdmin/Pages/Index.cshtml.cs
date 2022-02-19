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
        public string WireguardStatus { get; set; }

        public IndexModel(ILogger<IndexModel> logger, IDatabaseService databaseService, IAdminApiService adminApiService)
        {
            _logger = logger;
            this.databaseService = databaseService;
            this.adminApiService = adminApiService;
            Result = "";
            WireguardStatus = "Not Loaded";
        }

        public async Task OnGet()
        {
            WireguardStatus = await adminApiService.ShowWireguardStatusAsync();
        }

        public async Task OnPost()
        {
            Console.WriteLine("hit post");
            var clientName = Request.Form["name"];
            int id = databaseService.GetLargestId() + 1;
            Console.WriteLine($"client Name: {clientName}, clientID: {id}");
            ClientMessageInfo cmi = new()
            {
                Id = id,
                Name = clientName
            };
            ClientInformation newClient = await adminApiService.AddWireguardClientAsync(cmi);
            Console.WriteLine($"New Client {newClient.ClientName}");
            Console.WriteLine($"Client Public Key {newClient.ClientPublicKey}");
            //databaseService.SaveClientInformation(newClient);
            //await adminApiService.RestartWireguardAsync();
            
        }

        public async Task OnPostWireguard()
        {
            Console.WriteLine("Hit the post request");
            await adminApiService.RestartWireguardAsync();
            Console.WriteLine("after the post request");
        }
    }
}
