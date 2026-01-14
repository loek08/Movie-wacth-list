using System;
using System.Collections.Generic;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using LogicLayer.Interfaces;
using LogicLayer.Models;

namespace DataLayer.Repositories
{
    public class ReviewRepository
    {
        private readonly string _connectionString;

        public ReviewRepository(IConfiguration config)
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

        public IEnumerable<Review> GetReviews()
        {
            var reviews = new List<Review>();
            using (var conn = new SqlConnection(_connectionString))
            {
                conn.Open();
                string query = "SELECT Id, MovieId, UserId, Rating, Comment FROM Reviews";
                using (var cmd = new SqlCommand(query, conn))
                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        var review = new Review
                        {
                            Rating = reader.GetInt32(reader.GetOrdinal("Rating")),
                            Comment = reader.GetString(reader.GetOrdinal("Comment"))
                        };
                        reviews.Add(review);
                    }
                }
            }
            return reviews;
        }

    }
}
