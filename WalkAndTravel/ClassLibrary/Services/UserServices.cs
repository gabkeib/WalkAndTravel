using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WalkAndTravel.ClassLibrary.Repositories;
using WalkAndTravel.Models;

namespace WalkAndTravel.ClassLibrary.Services
{
    public class UserServices : IUserServices
    {

        private IUserRepository _userRepository;

        public UserServices(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public int CreateNewUser(User user)
        {
            return _userRepository.CreateNewUser(user);
        }

        public Task<User> GetByEmail(string email)
        {
            return _userRepository.GetByEmail(email);
        }

        public Task<User> GetById(int id)
        {
            return _userRepository.GetById(id);
        }
        public Task<User> EarnExp(int id, int exp)
        {
            return _userRepository.EarnExp(id, exp);
        }
    }
}
