using BoredShared.Models;
using BoredWebApp.Models;
using Dapper;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Primitives;
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
                connection.Execute(
                   "CREATE TABLE IF NOT EXISTS Users(" +
                   "ID VARCHAR(128) PRIMARY KEY," +
                   "name VARCHAR(128)," +
                   "photo VARCHAR(256)," +
                   "status VARCHAR(32));" 
                   );
                connection.Execute(
                    "CREATE TABLE IF NOT EXISTS Comments(" +
                    "UserName VARCHAR(32)," +
                    "Date VARCHAR(128)," +
                    "Body TEXT);"
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
                connection.Execute("UPDATE UserCookies SET cookie = '' WHERE userName = @UserName;", parameters);
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
            var dictionary = new Dictionary<string, object>
            {
                { "@UserName", userCookie.UserName.Value },
                { "@Value",  userCookie.Value.Value }
            };
            var parameters = new DynamicParameters(dictionary);
            try
            {
                using (connection)
                {
                    connection.Execute(
                        "INSERT INTO UserCookies " +
                        "VALUES (@UserName, @Value) " +
                        "ON CONFLICT (UserName) DO UPDATE SET cookie = Excluded.cookie;",
                        parameters
                        );
                }
            }
            catch (Npgsql.PostgresException e)
            {
                throw new Exception("Error Saving UserCookie: " + e.Message);
            }
        }


        public void AddUser(string ID)
        {
            var connection = new NpgsqlConnection(config.GetValue<string>("psqldb"));
            var dictionary = new Dictionary<string, object>
            {
                { "@ID", ID }
            };
            var parameters = new DynamicParameters(dictionary);
            try
            {
                using (connection)
                {
                    connection.Execute(
                        "INSERT INTO Users (ID, status) " +
                        "VALUES (@ID, 'New') " +
                        "ON CONFLICT (ID) DO NOTHING; ",
                        parameters
                        );
                }
            }
            catch (Npgsql.PostgresException e)
            {
                throw new Exception("Error Saving Users: " + e.Message);
            }
        }

        public void SaveNameAndPhoto(string id, string name)
        {
            var connection = new NpgsqlConnection(config.GetValue<string>("psqldb"));
            var dictionary = new Dictionary<string, object>
            {
                { "@ID", id },
                { "@Name", name }
            };
            var parameters = new DynamicParameters(dictionary);
            try
            {
                using (connection)
                {
                    connection.Execute(
                        "UPDATE Users " +
                        "SET name = @Name " +
                        "Where ID = @ID;",
                        parameters
                        );
                }
            }
            catch (Npgsql.PostgresException e)
            {
                throw new Exception("Error Saving name and photo: " + e.Message);
            }
        }

        public List<Comment> GetComments()
        {
            var connection = new NpgsqlConnection(config.GetValue<string>("psqldb"));
            using (connection)
            {
                var comments = connection.Query<Comment>(
                    "SELECT * FROM Comments;"
                    );
                return (List<Comment>)comments;
            }
        }

        public void SaveComment(Comment comment)
        {
            var connection = new NpgsqlConnection(config.GetValue<string>("psqldb"));
            var dictionary = new Dictionary<string, object>
            {
                { "@User", comment.UserName },
                { "@Date", comment.Date },
                { "@Body", comment.Body }
            };
            var parameters = new DynamicParameters(dictionary);
            try
            {
                using (connection)
                {
                    connection.Execute(
                        "INSERT INTO Comments " +
                        "VALUES (@User, @Date, @Body);",
                        parameters
                        );
                }
            }
            catch (Npgsql.PostgresException e)
            {
                throw new Exception("Error Saving Users: " + e.Message);
            }
        }

        public string GetUserName(string id)
        {
            var connection = new NpgsqlConnection(config.GetValue<string>("psqldb"));

            var dictionary = new Dictionary<string, object>
            {
                { "@ID", id }
            };
            var parameters = new DynamicParameters(dictionary);

            try
            {
                using (connection)
                {
                    var userName = connection.QuerySingle<string>(
                        "SELECT name FROM users WHERE ID = @ID;",
                        parameters);
                    return userName;
                }
            }
            catch (InvalidOperationException e)
            {
                throw new InvalidOperationException(e.Message);
            }
        }

        public string GetStatus(string id)
        {
            var connection = new NpgsqlConnection(config.GetValue<string>("psqldb"));

            var dictionary = new Dictionary<string, object>
            {
                { "@ID", id }
            };
            var parameters = new DynamicParameters(dictionary);

            try
            {
                using (connection)
                {
                    var userName = connection.QuerySingle<string>(
                        "SELECT status FROM users WHERE ID = @ID;",
                        parameters);
                    return userName;
                }
            }
            catch (InvalidOperationException e)
            {
                throw new InvalidOperationException(e.Message);
            }
        }

        public void RequestAdminAccess(string id)
        {
            Console.WriteLine($"ID: {id}");
            var connection = new NpgsqlConnection(config.GetValue<string>("psqldb"));
            var dictionary = new Dictionary<string, object>
            {
                { "@ID", id },
                { "@Status", "Requested" }
            };
            var parameters = new DynamicParameters(dictionary);
            try
            {
                using (connection)
                {
                    connection.Execute(
                        "UPDATE users " +
                        "SET status = @Status " +
                        "Where ID = @ID;",
                        parameters
                        );
                }
            }
            catch (Npgsql.PostgresException e)
            {
                throw new Exception("Error updating user status: " + e.Message);
            }
        }
    }
}
