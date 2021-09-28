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
            Activity = new ActivityModel
            {
                Activity = "",
                Type = ""
            };
        }

        public async Task OnGet()
        {
            Activity = await boredAPIService.GetRandomActivity();
        }

        public async Task OnPost(ActivityModel activity)
        {
            string type;
            if (Request.Form["type"] == "")
            {
                type = "";
            }
            else type = Request.Form["type"];

            int? participants;
            if(Request.Form["participants"] == "null")
            {
                participants = null;
            }
            else participants = Int32.Parse(Request.Form["participants"]);

            double? price;
            if (Request.Form["price"] == "null")
            {
                price = null;
            }
            price = Double.Parse(Request.Form["price"]) / 100;
            Activity = await boredAPIService.GetSpecificActivity(activity.Type, activity.Participants, activity.Price);
        }
    }
}
