using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LogicLayer.Models;

namespace LogicLayer.Interfaces
{
    public interface IUserRepository
    {
        IEnumerable<User> GetUsers();
    }
}
