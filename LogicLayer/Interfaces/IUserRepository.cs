
using LogicLayer.Models;

namespace LogicLayer.Interfaces
{
    public interface IUserRepository
    {
        IEnumerable<User> GetUsers();

        void AddUser(User user);    
    }
}
