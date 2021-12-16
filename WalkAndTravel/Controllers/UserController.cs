using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;
using WalkAndTravel.ClassLibrary;
using WalkAndTravel.ClassLibrary.DTO;
using WalkAndTravel.ClassLibrary.Models;
using WalkAndTravel.ClassLibrary.Repositories;
using WalkAndTravel.ClassLibrary.Services;
using WalkAndTravel.DataAccess;
using WalkAndTravel.Models;
using WalkAndTravel.Services;
using System.Security.Claims;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;


namespace WalkAndTravel.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UserController : ControllerBase
    {
        private readonly JwtService _jwtService;
        private readonly IUserServices _userServices;

        public UserController(IUserServices services)
        {
            _userServices = services;
            _jwtService = new JwtService();
        }

        }

        [HttpPost("register")]
        public IActionResult Register(RegisterDto dto)
        {
            var user1 = _userServices.GetByEmail(dto.Email);
            if (user1.Result != null) return BadRequest(new { message = "User Already Exists" });
            var user = new User
            {
                Name = dto.Name,
                Email = dto.Email,
                Age = dto.Age,
                Surname = dto.Surname,
                Username = dto.Username,
                Level = dto.Level,
                Exp = dto.Exp,
                Password = BCrypt.Net.BCrypt.HashPassword(dto.Password)
            };
            _userServices.CreateNewUser(user);
            return Ok(new { message = "Success" });
        }



        [HttpPost("login")]
        public async Task<IActionResult> Login(LoginDto dto)
        {
            var user = await _userServices.GetByEmail(dto.Email);
            if (user == null)
            {
                return BadRequest(Tuple.Create("", "No such user"));
            }
            if (!BCrypt.Net.BCrypt.Verify(dto.Password, user.Password))
            {
                return BadRequest(Tuple.Create("", "Wrong password"));
            }
            var jwt = _jwtService.Generate(user.Id);
            Response.Cookies.Append("jwt", jwt, new CookieOptions { HttpOnly = true, SameSite = SameSiteMode.None, Secure = false });

            return Ok(Tuple.Create(jwt , "Success"));
        }

        [HttpGet("user/{token}")]
        public async Task<IActionResult> User(string token)
        {
            //var _token = Request.Cookies["jwt"];
            var _token = _jwtService.Verify(token);
            int userId = int.Parse(_token.Issuer);
            var user = await _userServices.GetById(userId);
            return Ok(user);
        }

        [HttpPost("logout")]
        public IActionResult Logout()
        {
            Response.Cookies.Delete("jwt");
            return Ok(new { message = "Success" });
        }
    }
}
