using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WalkAndTravel.ClassLibrary.Repositories;
using WalkAndTravel.DataAccess;
using WalkAndTravel.Models;
using Xunit;

namespace WalkAndTravelTests.Repository
{
    class UserRepositoryTest
    {
        protected DbContextOptions<DataContext> ContextOptions { get; }
        protected UserRepositoryTest(DbContextOptions<DataContext> contextOptions)
        {
            ContextOptions = contextOptions;
            Seed();
        }

        private void Seed()
        {
            using (var context = new DataContext(ContextOptions))
            {
                context.Database.EnsureDeleted();
                context.Database.EnsureCreated();

                User user1 = new User();
                user1.Email = "mail@gmai.com";
                user1.Age = 20;
                user1.Name = "Kazimeras";
                user1.Nickname = "Kazys";
                user1.Password = "50F694693F42F1823689BCB4C2D994AF3DDB2D9F551E9DD4FC8727F4E79F5F4F";

                User user2 = new User();
                user2.Email = "badmail@two.com";
                user2.Name = "Jurgita";
                user2.Password = "9878A9C30DE5A9B48D2506D3628E4F4BA86FA5BC17244BE86406FEB68CCE1AC9";

                User user3 = new User();
                user3.Email = "ech@outbox.com";
                user3.Name = "Jin";
                user3.Password = "B571746DDC21C2C46A71711101B298DD4316E1EA1BEE7C0A128E354B7E5625C9";


                context.AddRange(user1, user2, user3);
                context.SaveChanges();
            }
        }

        public async void UserRepository_GetUsers_ReturnsCorrectAmountOfUsers()
        {
            using (var context = new DataContext(ContextOptions))
            {
                var repository = new UserRepository(context);

                var users = await repository.GetUsers();

                Assert.Equal(3, users.Count);

            }

        }

        public async void UserRepository_GetByEmail_ReturnsCorrectUser(string email, string name)
        {
            using (var context = new DataContext(ContextOptions))
            {
                var repository = new UserRepository(context);

                var user = await repository.GetByEmail(email);

                Assert.Equal(name, user.Name);
            }
        }

        public async void UserRepository_GetById_ReturnsCorrectUser(int id, string name)
        {
            using (var context = new DataContext(ContextOptions))
            {
                var repository = new UserRepository(context);

                var user = await repository.GetById(id);

                Assert.Equal(name, user.Name);
            }
        }

        public void UserRepository_DeleteUser_DeletesCorrectUser(int id, string name)
        {
            using (var context = new DataContext(ContextOptions))
            {
                var repository = new UserRepository(context);

                var user = repository.DeleteUser(id);

                Assert.Equal(name, user.Name);
            }
        }

        public void UserRepository_CreateNewUser_AddsCorrectUser()
        {
            using (var context = new DataContext(ContextOptions))
            {
                var repository = new UserRepository(context);

                var user = new User();
                user.Name = "Antanas";
                user.Email = "squarepizza@yamoo.com";
                user.Password = "B9488F5027319875F1644421AD4244E0BC78FB9379DC4AC6A9213B6992051CCB";

                int res = repository.CreateNewUser(user);

                Assert.NotEqual(-1, res);
            }
        }
    }
}

