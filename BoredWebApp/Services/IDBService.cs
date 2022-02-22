using BoredShared.Models;
using BoredWebApp.Models;
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
        byte[] GetSalt(string userName);
        string GetHash(string userName);
        void SaveCookie(UserCookie cookie);
        string GetCookieValue(string userName);
        void RemoveCookie(string userName);
    }
}
