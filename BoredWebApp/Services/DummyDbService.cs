using BoredShared.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BoredWebApp.Services
{
    public class DummyDbService : IDBService
    {
        public List<ActivityModel> SavedActivities = new List<ActivityModel>();

        public List<ActivityModel> getSavedActivities()
        {
            return SavedActivities;
        }

        public void SaveActivity(ActivityModel activity)
        {
            SavedActivities.Add(activity);
        }
    }
}
