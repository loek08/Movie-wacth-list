using System;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using LogicLayer.Interfaces;
using LogicLayer.Models;
using System.Net.Mime;

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
        // dit is een uitleg dat ik IEnumerable een array kan maken
        //public IEnumerable<User> GetArrayUser()
        //{
        //    User[] users = Array.Empty<User>();
        //    users = (User[])GetUsers();
        //    return users;
        //}

        public void AddUser(User user)
        {
            using (var conn = new SqlConnection(_connectionString))
            {
                conn.Open();
                string query = "INSERT INTO Users (firstName, lastName, emailAdress, Password, userName) " +
                                "VALUES (@FirstName, @LastName, @EmailAdress, @Password, @UserName)";
                using (var cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@FirstName", user.firstName);
                    cmd.Parameters.AddWithValue("@LastName", user.lastName);
                    cmd.Parameters.AddWithValue("@EmailAdress", user.emailAdress);
                    cmd.Parameters.AddWithValue("@Password", user.Password);
                    cmd.Parameters.AddWithValue("@UserName", user.userName);
                    cmd.ExecuteNonQuery();
                }
            }
        }

        public int checkUserIdExist(int userId)
        {
            using (var conn = new SqlConnection(_connectionString))
            {
                conn.Open();
                using var cmd = conn.CreateCommand();
                cmd.CommandText = "SELECT CASE WHEN EXISTS(SELECT 1 FROM Users WHERE Id = @Id) THEN 1 ELSE 0 END";
                cmd.Parameters.AddWithValue("@Id", userId);
                var result = cmd.ExecuteScalar();
                return Convert.ToInt32(result);
            }
        }
    }
}