using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;
using Moq;
using WalkAndTravel.ClassLibrary.Services;
using WalkAndTravel.Models;
using WalkAndTravel.Controllers;
using WalkAndTravel.ClassLibrary.DTO;
using Microsoft.AspNetCore.Mvc;

namespace WalkAndTravelTests.Controllers
{
    public class UserControllerTests
    {
        [Fact]
        public async void UserController_Register_CreatesUser()
        {
            var userService = new Mock<IUserServices>();

            userService.Setup(ms => ms.GetByEmail(It.IsAny<string>())).ReturnsAsync((User)null);
            userService.Setup(ms => ms.CreateNewUser(It.IsAny<User>())).Returns(1);

            var controller = new UserController(userService.Object);

            RegisterDto user2 = new RegisterDto();
            user2.Email = "badmail@two.com";
            user2.Name = "Jurgita";
            user2.Password = "9878A9C30DE5A9B48D2506D3628E4F4BA86FA5BC17244BE86406FEB68CCE1AC9";

            var response = await controller.Register(user2);
           
            var okObjectResult = response as CreatedResult;
            Assert.NotNull(okObjectResult);

            var actual = okObjectResult.Value;
            Assert.Equal(1, actual);
        }

        [Fact]
        public async void UserController_Register_CreatesSameUser()
        {
            var userService = new Mock<IUserServices>();

            User user3 = new User();
            user3.Email = "ech@outbox.com";
            user3.Name = "Jin";
            user3.Password = "B571746DDC21C2C46A71711101B298DD4316E1EA1BEE7C0A128E354B7E5625C9";

            userService.Setup(ms => ms.GetByEmail(It.IsAny<string>())).ReturnsAsync(user3);
            userService.Setup(ms => ms.CreateNewUser(It.IsAny<User>())).Returns(1);

            var controller = new UserController(userService.Object);

            RegisterDto user2 = new RegisterDto();
            user2.Email = "ech@outbox.com";
            user2.Name = "Jin";
            user2.Password = "B571746DDC21C2C46A71711101B298DD4316E1EA1BEE7C0A128E354B7E5625C9";

            var response = await controller.Register(user2);

            Assert.IsType<BadRequestObjectResult>(response);
        }

       
    }
}
