using LogicLayer.Interfaces;

namespace LogicLayer.Models
{
    public class User
    {
        public int Id { get; set; }
        public string firstName { get; set; }
        public string lastName { get; set; }
        public string emailAdress { get; set; }
        public string Password { get; set; }
        public string userName { get; set; }
        public List<MovieToWatch> watchList { get; set; }

        public User()
        {
            watchList = new List<MovieToWatch>();
        }

        public bool CheckIfUserExistInDatabase(int userId, IUserRepository repository)
        {
            if (repository.checkUserIdExist(userId) > 0)
            {
                return true;
            }
            return false;
        }
    }
}
