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

        [BindProperty]
        public ActivityFormRequest ActivityFormRequest { get; set; }

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

        public async Task OnPost()
        {
            var minandMaxPrice = computeMinAndMaxPrice(ActivityFormRequest.Price);
            var minPrice = minandMaxPrice[0];
            var maxPrice = minandMaxPrice[1];
            Activity = await boredAPIService.GetSpecificActivity(
                ActivityFormRequest.Type,
                ActivityFormRequest.Participants,
                minPrice, maxPrice);
        }

        public double[] computeMinAndMaxPrice(string price)
        {
            double minPrice;
            double maxPrice;
            switch (price)
            {
                case "high":
                    minPrice = 0.7;
                    maxPrice = 1;
                    break;
                case "medium":
                    minPrice = 0.4;
                    maxPrice = 0.7;
                    break;
                default:
                    minPrice = 0;
                    maxPrice = .4;
                    break;
            }
            return new[] { minPrice, maxPrice };
        }
    }
}
