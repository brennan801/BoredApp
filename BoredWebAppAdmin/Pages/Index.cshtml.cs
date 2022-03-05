using BoredWebAppAdmin.Models;
using BoredWebAppAdmin.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Text;
using System.Net;

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
            databaseService.SaveClientInformation(newClient);
            
        }

        public void OnPostDownload()
        {
            var clientID = Request.Form["dId"];
            ClientInformation client = databaseService.GetClient(clientID);
            var fullPath = $"~/BoredWebAppAdmin/wwwroot/clients/client{clientID}.txt";
            using (StreamWriter writer = new StreamWriter(fullPath))
            {
                writer.WriteLine("[Interface]");
                writer.WriteLine($"PrivateKey = {client.ClientPrivateKey}");
                writer.WriteLine($"Address = {client.IpAddress}");
                writer.WriteLine();
                writer.WriteLine("[Peer]");
                writer.WriteLine("PublicKey = Z6KdvO0qkeoPkkLjs8ANPhydzU23T3YjOw2JCG1wrTY=");
                writer.WriteLine("AllowedIPs = 10.200.20.1/24");

            }

        }

        public async Task OnPostWireguard()
        {
            Console.WriteLine("Hit the post request");
            await adminApiService.RestartWireguardAsync();
            Console.WriteLine("after the post request");
        }
    }
}
