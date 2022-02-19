using BoredShared.Models;
using Microsoft.Extensions.Primitives;
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
        string GetSalt(string userName);
        string GetHash(string userName);
    }
}
