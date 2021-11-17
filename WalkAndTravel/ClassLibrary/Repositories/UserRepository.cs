using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WalkAndTravel.DataAccess;
using WalkAndTravel.Models;

namespace WalkAndTravel.ClassLibrary.Repositories
{
    public class UserRepository : IUserRepository
    {

        public int CreateNewUser(User user)
        {
            System.Diagnostics.Debug.WriteLine("here");
            System.Diagnostics.Debug.WriteLine(user);
            using (var context = new DataContext())
            {
                int id = 0;
                context.Users.Add(user);
                try 
                {
                    context.SaveChanges();
                }
                catch (DbUpdateException e)
                {
                    return -1;
                }

                foreach (var _user in context.Users)
                {
                    if (_user.Email == user.Email)
                    {
                        id = _user.Id;
                        break;
                    }
                }
                //Log(this, new ClassLibrary.Logging.LogEventArgs("Save route", "Custom", newRoute.Name));
                return id;
            }
        }

        public void DeleteUser(int Id)
        {
            using (var context = new DataContext())
            {
                var userDelete = context.Users.FirstOrDefault(e => e.Id == Id);
                context.Users.Remove(userDelete);
                context.SaveChanges();
            }
        }

        public async Task<User> GetByEmail(string email)
        {
            using(var context = new DataContext())
            {
                return await context.Users.FirstOrDefaultAsync(e => e.Email == email);
            }
        }
        public async Task<User> GetById(int id)
        {
            using (var context = new DataContext())
            {
                return await context.Users.FirstOrDefaultAsync(e => e.Id == id);
            }
        }

        public Task<List<User>> GetUsers()
        {
            throw new NotImplementedException();
        }
    }
}
