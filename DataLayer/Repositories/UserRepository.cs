using System;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using LogicLayer.Interfaces;
using LogicLayer.Models;

namespace DataLayer.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly string _connectionString;

        public UserRepository(IConfiguration config)
        {
            if (config == null) throw new ArgumentNullException(nameof(config));

            // Try the app's configured name first, then a known alternative.
            _connectionString = config.GetConnectionString("DefaultConnection")
                             ?? config.GetConnectionString("MovieList");

            if (string.IsNullOrWhiteSpace(_connectionString))
            {
                throw new InvalidOperationException("Connection string 'DefaultConnection' or 'MovieList' not found in configuration.");
            }
        }

        public SqlConnection CreateConnection()
        {
            return new SqlConnection(_connectionString);
        }

        public IEnumerable<User> GetUsers()
        {
            var users = new List<User>();
            using (var conn = new SqlConnection(_connectionString))
            {
                conn.Open();
                string query = "SELECT * FROM Users";
                using (var cmd = new SqlCommand(query, conn))
                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        var user = new User
                        {
                            Id = reader.GetInt32(reader.GetOrdinal("Id")),
                            firstName = reader.GetString(reader.GetOrdinal("firstName")),
                            lastName = reader.GetString(reader.GetOrdinal("lastName")),
                            emailAdress = reader.GetString(reader.GetOrdinal("emailAdress")),
                            Password = reader.GetString(reader.GetOrdinal("Password")),
                            userName = reader.GetString(reader.GetOrdinal("userName"))
                        };
                        users.Add(user);
                    }
                }
            }
            return users;
        }
    }
}
