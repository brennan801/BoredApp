using BoredWebAppAdmin.Models;
using Dapper;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Primitives;
using Npgsql;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BoredWebAppAdmin.Services
{
    public class DatabaseService : IDatabaseService
    {
        private readonly IConfiguration config;
        public string psqlString { get; set; }


        public DatabaseService(IConfiguration config)
        {
            psqlString = Environment.GetEnvironmentVariable("psqldb");

            var connection = new NpgsqlConnection(psqlString);
            using (connection)
            {
                connection.Execute(
                    "CREATE TABLE IF NOT EXISTS Clients(" +
                    "id int," +
                    "client VARCHAR(128)," +
                    "ipAddress VARCHAR(128)," +
                    "dateAdded VARCHAR(128)," +
                    "allowedIpRange VARCHAR(128)," +
                    "clientPublicKey VARCHAR(128)," +
                    "clientPrivateKey VARCHAR(128));"
                    );

                connection.Execute(
                    "CREATE TABLE IF NOT EXISTS Users(" +
                    "userName VARCHAR(32)," +
                    "salt bytea," +
                    "hash VARCHAR(128));"
                    );
            }

            this.config = config;
        }

        public ClientInformation GetClient(StringValues clientID)
        {
            var connection = new NpgsqlConnection(config.GetValue<string>("wgadmin"));
            var dictionary = new Dictionary<string, object>
            {
                { "@ID", clientID }
            };
            var parameters = new DynamicParameters(dictionary);

            using (connection)
            {
                ClientInformation client = connection.QuerySingle<ClientInformation>(
                    "SELECT * FROM Clients WHERE id = @ID",
                    parameters);
                return client;
            }
        }

        public int GetLargestId()
        {
            var connection = new NpgsqlConnection(config.GetValue<string>("wgadmin"));
            int id;
            try
            {
                using (connection)
                {
                    id = connection.Query<int>( "SELECT max(id) from Clients").ToList().First();
                    
                }
            }
            catch (Npgsql.PostgresException e)
            {
                throw new Exception("Error Saving Client Info: " + e.Message);
            }
            return id;
        }

        public void SaveClientInformation(ClientInformation clientInformation)
        {
            var connection = new NpgsqlConnection(config.GetValue<string>("wgadmin"));
            var dictionary = new Dictionary<string, object>
            {
                { "@ID", clientInformation.ID.Value },
                { "@ClientName", clientInformation.ClientName.Value },
                { "@IPAddress", clientInformation.IpAddress.Value },
                { "@DateAdded", clientInformation.DateAdded },
                { "@AllowedIPRange", clientInformation.AllowedIpRange },
                { "@ClientPublicKey", clientInformation.ClientPublicKey },
                { "@ClientPrivateKey", clientInformation.ClientPrivateKey }
            };
            var parameters = new DynamicParameters(dictionary);
            try
            {
                using (connection)
                {
                    connection.Execute(
                        "INSERT INTO Clients " +
                        "VALUES (@ID, @ClientName, @IPAddress, @DateAdded, @AllowedIPRange, @ClientPublicKey, @ClientPrivateKey);",
                        parameters
                        );
                }
            }
            catch (Npgsql.PostgresException e)
            {
                throw new Exception("Error Saving Client Info: " + e.Message);
            }
        }

        public void SaveNewUser(User newUser)
        {
            var connection = new NpgsqlConnection(config.GetValue<string>("wgadmin"));
            var dictionary = new Dictionary<string, object>
            {
                { "@UserName", newUser.UserName.Value },
                { "@Salt",  newUser.Salt },
                { "@Hash", newUser.Hash }
            };
            var parameters = new DynamicParameters(dictionary);
            try
            {
                using (connection)
                {
                    connection.Execute(
                        "INSERT INTO Users " +
                        "VALUES (@UserName, @Salt, @Hash);",
                        parameters
                        );
                }
            }
            catch (Npgsql.PostgresException e)
            {
                throw new Exception("Error Saving Client Info: " + e.Message);
            }
        }

        public List<BoredShared.Models.User> GetUsers()
        {
           
            var connection = new NpgsqlConnection(config.GetValue<string>("psqldb"));

            //List<ActivityModel> savedActivities = new();
            using (connection)
            {
                var savedActivities = connection.Query<BoredShared.Models.User>(
                    "SELECT * FROM SavedActivities"
                    );
                return (List<BoredShared.Models.User>)savedActivities;
            }
        }
    }
}
