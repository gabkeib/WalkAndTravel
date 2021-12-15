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
        private DataContext context;
        public UserRepository(DataContext context)
        {
            this.context = context;
        }
        public int CreateNewUser(User user)
        {
            System.Diagnostics.Debug.WriteLine("here");
            System.Diagnostics.Debug.WriteLine(user);
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

        public User DeleteUser(int Id)
        {
            var userDelete = context.Users.FirstOrDefault(e => e.Id == Id);
            var user = context.Users.Remove(userDelete);
            context.SaveChanges();
            return userDelete;
        }

        public async Task<User> GetByEmail(string email)
        {
            return await context.Users.FirstOrDefaultAsync(e => e.Email == email);
        }
        public async Task<User> GetById(int id)
        {
            return await context.Users.FirstOrDefaultAsync(e => e.Id == id);
        }

        public async Task<List<User>> GetUsers()
        {
            return await context.Users.ToListAsync();
        }
        public async Task<User> EarnExp(int id, int exp)
        {
            return await context.Users.ToListAsync();
                var user = await context.Users.FirstOrDefaultAsync(e => e.Id == id);
                Levels level = new Levels();
                level.Exp = user.Exp;
                level.Level = user.Level;
                level.AddExperience(exp);
                user.Exp = level.Exp;
                user.Level = level.Level;
                context.Users.Update(user);
                context.SaveChanges();
                return user;
        }
    }
}
