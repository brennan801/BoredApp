using BoredShared.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BoredWebApp.Services
{
    public interface IBoredAPIService
    {
        Task<ActivityModel> GetRandomActivity();
        Task<ActivityModel> GetSpecificActivity(string Type, int? participants, double? price);
    }
}
