using BoredWebAppAdmin.Models;
using Dapper;
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
        public DatabaseService()
        {
            var connection = new NpgsqlConnection("User ID=admin;Password=password;Host=pgsql_db;Port=5432;Database=boredWebApp;");
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
                    "salt VARBINARY(max)," +
                    "hash VARCHAR(128));"
                    );
            }
        }

        public int GetLargestId()
        {
            var connection = new NpgsqlConnection("User ID=wgadmin;Password=wgadmin;Host=pgsql_db;Port=5432;Database=boredWebApp;");
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
            var connection = new NpgsqlConnection("User ID=wgadmin;Password=wgadmin;Host=pgsql_db;Port=5432;Database=boredWebApp;");

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
            var connection = new NpgsqlConnection("User ID=wgadmin;Password=wgadmin;Host=pgsql_db;Port=5432;Database=boredWebApp;");
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
