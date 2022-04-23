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
        void SaveCookie(UserCookie cookie);
        string GetCookieValue(string userName);
        void RemoveCookie(string userName);
        void AddUser(string ID);
        void SaveNameAndPhoto(string id, string name);
        List<Comment> GetComments();
        void SaveComment(Comment comment);
        string GetUserName(string id);
        string GetStatus(string iD);
        void RequestAdminAccess(string iD);
    }
}
