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
        public IEnumerable<MovieToWatch> ToWatches(int userId)
        {
            var toWatch = new List<MovieToWatch>();
            using (var conn = new SqlConnection(_connectionString))
            {
                conn.Open();
                 string query = @"
                    SELECT m.Id, m.Name, ws.StatusNaam, wl.StatusId, wl.UserId
                    FROM Movies m
                    INNER JOIN WatchLists wl ON wl.MovieId = m.Id
                    INNER JOIN WatchStatus ws ON wl.StatusId = ws.StatusId
                    WHERE wl.UserId = @userId;
                ";

                using (var cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.Add(new SqlParameter("@userId", System.Data.SqlDbType.Int) { Value = userId });
                    using (var reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            var movieToWatch = new MovieToWatch(
                                reader.GetString(reader.GetOrdinal("Name")),
                                reader.GetInt32(reader.GetOrdinal("Id")),
                                reader.GetString(reader.GetOrdinal("StatusNaam"))
                            );
                            toWatch.Add(movieToWatch);
                        }
                    }
                }
            }
            return toWatch;
        }
    }
}
