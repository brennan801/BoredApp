using BoredShared.Models;
using BoredWebApp.Models;
using Dapper;
using Microsoft.Extensions.Configuration;
using Npgsql;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BoredWebApp.Services
{
    public class DbService : IDBService
    {
        private readonly IConfiguration config;
        public string psqlString { get; set; }

        public DbService(IConfiguration config)
        {
            psqlString = Environment.GetEnvironmentVariable("psqldb");

            var connection = new NpgsqlConnection(psqlString);
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
                connection.Execute(
                    "CREATE TABLE IF NOT EXISTS UserCookies(" +
                    "username VARCHAR(128) PRIMARY KEY," +
                    "cookie VARCHAR(32));"
                    );
            }

            this.config = config;
        }

        public string GetCookieValue(string userName)
        {
            var connection = new NpgsqlConnection(config.GetValue<string>("psqldb"));
            var dictionary = new Dictionary<string, object>
            {
                { "@UserName", userName }
            };
            var parameters = new DynamicParameters(dictionary);
            try
            {
                using (connection)
                {
                    var cookie = connection.QuerySingle<string>(
                        "SELECT cookie FROM UserCookies WHERE userName = @UserName;",
                        parameters);
                    return cookie;
                }
            }
            catch (InvalidOperationException e)
            {
                throw new InvalidOperationException(e.Message);
            }
        }

        public string GetHash(string userName)
        {
            var connection = new NpgsqlConnection(config.GetValue<string>("psqldb"));

            var dictionary = new Dictionary<string, object>
            {
                { "@UserName", userName }
            };
            var parameters = new DynamicParameters(dictionary);

            try
            {
                using (connection)
                {
                    var hashed = connection.QuerySingle<string>(
                        "SELECT hash FROM Users WHERE userName = @UserName;",
                        parameters);
                    return hashed;
                }
            }
            catch (InvalidOperationException e)
            {
                throw new InvalidOperationException(e.Message);
            }
        }

        public byte[] GetSalt(string userName)
        {
            var connection = new NpgsqlConnection(config.GetValue<string>("psqldb"));

            var dictionary = new Dictionary<string, object>
            {
                { "@UserName", userName }
            };
            var parameters = new DynamicParameters(dictionary);

            try
            {
                using (connection)
                {
                    var salt = connection.QuerySingle<byte[]>(
                        "SELECT salt FROM Users WHERE userName = @UserName;",
                        parameters);
                    return salt;
                }
            }
            catch (InvalidOperationException e)
            {
                throw new InvalidOperationException(e.Message);
            }
        }

        public List<ActivityModel> getSavedActivities()
        {
            var connection = new NpgsqlConnection(config.GetValue<string>("psqldb"));

            //List<ActivityModel> savedActivities = new();
            using (connection)
            {
                var savedActivities =  connection.Query<ActivityModel>(
                    "SELECT * FROM SavedActivities"
                    );
                return (List<ActivityModel>)savedActivities;
            }
        }

        public void RemoveCookie(string userName)
        {
            var connection = new NpgsqlConnection(config.GetValue<string>("psqldb"));

            var dictionary = new Dictionary<string, object>
            {
                { "@UserName", userName }
            };
            var parameters = new DynamicParameters(dictionary);
           
            using (connection)
            {
                connection.Execute("UPDATE UserCookies SET cookie = '' WHERE userName = @userName;", parameters);
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
            var connection = new NpgsqlConnection(config.GetValue<string>("psqldb"));

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

        public void SaveCookie(UserCookie userCookie)
        {
            var connection = new NpgsqlConnection(config.GetValue<string>("psqldb"));
            try
            {
                using (connection)
                {
                    connection.Execute(
                        "INSERT INTO UserCookies " +
                        "VALUES (@UserName, @Cookie) " +
                        "ON CONFLICT (username) DO UPDATE SET cookie = Excluded.cookie;",
                        userCookie
                        );
                }
            }
            catch (Npgsql.PostgresException e)
            {
                throw new Exception("Error Saving Activity: " + e.Message);
            }
        }
    }
}
