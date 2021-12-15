using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WalkAndTravel.DataAccess;
using Xunit;

namespace WalkAndTravelTests.Controllers
{
    public class SqliteRouteRepositoryTest : RouteRepositoryTest
    {
        public SqliteRouteRepositoryTest() 
            : base (
                  new DbContextOptionsBuilder<DataContext>()
                  .UseSqlite("Filename=Test.db")
                  .Options)
        { }

    }
}
