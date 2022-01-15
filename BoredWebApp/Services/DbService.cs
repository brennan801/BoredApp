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
        public DbService()
        {
            var connection = new NpgsqlConnection("User ID=admin;Password=password;Host=pgsql_db;Port=5432;Database=boredWebApp;");
            using (connection)
            {
                connection.Execute(
                    "CREATE TABLE IF NOT EXISTS SavedActivities(" +
                    "activity VARCHAR(128)," +
                    "type VARCHAR(32)," +
                    "participants INTEGER," +
                    "price FLOAT," +
                    "link VARCHAR(256)," +
                    "key VARCHAR(256)," +
                    "accessibility FLOAT," +
                    "error VARCHAR(256))"
                    );
            }
        }
        public List<ActivityModel> getSavedActivities()
        {
            var connection = new NpgsqlConnection("User ID=admin;Password=password;Host=pgsql_db;Port=5432;Database=boredWebApp;");

            //List<ActivityModel> savedActivities = new();
            using (connection)
            {
                var savedActivities =  connection.Query<ActivityModel>(
                    "SELECT * FROM SavedActivities"
                    );
                return (List<ActivityModel>)savedActivities;
            }
        }

        public void SaveActivity(ActivityModel activity)
        {
            ActivityModel fakeActivity = new ActivityModel()
            {
                Activity = "Here is a fake one for the database",
                Type = "Fake",
                Participants = 0,
                Price = 0
            };
            var connection = new NpgsqlConnection("User ID=admin;Password=password;Host=pgsql_db;Port=5432;Database=boredWebApp;");

            try
            {
                using (connection)
                {
                    connection.Execute(
                        "INSERT INTO SavedActivities " +
                        "VALUES (@Activity, @Type, @Participants, @Price, @Link, @Key, @Accessibility, @Error);",
                        fakeActivity
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
