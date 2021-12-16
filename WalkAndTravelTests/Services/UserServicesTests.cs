using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WalkAndTravel.ClassLibrary.Repositories;
using WalkAndTravel.ClassLibrary.Services;
using WalkAndTravel.Models;
using Xunit;

namespace WalkAndTravelTests.Services
{
    public class UserServicesTests
    {
        [Fact]
        public async void UserServices_GetUsers_ReturnsUsers()
        {

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

            List<User> users = new List<User> { user1, user2, user3 };
            var repository = new Mock<IUserRepository>();
            repository.Setup(mr => mr.GetUsers()).ReturnsAsync(users);

            var service = new UserServices(repository.Object);
            var response = await service.GetUsers();
            var list = response.ToList();

            Assert.Equal(3, list.Count);
        }

        [Fact]
        public void UserServices_SaveNewUser_ReturnsResponse()
        {

            User user1 = new User();
            user1.Email = "mail@gmai.com";
            user1.Age = 20;
            user1.Name = "Kazimeras";
            user1.Nickname = "Kazys";
            user1.Password = "50F694693F42F1823689BCB4C2D994AF3DDB2D9F551E9DD4FC8727F4E79F5F4F";

            var repository = new Mock<IUserRepository>();
            repository.Setup(mr => mr.CreateNewUser(It.IsAny<User>())).Returns(1);

            var service = new UserServices(repository.Object);
            var response = service.CreateNewUser(user1);

            Assert.Equal(1, response);
        }

        [Fact]
        public async void UserServices_GetByEmail_ReturnsResponse()
        {

            User user1 = new User();
            user1.Email = "mail@gmai.com";
            user1.Age = 20;
            user1.Name = "Kazimeras";
            user1.Nickname = "Kazys";
            user1.Password = "50F694693F42F1823689BCB4C2D994AF3DDB2D9F551E9DD4FC8727F4E79F5F4F";

            var repository = new Mock<IUserRepository>();
            repository.Setup(mr => mr.GetByEmail(It.IsAny<string>())).ReturnsAsync(user1);

            var service = new UserServices(repository.Object);
            var response = await service.GetByEmail("mail@gmai.com");

            Assert.Equal("Kazimeras", response.Name);
        }

        [Fact]
        public async void UserServices_GetById_ReturnsResponse()
        {

            User user1 = new User();
            user1.Email = "mail@gmai.com";
            user1.Age = 20;
            user1.Name = "Kazimeras";
            user1.Nickname = "Kazys";
            user1.Password = "50F694693F42F1823689BCB4C2D994AF3DDB2D9F551E9DD4FC8727F4E79F5F4F";

            var repository = new Mock<IUserRepository>();
            repository.Setup(mr => mr.GetById(It.IsAny<int>())).ReturnsAsync(user1);

            var service = new UserServices(repository.Object);
            var response = await service.GetById(1);

            Assert.Equal("Kazimeras", response.Name);
        }
    }
}
