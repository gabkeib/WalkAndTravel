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
        public void UserServices_SaveNewUser_ReturnsResponse()
        {

            User user1 = new User();
            user1.Email = "mail@gmai.com";
            user1.Age = 20;
            user1.Name = "Kazimeras";
            user1.Username = "Kazys";
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
            user1.Username = "Kazys";
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
            user1.Username = "Kazys";
            user1.Password = "50F694693F42F1823689BCB4C2D994AF3DDB2D9F551E9DD4FC8727F4E79F5F4F";

            var repository = new Mock<IUserRepository>();
            repository.Setup(mr => mr.GetById(It.IsAny<int>())).ReturnsAsync(user1);

            var service = new UserServices(repository.Object);
            var response = await service.GetById(1);

            Assert.Equal("Kazimeras", response.Name);
        }
    }
}
