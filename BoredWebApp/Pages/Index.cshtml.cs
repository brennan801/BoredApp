using BoredShared.Models;
using BoredWebApp.Models;
using BoredWebApp.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace BoredWebApp.Pages
{
    public class IndexModel : PageModel
    {
        private readonly IBoredAPIService boredAPIService;
        private readonly IDBService dBService;

        public ActivityModel Activity { get; set; }
        public ActivityModel SpecificActivity { get; set; }
        public List<Comment> Comments { get; set; }

        [BindProperty]
        public ActivityFormRequest ActivityFormRequest { get; set; }

        public IndexModel(IBoredAPIService boredAPIService, IDBService dBService)
        {
            this.boredAPIService = boredAPIService;
            this.dBService = dBService;
            Activity = new ActivityModel();
            SpecificActivity = new ActivityModel();
            SpecificActivity.Activity = "Generate New Activity With Form";
            ActivityFormRequest = new ActivityFormRequest();
            
        }

        public void OnPostSave()
        {
            dBService.SaveActivity(Activity);
        }

        public IActionResult OnPostLogin()
        {
            return Redirect("/account/login");
        }

        public void OnPostComment()
        {
            Console.WriteLine("Hi!");
            Comment comment = new Comment();
            /*if (User.Identity.IsAuthenticated)
            {
                comment.User = User.Identity.Name;
            }
            else
            {
                comment.User = "Guest";
            }*/
            comment.User = "Guest";
            comment.Date = DateTime.Now.Date.ToString();
            comment.Body = Request.Form["body"];
            dBService.SaveComment(comment);
        }

        public async Task OnGet()
        {
            Activity = await boredAPIService.GetRandomActivity();
            Comments = dBService.GetComments();
        }

        public async Task OnPost()
        {
            var minandMaxPrice = computeMinAndMaxPrice(ActivityFormRequest.Price);
            var minPrice = minandMaxPrice[0];
            var maxPrice = minandMaxPrice[1];
            var type = ActivityFormRequest.Type;
            var participants = ActivityFormRequest.Participants;
            var uri = $"http://www.boredapi.com/api/activity?type={type}&participants={participants}&minprice={minPrice}&maxPrice={maxPrice}";
            var httpClient = new HttpClient();
            var result = await httpClient.GetStringAsync(uri);
            var data = JsonConvert.DeserializeObject<ActivityModel>(result);

            var activityResult = new ActivityModel()
            {
                Activity = data.Activity,
                Accessibility = data.Accessibility,
                Type = data.Type,
                Participants = data.Participants,
                Price = data.Price,
                Link = data.Link,
                Key = data.Key,
                Error = data.Error
            };
            if (activityResult.Error is not null)
            {
                SpecificActivity.Activity = "No Activity Found";
            }
            else SpecificActivity = activityResult;
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
