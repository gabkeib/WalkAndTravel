using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WalkAndTravel.ClassLibrary.Models
{
    public class UserMinimal
        {
            public int Id { get; set; }

            public string Name { get; set; }

            public string Surname { get; set; }

            public string Username { get; set; }

            public string Email { get; set; }

            public int Level { get; set; }

            public int Exp { get; set; }

            public int Age { get; set; }
        }
}
