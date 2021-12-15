using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace WalkAndTravelTests.Controllers
{
    public class RouteRepositoryTestWindow
    {
        [Fact]
        public void RouteRepository_GetRoutes_ReturnsCorrectNumberOfRoutes()
        {
            SqliteRouteRepositoryTest sqliteRouteRepositoryTest = new SqliteRouteRepositoryTest();
            sqliteRouteRepositoryTest.RouteRepository_GetRoutes_ReturnsCorrectNumberOfRoutes();
        }

        [Theory]
        [InlineData(1, 1)]
        [InlineData(2, 1)]
        [InlineData(0, 2)]
        public void RouteRepository_GetPagingRouteList_ReturnsCorrectNumberOfRoutes(int page, int pageSize)
        {
            SqliteRouteRepositoryTest sqliteRouteRepositoryTest = new SqliteRouteRepositoryTest();
            sqliteRouteRepositoryTest.RouteRepository_GetPagingRouteList_ReturnsCorrectNumberOfRoutes(page, pageSize);
        }

        [Fact]
        public void RouteRepository_SaveNewRoute_InsertsUserCorrectly()
        {
            SqliteRouteRepositoryTest sqliteRouteRepositoryTest = new SqliteRouteRepositoryTest();
            sqliteRouteRepositoryTest.RouteRepository_SaveNewRoute_InsertsUserCorrectly();
        }
        
        [Fact]
        public void RouteRepository_DeleteRoute_DeletesCorrectRoute()
        {
            SqliteRouteRepositoryTest sqliteRouteRepositoryTest = new SqliteRouteRepositoryTest();
            sqliteRouteRepositoryTest.RouteRepository_DeleteRoute_DeletesCorrectRoute();
        }

        [Fact]
        public void RouteRepository_GetRandomRoute_ReturnsCorrectRoute()
        {
            SqliteRouteRepositoryTest sqliteRouteRepositoryTest = new SqliteRouteRepositoryTest();
            sqliteRouteRepositoryTest.RouteRepository_GetRandomRoute_ReturnsCorrectRoute();
        }
        [Fact]
        public void RouteRepository_GetRandomPOIRoute_ReturnsCorrectRoute()
        {
            SqliteRouteRepositoryTest sqliteRouteRepositoryTest = new SqliteRouteRepositoryTest();
            sqliteRouteRepositoryTest.RouteRepository_GetRandomPOIRoute_ReturnsCorrectRoute();
        }

        [Fact]
        public void RouteRepository_GetRoutesNumbers_ReturnsCorrectRouteStatistics()
        {
            SqliteRouteRepositoryTest sqliteRouteRepositoryTest = new SqliteRouteRepositoryTest();
            sqliteRouteRepositoryTest.RouteRepository_GetRoutesNumbers_ReturnsCorrectRouteStatistics();
        }
    }
}

