using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WalkAndTravel.ClassLibrary;
using WalkAndTravel.ClassLibrary.DTO;
using WalkAndTravel.ClassLibrary.Repositories;
using WalkAndTravel.ClassLibrary.Services;
using WalkAndTravel.DataAccess;
using WalkAndTravel.Models;
using WalkAndTravel.Services;

namespace WalkAndTravel.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UserController : ControllerBase
    {
        private readonly JwtService _jwtService;
        private readonly IUserServices _userServices;

        private readonly ILogger<UserController> _logger;

        // [Authorize]
        [HttpPost("farm")]
        public IActionResult FarmExp()
        {
            using(var context = new DataContext())
            {
                List<Task> tasks = new();
                //var user = context.Users.FirstOrDefault();
                var user = new User();
                var level = new Levels();
                level.Exp = user.Exp;
                level.Level = user.Level;
                foreach (var route in context.Routes)
                {

                    tasks.Add(level.CompleteTrail(route.Length));
                    

                }
                Task.WaitAll(tasks.ToArray());
                return Ok(level);
            }

        }

        public UserController(ILogger<UserController> logger)
        {
            _userServices = new UserServices(new UserRepository());
            _logger = logger;
        }

        [HttpPost("register")]
        public IActionResult Register(RegisterDto dto)
        {
            var user1 = _userServices.GetByEmail(dto.Email);
            if (user1 != null) return BadRequest(new { message = "User Already Exists" });

            var user = new User
            {
                Name = dto.Name,
                Email = dto.Email,
                Age = dto.Age,
                Surname = dto.Surname,
                Nickname = dto.Nickname,
                Level = dto.Level,
                Exp = dto.Exp,
                Password = BCrypt.Net.BCrypt.HashPassword(dto.Password)
            };
            _userServices.CreateNewUser(user);
            return Created("success", _userServices.CreateNewUser(user));
        }



        [HttpPost("login")]
        public async Task<IActionResult> Login(LoginDto dto)
        {
            var user = await _userServices.GetByEmail(dto.Email);
            if (user == null) return BadRequest(new { message = "Invalid Credentials" });
            if (!BCrypt.Net.BCrypt.Verify(dto.Password, user.Password))
            {
                return BadRequest(new { message = "Invalid Credentials" });
            }
            var jwt = _jwtService.Generate(user.Id);



            Response.Cookies.Append("jwt", jwt, new CookieOptions { HttpOnly = true });



            return Ok(new { jwt });
        }


        [Authorize]
        [HttpGet("user")]
        public IActionResult User()
        {
            var jwt = Request.Cookies["jwt"];
            var token = _jwtService.Verify(jwt);
            int userId = int.Parse(token.Issuer);
            var user = _userServices.GetById(userId);
            return Ok(user);
        }

        [Authorize]
        [HttpPost("logout")]
        public IActionResult Logout()
        {
            Response.Cookies.Delete("jwt");
            return Ok(new { message = "success" });
        }
    }
}
