using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace WalkAndTravel.ClassLibrary.DTO
{
    public class RegisterDto
    {
        public string Name { get; set; }

        public string Surname { get; set; }

        public string Username { get; set; }

        public string Email { get; set; }

        public string Password { get; set; }

        //public Levels Level { get; set; }

        public int Level { get; set; }
        public int Exp { get; set; }

        public int Age { get; set; }
    }
}
