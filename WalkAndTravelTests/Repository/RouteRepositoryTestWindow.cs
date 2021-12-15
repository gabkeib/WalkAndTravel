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
        private SqliteRouteRepositoryTest sqliteRouteRepositoryTest;
        public RouteRepositoryTestWindow()
        {
            sqliteRouteRepositoryTest = new SqliteRouteRepositoryTest();
        }

        [Fact]
        public void RouteRepository_GetRoutes_ReturnsCorrectNumberOfRoutes()
        {
            sqliteRouteRepositoryTest.RouteRepository_GetRoutes_ReturnsCorrectNumberOfRoutes();
        }

        [Theory]
        [InlineData(1, 1)]
        [InlineData(2, 1)]
        [InlineData(0, 2)]
        public void RouteRepository_GetPagingRouteList_ReturnsCorrectNumberOfRoutes(int page, int pageSize)
        {
            sqliteRouteRepositoryTest.RouteRepository_GetPagingRouteList_ReturnsCorrectNumberOfRoutes(page, pageSize);
        }

        [Fact]
        public void RouteRepository_SaveNewRoute_InsertsUserCorrectly()
        {
            sqliteRouteRepositoryTest.RouteRepository_SaveNewRoute_InsertsUserCorrectly();
        }
        
        [Fact]
        public void RouteRepository_DeleteRoute_DeletesCorrectRoute()
        {
            sqliteRouteRepositoryTest.RouteRepository_DeleteRoute_DeletesCorrectRoute();
        }

        [Fact]
        public void RouteRepository_GetRandomRoute_ReturnsCorrectRoute()
        {
            sqliteRouteRepositoryTest.RouteRepository_GetRandomRoute_ReturnsCorrectRoute();
        }
        [Fact]
        public void RouteRepository_GetRandomPOIRoute_ReturnsCorrectRoute()
        {
            sqliteRouteRepositoryTest.RouteRepository_GetRandomPOIRoute_ReturnsCorrectRoute();
        }

        [Fact]
        public void RouteRepository_GetRoutesNumbers_ReturnsCorrectRouteStatistics()
        {
            sqliteRouteRepositoryTest.RouteRepository_GetRoutesNumbers_ReturnsCorrectRouteStatistics();
        }
    }
}

