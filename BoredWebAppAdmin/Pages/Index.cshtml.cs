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
    public class IndexModel : PageModel
    {
        private readonly ILogger<IndexModel> _logger;
        private readonly IDatabaseService databaseService;

        [BindProperty]
        public ClientInformation ClientInformationFormRequest { get; set; }

        public IndexModel(ILogger<IndexModel> logger, IDatabaseService databaseService)
        {
            _logger = logger;
            this.databaseService = databaseService;
        }

        public void OnGet()
        {

        }

        public void OnPost()
        {
            ClientInformation client = new()
            {
                ClientName = ClientInformationFormRequest.ClientName,
                IPAddress = ClientInformationFormRequest.IPAddress,
                DateAdded = ClientInformationFormRequest.DateAdded,
                AllowedIPRange = ClientInformationFormRequest.AllowedIPRange,
                ClientPublicKey = ClientInformationFormRequest.ClientPublicKey,
                ClientPrivateKey = ClientInformationFormRequest.ClientPrivateKey
            };
            databaseService.SaveClientInformation(client);
        }
    }
}
