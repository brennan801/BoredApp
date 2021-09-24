using BoredShared.Models;
using BoredWebApp.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BoredWebApp.Pages
{
    public class IndexModel : PageModel
    {
        private readonly IBoredAPIService boredAPIService;
        public ActivityModel Activity { get; set; }

        public IndexModel(IBoredAPIService boredAPIService)
        {
            this.boredAPIService = boredAPIService;
            Activity = new ActivityModel();
            Activity.Activity = "";
        }

        public void OnGet()
        {

        }

        public async Task OnPost()
        {
            Activity = await boredAPIService.GetRandomActivity();
        }
    }
}
