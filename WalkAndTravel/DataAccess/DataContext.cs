using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WalkAndTravel.ClassLibrary;
using WalkAndTravel.Models;

namespace WalkAndTravel.DataAccess
{
    public class DataContext : DbContext
    {
        private const string ConnectionString = "Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=WAT_DataBase;Integrated Security=True;";

        public DbSet<Route> Routes { get; set; }
        public DbSet<Marker> Markers { get; set; }
        public DbSet<User> Users { get; set; }

        public DataContext(DbContextOptions<DataContext> options) : base(options){}
    }
}
