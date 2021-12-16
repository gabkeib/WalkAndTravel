using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WalkAndTravel.ClassLibrary;
using WalkAndTravel.ClassLibrary.Repositories;
using WalkAndTravel.DataAccess;
using Xunit;

namespace WalkAndTravelTests.Controllers
{

    public abstract class RouteRepositoryTest
    {
        protected DbContextOptions<DataContext> ContextOptions { get; }
        protected RouteRepositoryTest(DbContextOptions<DataContext> contextOptions)
        {
            ContextOptions = contextOptions;
            Seed();
        }

        private void Seed()
        {
            using (var context = new DataContext(ContextOptions))
            {
                context.Database.EnsureDeleted();
                context.Database.EnsureCreated();

                List<Marker> markers1 = new List<Marker> { new Marker(54.6866, 25.288), new Marker(54.6902, 25.2764) };
                List<double[]> markersC1 = new List<double[]> { new double[] { 54.6866, 25.288 }, new double[] { 54.6902, 25.2764 } };
                var route1 = new Route(3.2, markers1, markersC1, name: "Route1", LengthType.Medium);

                List<Marker> markers2 = new List<Marker> { new Marker(54.6866, 25.288), new Marker(54.717755, 25.221089), new Marker(54.6902, 25.2764) };
                List<double[]> markersC2 = new List<double[]> { new double[] { 54.6866, 25.288 }, new double[] { 54.717755, 25.221089 }, new double[] { 54.6902, 25.2764 } };
                var route2 = new Route(4.2, markers2, markersC2, name: "Route2", LengthType.Long);

                List<Marker> markers3 = new List<Marker> { new Marker(54.6866, 25.288), new Marker(54.717755, 25.221089), new Marker(54.6859564, 25.2861464), new Marker(54.6902, 25.2764) };
                var route3 = new Route();
                route3.Markers = markers3;
                var markersC3 = Route.MarkersListToArray(markers3);
                route3.Coordinates = markersC3;
                route3.Length = 5.6;
                route3.PickLengthType();
                route3.Name = "Route3";

                context.AddRange(route1, route2, route3);
                context.SaveChanges();
            }
        }


        public async void RouteRepository_GetRoutes_ReturnsCorrectNumberOfRoutes()
        {
            using (var context = new DataContext(ContextOptions))
            {
                var repository = new RouteRepository(context);

                var routes = await repository.GetRoutes();

                Assert.Equal(3, routes.Count);
            }
        }

        public async void RouteRepository_GetPagingRouteList_ReturnsCorrectNumberOfRoutes(int page, int pageSize)
        {
            using (var context = new DataContext(ContextOptions))
            {
                var repository = new RouteRepository(context);

                var routes = await repository.GetPagingRouteList(page, pageSize);

                Assert.Equal(pageSize, routes.Count);
            }
        }

        public void RouteRepository_SaveNewRoute_InsertsUserCorrectly()
        {
            using (var context = new DataContext(ContextOptions))
            {
                var repository = new RouteRepository(context);

                List<Marker> markers = new List<Marker> { new Marker(54.6866, 25.288), new Marker(54.717755, 25.221089) };
                double[] markersC = new double[] { 54.6866, 25.288, 54.717755, 25.221089 };
                var route4 = new RouteMinimal();
                route4.Name = "Route4";
                route4.Route = markersC;
                int result = repository.SaveNewRoute(route4);

                Assert.NotEqual(-1, result);
            }
        }

        public void RouteRepository_DeleteRoute_DeletesCorrectRoute()
        {
            using (var context = new DataContext(ContextOptions))
            {
                var repository = new RouteRepository(context);

                var route = repository.DeleteRoute(1);

                Assert.Equal("Route1", route.Name);
            }
        }

        public void RouteRepository_GetRandomRoute_ReturnsCorrectRoute()
        {
            using (var context = new DataContext(ContextOptions))
            {
                var repository = new RouteRepository(context);

                var route = repository.GetRandomRoute();

                Assert.InRange(route.Markers.Count, 4, 11);
            }
        }

        public void RouteRepository_GetRandomPOIRoute_ReturnsCorrectRoute()
        {
            using (var context = new DataContext(ContextOptions))
            {
                var repository = new RouteRepository(context);

                var route = repository.GetRandomPOIRoute();

                Assert.InRange(route.Markers.Count, 4, 11);
            }
        }

        public async void RouteRepository_GetRoutesNumbers_ReturnsCorrectRouteStatistics()
        {
            using (var context = new DataContext(ContextOptions))
            {
                var repository = new RouteRepository(context);

                var route = await repository.GetRoutesNumbers();

                Assert.Equal(3, route.Count);
                Assert.Equal(1, route[0].Count);
                Assert.Equal(1, route[1].Count);
                Assert.Equal(1, route[2].Count);
            }
        }

        public void RouteRepository_SearchRoutes_ReturnsCorrectRoutes(string keyword, int result)
        {
            using (var context = new DataContext(ContextOptions))
            {
                var repository = new RouteRepository(context);

                var route = repository.SearchRoutes(keyword);

                Assert.Equal(result, route.Count);
            }
        }

        public void RouteRepository_SearchRouteByID_ReturnsCorrectRoute(int id, string name)
        {
            using (var context = new DataContext(ContextOptions))
            {
                var repository = new RouteRepository(context);

                var route = repository.SearchRouteByID(id);

                Assert.Equal(name, route.Name);
            }
        }
    }
}
