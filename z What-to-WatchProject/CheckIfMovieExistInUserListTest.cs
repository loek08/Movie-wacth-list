using System.Collections.Generic;
using System.Linq;
using LogicLayer.Models;
using LogicLayer.Services;
using Moq;
using Xunit;


namespace z_What_to_WatchProject
{
    public class CheckIfMovieExistInUserListTest
    {
        [Fact]
        public void MovieisNotInListCanBeAddedToList()
        {
            // Arrange
            var mockMovieRepository = new Mock<LogicLayer.Interfaces.IMovieRepository>();
            int userId = 1;
            int movieId = 2;
            // Simulate that the movie is not in the testUser's watch list
            mockMovieRepository.Setup(repo => repo.ToWatches(userId))
                .Returns(new List<MovieToWatch>
                {
                    new MovieToWatch("Dark knight", 1, "want to watch")
                });
            var movieRepository = mockMovieRepository.Object;

            // Create a test user instance
            var testUser = new User
            {
                Id = userId,
                firstName = "Test",
                lastName = "User",
                emailAdress = "test@example.com",
                Password = "password",
                userName = "testuser"
            };

            // Create an instance of the model and use its method under test
            var movieToWatchModel = new MovieToWatch("the super mario bros. movie", 2, "want to watch");

            // Act
            var movieService = new MovieService(movieRepository);
            bool movieExists = testUser.HasMovieToWatch(movieId, userId, movieService);

            // Assert
            Assert.True(movieExists, "Movie should not exist in the testUser's watch list and can be added.");
        }
        //    //[Fact]
        //    //public void MovieIsInList_CannotBeAddedToList()
        //    //{
        //    //    // Arrange
        //    //    var mockMovieRepository = new Mock<LogicLayer.Interfaces.IMovieRepository>();
        //    //    int userId = 1;
        //    //    int movieId = 2;
        //    //    // Simulate that the movie is not in the testUser's watch list
        //    //    mockMovieRepository.Setup(repo => repo.ToWatches(userId))
        //    //        .Returns(new List<MovieToWatch>
        //    //        {
        //    //            new MovieToWatch("the super mario bros. movie", 2, "want to watch")
        //    //        });
        //    //    var movieRepository = mockMovieRepository.Object;

        //    //    // Create an instance of the model and use its method under test
        //    //    var movieToWatchModel = new MovieToWatch("the super mario bros. movie", 2, "want to watch");

        //    //    // Act
        //    //    bool movieExists = movieToWatchModel.CheckIfMovieExistInUserList(userId, movieId, movieRepository);

        //    //    // Assert
        //    //    Assert.True(movieExists, "Movie should not exist in the testUser's watch list and can be added.");
        //    //}

        //}
    }
}