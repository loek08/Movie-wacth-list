
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using LogicLayer.Interfaces;
using LogicLayer.Models;


namespace DataLayer.Repositories
{
    public class MovieRepository : IMovieRepository
    {
        private readonly string _connectionString;

        public MovieRepository(IConfiguration config)
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
        
        public IEnumerable<Movie> GetMovies()
        {
            var movies = new List<Movie>();
            using (var conn = new SqlConnection(_connectionString))
            {
                conn.Open();
                string query = "SELECT * FROM Movies";
                using (var cmd = new SqlCommand(query, conn))
                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        var movie = new Movie
                        {
                            Id = reader.GetInt32(reader.GetOrdinal("Id")),
                            Name = reader.GetString(reader.GetOrdinal("Name")),
                            ReleaseYear = reader.GetInt32(reader.GetOrdinal("ReleaseYear")),
                            Info = reader.GetString(reader.GetOrdinal("Info"))
                        };
                        movies.Add(movie);
                    }
                }
            }
            return movies;
        }


    }
}
