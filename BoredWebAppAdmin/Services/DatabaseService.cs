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

        public DatabaseService(IConfiguration config)
        {
            var connection = new NpgsqlConnection(config.GetConnectionString("psqldb"));
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

        public int GetLargestId()
        {
            var connection = new NpgsqlConnection(config.GetConnectionString("wgAdmin"));
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
            var connection = new NpgsqlConnection(config.GetConnectionString("wgAdmin"));

            try
            {
                using (connection)
                {
                    connection.Execute(
                        "INSERT INTO Clients " +
                        "VALUES (@ID, @ClientName, @IPAddress, @DateAdded, @AllowedIPRange, @ClientPublicKey, @ClientPrivateKey);",
                        clientInformation
                        );
                }
            }
            catch (Npgsql.PostgresException e)
            {
                throw new Exception("Error Saving Client Info: " + e.Message);
            }
        }

        public void SaveNewUser(NewUser newUser)
        {
            var connection = new NpgsqlConnection(config.GetConnectionString("wgAdmin"));
            try
            {
                using (connection)
                {
                    connection.Execute(
                        "INSERT INTO Users " +
                        "VALUES (@UserName, @Salt, @Hashed);",
                        newUser
                        );
                }
            }
            catch (Npgsql.PostgresException e)
            {
                throw new Exception("Error Saving Client Info: " + e.Message);
            }
        }
    }
}
