using BoredShared.Models;
using Dapper;
using Npgsql;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BoredWebApp.Services
{
    public class DbService : IDBService
    {
        private readonly NpgsqlConnection connection;
        public DbService()
        {
            connection = new NpgsqlConnection("User ID=admin;Password=admin1234;Host=localhost;Port=5432;Database=boredWebApp;");
        }
        public List<ActivityModel> getSavedActivities()
        {
            List<ActivityModel> savedActivities = new();
            using (connection)
            {
                savedActivities = (List<ActivityModel>)connection.Query<List<ActivityModel>>(
                    "SELECT * FROM SavedActivities"
                    );
            }
            return savedActivities;
        }

        public void SaveActivity(ActivityModel activity)
        {
            try
            {
                using (connection)
                {
                    connection.Execute(
                        "INSERT INTO SavedActivities" +
                        "VALUES (@activity, @type, @participants, @price, @link, @key, @accessibility, @error);",
                        activity
                        );
                }
            }
            catch(Npgsql.PostgresException e)
            {
                throw new Exception("Error Saving Activity: " + e.Message);
            }
        }
    }
}
