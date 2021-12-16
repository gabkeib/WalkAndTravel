using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WalkAndTravel.DataAccess;

namespace WalkAndTravelTests.Repository
{
    class SqliteUserRepositoryTest : UserRepositoryTest
    {
        public SqliteUserRepositoryTest():
            base (
                  new DbContextOptionsBuilder<DataContext>()
                  .UseSqlite("Filename=Test2.db")
                  .Options)
        { }
    }
}
