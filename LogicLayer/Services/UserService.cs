using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LogicLayer.Interfaces;
using LogicLayer.Models;

namespace LogicLayer.Services
{
    public class UserService
    {
      
        private readonly IUserRepository _userRepository;   

        public UserService(IUserRepository userRepository)
        {
            _userRepository = userRepository ?? throw new ArgumentNullException(nameof(userRepository));
        }

        public IEnumerable<User> GetAllUsers()
        {
            return _userRepository.GetUsers();
        }

        public IEnumerable<User> GetUsers() 
        {
            return _userRepository.GetUsers();
        }

        public bool checkUserId(int userId )
        {
            if (userId < 0)
            {
                throw new ArgumentException("Invalid user ID.");
            }

            var user = new User();
            var result = user.CheckIfUserExistInDatabase(userId, _userRepository);

            return result;
        }


    }
}
