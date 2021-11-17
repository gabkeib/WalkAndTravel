using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WalkAndTravel.Models;

namespace WalkAndTravel.ClassLibrary.Repositories
{
    public interface IUserRepository
    {
        Task<List<User>> GetUsers();

        int CreateNewUser(User user);

        Task<User> GetByEmail(string email);

        Task<User> GetById(int id);
    }
}
