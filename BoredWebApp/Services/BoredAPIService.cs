using BoredShared.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace BoredWebApp.Services
{
    public class BoredAPIService : IBoredAPIService
    {
        public async Task<ActivityModel> GetRandomActivity()
        {
            var uri = $"http://www.boredapi.com/api/activity/";

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
                Key = data.Key
            };
            return activityResult;
        }

        public async Task<ActivityModel> GetSpecificActivity(string type, int? participants, double? price)
        {
            var uri = $"http://www.boredapi.com/api/activity?type={type}&participants={participants}&price={price}";
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
                Key = data.Key
            };
            return activityResult;
        }
    }
}
