using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;
using Moq;
using WalkAndTravel.ClassLibrary.Repositories;
using WalkAndTravel.ClassLibrary.Services;
using WalkAndTravel.ClassLibrary;
using WalkAndTravel.ClassLibrary.Models;
using WalkAndTravel.ClassLibrary.DTO;

namespace WalkAndTravelTests.Services
{
    public class RouteServiceTests
    {
        [Fact]
        public void RouteService_GetRandomPOIRoute_GetsCorrectValue()
        {
            var repository = new Mock<IRouteRepository>();

            List<Marker> markers1 = new List<Marker> { new Marker(54.6866, 25.288), new Marker(54.6902, 25.2764) };
            List<double[]> markersC1 = new List<double[]> { new double[] { 54.6866, 25.288 }, new double[] { 54.6902, 25.2764 } };
            var route1 = new Route(3.2, markers1, markersC1, name: "Route1", LengthType.Medium);

            repository.Setup(mr => mr.GetRandomPOIRoute()).Returns(route1);
            var service = new RouteServices(repository.Object);

            var route = service.GetRandomPOIRoute();
            Assert.Equal("Route1", route.Name);

        }
        [Fact]
        public void RouteService_GetRandomRoute_GetsCorrectValue()
        {
            var repository = new Mock<IRouteRepository>();

            List<Marker> markers1 = new List<Marker> { new Marker(54.6866, 25.288), new Marker(54.6902, 25.2764) };
            List<double[]> markersC1 = new List<double[]> { new double[] { 54.6866, 25.288 }, new double[] { 54.6902, 25.2764 } };
            var route1 = new Route(3.2, markers1, markersC1, name: "Route1", LengthType.Medium);

            repository.Setup(mr => mr.GetRandomRoute()).Returns(route1);
            var service = new RouteServices(repository.Object);

            var route = service.GetRandomRoute();
            Assert.Equal("Route1", route.Name);

        }
        [Fact]
        public async void RouteService_GetPagingRouteList_ReturnsCorrectSize()
        {
            var repository = new Mock<IRouteRepository>();

            List<Marker> markers1 = new List<Marker> { new Marker(54.6866, 25.288), new Marker(54.6902, 25.2764) };
            List<double[]> markersC1 = new List<double[]> { new double[] { 54.6866, 25.288 }, new double[] { 54.6902, 25.2764 } };
            var route1 = new Route(3.2, markers1, markersC1, name: "Route1", LengthType.Medium);
            var route2 = new Route(3.2, markers1, markersC1, name: "Route1", LengthType.Medium);
            var routesList = new List<Route> { route1, route2 };
            

            repository.Setup(mr => mr.GetPagingRouteList(It.IsAny<int>(), It.IsAny<int>())).ReturnsAsync(routesList);
            var service = new RouteServices(repository.Object);

            var routes = await service.GetPagingRouteList(1,2);
            var routesNewList = routes.ToList();
            Assert.Equal(2, routesNewList.Count);

        }

        [Fact]
        public async void RouteService_GetRoutes_ReturnsCorrectSize()
        {
            var repository = new Mock<IRouteRepository>();

            List<Marker> markers1 = new List<Marker> { new Marker(54.6866, 25.288), new Marker(54.6902, 25.2764) };
            List<double[]> markersC1 = new List<double[]> { new double[] { 54.6866, 25.288 }, new double[] { 54.6902, 25.2764 } };
            var route1 = new Route(3.2, markers1, markersC1, name: "Route1", LengthType.Medium);
            var route2 = new Route(3.2, markers1, markersC1, name: "Route2", LengthType.Medium);
            var routesList = new List<Route> { route1, route2 };


            repository.Setup(mr => mr.GetRoutes()).ReturnsAsync(routesList);
            var service = new RouteServices(repository.Object);

            var routes = await service.GetRoutes();
            var routesNewList = routes.ToList();
            Assert.Equal(2, routesNewList.Count);
        }

        [Fact]
        public void RouteService_SearchRoutes_ReturnsCorrectSize()
        {
            var repository = new Mock<IRouteRepository>();

            List<Marker> markers1 = new List<Marker> { new Marker(54.6866, 25.288), new Marker(54.6902, 25.2764) };
            List<double[]> markersC1 = new List<double[]> { new double[] { 54.6866, 25.288 }, new double[] { 54.6902, 25.2764 } };
            var route1 = new Route(3.2, markers1, markersC1, name: "Route1", LengthType.Medium);
            var route2 = new Route(3.2, markers1, markersC1, name: "Route2", LengthType.Medium);
            var routesList = new List<Route> { route1, route2 };


            repository.Setup(mr => mr.SearchRoutes(It.IsAny<string>())).Returns(routesList);
            var service = new RouteServices(repository.Object);

            var routes = service.SearchRoutes("rout");
            var routesNewList = routes.ToList();
            Assert.Equal(2, routesNewList.Count);
        }

        [Fact]
        public async void RouteService_GetRoutesNumbers_ReturnsCorrectSize()
        {
            var repository = new Mock<IRouteRepository>();

            List<RoutesCounter> routesCounters = new List<RoutesCounter> { new RoutesCounter("Short", 3), new RoutesCounter("Long", 60) };


            repository.Setup(mr => mr.GetRoutesNumbers()).ReturnsAsync(routesCounters);
            var service = new RouteServices(repository.Object);

            var routes = await service.GetRoutesNumbers();
            var routesNewList = routes.ToList();
            Assert.Equal(2, routesNewList.Count);

        }
        [Fact]
        public async void RouteService_SaveNewRoute_ReturnsNumber()
        {
            List<Marker> markers = new List<Marker> { new Marker(54.6866, 25.288), new Marker(54.717755, 25.221089) };
            double[] markersC = new double[] { 54.6866, 25.288, 54.717755, 25.221089 };
            var route4 = new RouteMinimal();

            var repository = new Mock<IRouteRepository>();
            repository.Setup(mr => mr.SaveNewRoute(It.IsAny<SaveDto>())).ReturnsAsync(1);
            
            var service = new RouteServices(repository.Object);
            var response = await service.SaveNewRoute(route4);

            Assert.Equal(1, response);

        }

        [Fact]
        public void RouteService_DeleteRoute_ReturnsRoute()
        {
            List<Marker> markers1 = new List<Marker> { new Marker(54.6866, 25.288), new Marker(54.6902, 25.2764) };
            List<double[]> markersC1 = new List<double[]> { new double[] { 54.6866, 25.288 }, new double[] { 54.6902, 25.2764 } };
            var route1 = new Route(3.2, markers1, markersC1, name: "Route1", LengthType.Medium);

            var repository = new Mock<IRouteRepository>();
            repository.Setup(mr => mr.DeleteRoute(It.IsAny<int>())).Returns(route1);

            var service = new RouteServices(repository.Object);
            var response = service.DeleteRoute(4);

            Assert.Equal("Route1", response.Name);

        }

        [Fact]
        public void RouteService_SearchRouteById_ReturnsRoute()
        {
            List<Marker> markers1 = new List<Marker> { new Marker(54.6866, 25.288), new Marker(54.6902, 25.2764) };
            List<double[]> markersC1 = new List<double[]> { new double[] { 54.6866, 25.288 }, new double[] { 54.6902, 25.2764 } };
            var route1 = new Route(3.2, markers1, markersC1, name: "Route1", LengthType.Medium);

            var repository = new Mock<IRouteRepository>();
            repository.Setup(mr => mr.SearchRouteByID(It.IsAny<int>())).Returns(route1);

            var service = new RouteServices(repository.Object);
            var response = service.SearchRouteByID(4);

            Assert.Equal("Route1", response.Name);

        }
    }
}
