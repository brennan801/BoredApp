using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BoredShared.Models;
using BoredWebApp.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace BoredWebApp.Pages
{
    public class SavedActivitiesModel : PageModel
    {
        private readonly IDBService dBService;

        public List<ActivityModel> SavedActivities { get; set; }
        public SavedActivitiesModel(IDBService dBService)
        {
            this.dBService = dBService;
            SavedActivities = new List<ActivityModel>();
        }
        public void OnGet()
        {
            SavedActivities = dBService.getSavedActivities();
        }
    }
}
