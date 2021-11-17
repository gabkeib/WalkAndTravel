using System.Text.Json.Serialization;
using WalkAndTravel.ClassLibrary;

namespace WalkAndTravel.Models
{
    public class User
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Surname { get; set; }

        public string Nickname { get; set; }

        public string Email { get; set; }

        [JsonIgnore]
        public string Password { get; set; }

        //public Levels Level { get; set; }

        public int Level { get; set; }

        public int Exp { get; set; }

        public int Age { get; set; }
    }
}