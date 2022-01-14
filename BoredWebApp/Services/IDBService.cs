using BoredShared.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BoredWebApp.Services
{
    public interface IDBService
    {
        void SaveActivity(ActivityModel activity);
        List<ActivityModel> getSavedActivities();
    }
}
