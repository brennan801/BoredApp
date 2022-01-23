using BoredWebAppAdmin.Models;
using Dapper;
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
                    "client VARCHAR(128)," +
                    "ipAddress VARCHAR(128)," +
                    "dateAdded DATETIME," +
                    "allowedIpRange VARCHAR(128)," +
                    "clientPublicKey VARCHAR(128)," +
                    "clientPrivateKey VARCHAR(128))"
                    );
            }
        }
        public void SaveClientInformation(ClientInformation clientInformation)
        {
            var connection = new NpgsqlConnection("User ID=admin;Password=password;Host=pgsql_db;Port=5432;Database=boredWebApp;");

            try
            {
                using (connection)
                {
                    connection.Execute(
                        "INSERT INTO Clients " +
                        "VALUES (@ClientName, @IPAddress, @DateAdded, @AllowedIPRange, @ClientPublicKey, @ClientPrivateKey);",
                        clientInformation
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
