using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WalkAndTravel.ClassLibrary;

namespace WalkAndTravel.DataAccess
{
    public class RoutesContext : DbContext
    {

        //public RoutesContext(DbContextOptions<RoutesContext> options)
        //    : base(options)
        //{
        //}

        private const string ConnectionString = "Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=WAT_DataBase;Integrated Security=True;";

        public DbSet<Route> Routes { get; set; }
        public DbSet<Marker> Markers { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
            optionsBuilder.UseSqlServer(ConnectionString);
        }

        /*protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Route>().ToTable("Route");
            modelBuilder.Entity<Marker>().ToTable("Marker");
        }*/
    }
}
