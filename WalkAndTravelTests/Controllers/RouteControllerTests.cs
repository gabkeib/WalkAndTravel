using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WalkAndTravel.ClassLibrary;
using Xunit;
using Moq;
using WalkAndTravel.ClassLibrary.Services;
using WalkAndTravel.Controllers;
using System.Net;
using System.Web.Http;
using System.Web.Http.Results;
using Microsoft.AspNetCore.Mvc;
using WalkAndTravel.ClassLibrary.Models;

namespace WalkAndTravelTests.Controllers
{
    public class RouteControllerTests
    {
        [Fact]
        public async void RouteController_SaveNewRoute_ReturnsCorrectResponseWhenError()
        {
            var service = new Mock<IRouteServices>();
            service.Setup(ms => ms.SaveNewRoute(It.IsAny<RouteMinimal>())).ReturnsAsync(-1);

            var controller = new RouteController(service.Object);

            var route = new RouteMinimal();
            route.Name = "Route4";
            route.Route = new double[] { 54.6866, 25.288, 54.717755, 25.221089 };
            var response = await controller.SaveNewRoute(route);
            Assert.IsType<Microsoft.AspNetCore.Mvc.BadRequestResult>(response);
        }

        [Fact]
        public async void RouteController_SaveNewRoute_ReturnsCorrectResponse()
        {
            var service = new Mock<IRouteServices>();
            service.Setup(ms => ms.SaveNewRoute(It.IsAny<RouteMinimal>())).ReturnsAsync(4);

            var controller = new RouteController(service.Object);

            var route = new RouteMinimal();
            route.Name = "Route4";
            route.Route = new double[] { 54.6866, 25.288, 54.717755, 25.221089 };
            var response = await controller.SaveNewRoute(route);

            var okObjectResult = response as OkObjectResult;
            Assert.NotNull(okObjectResult);

            var actual = okObjectResult.Value;

            Assert.Equal(4, actual);
        }

        [Fact]
        public void RouteController_GetRandomPOIRoute_ReturnsCorrectRoute()
        {
            var service = new Mock<IRouteServices>();

            List<Marker> markers1 = new List<Marker> { new Marker(54.6866, 25.288), new Marker(54.6902, 25.2764) };
            List<double[]> markersC1 = new List<double[]> { new double[] { 54.6866, 25.288 }, new double[] { 54.6902, 25.2764 } };
            var route1 = new Route(3.2, markers1, markersC1, name: "Route1", LengthType.Medium);

            service.Setup(ms => ms.GetRandomPOIRoute()).Returns(route1);

            var controller = new RouteController(service.Object);

            var response = controller.GetRandomPOIRoute();

            Assert.Equal(route1, response);
        }
        [Fact]
        public void RouteController_GetRandomRoute_ReturnsCorrectRoute()
        {
            var service = new Mock<IRouteServices>();

            List<Marker> markers1 = new List<Marker> { new Marker(54.6866, 25.288), new Marker(54.6902, 25.2764) };
            List<double[]> markersC1 = new List<double[]> { new double[] { 54.6866, 25.288 }, new double[] { 54.6902, 25.2764 } };
            var route1 = new Route(3.2, markers1, markersC1, name: "Route1", LengthType.Medium);

            service.Setup(ms => ms.GetRandomRoute()).Returns(route1);

            var controller = new RouteController(service.Object);

            var response = controller.GetRoutes();

            Assert.Equal(route1, response);
        }

        [Fact]
        public void RouteController_DeleteById_ReturnsCorrectRoute()
        {
            var service = new Mock<IRouteServices>();

            List<Marker> markers1 = new List<Marker> { new Marker(54.6866, 25.288), new Marker(54.6902, 25.2764) };
            List<double[]> markersC1 = new List<double[]> { new double[] { 54.6866, 25.288 }, new double[] { 54.6902, 25.2764 } };
            var route1 = new Route(3.2, markers1, markersC1, name: "Route1", LengthType.Medium);

            service.Setup(ms => ms.DeleteRoute(It.IsAny<int>())).Returns(route1);

            var controller = new RouteController(service.Object);

            var response = controller.DeleteById(1);

            Assert.Equal(route1, response);
        }

        [Fact]
        public async void RouteController_Get_ReturnsCorrectRoutes()
        {
            var service = new Mock<IRouteServices>();

            List<Marker> markers1 = new List<Marker> { new Marker(54.6866, 25.288), new Marker(54.6902, 25.2764) };
            List<double[]> markersC1 = new List<double[]> { new double[] { 54.6866, 25.288 }, new double[] { 54.6902, 25.2764 } };
            var route1 = new Route(3.2, markers1, markersC1, name: "Route1", LengthType.Medium);
            List<Route> routes = new List<Route> { route1, route1 };

            service.Setup(ms => ms.GetRoutes()).ReturnsAsync(routes);

            var controller = new RouteController(service.Object);

            var response = await controller.Get();
            var result = response.ToList();

            Assert.Equal(2, result.Count);
        }

        [Fact]
        public async void RouteController_GetPagingRouteList_ReturnsCorrectRoutes()
        {
            var service = new Mock<IRouteServices>();

            List<Marker> markers1 = new List<Marker> { new Marker(54.6866, 25.288), new Marker(54.6902, 25.2764) };
            List<double[]> markersC1 = new List<double[]> { new double[] { 54.6866, 25.288 }, new double[] { 54.6902, 25.2764 } };
            var route1 = new Route(3.2, markers1, markersC1, name: "Route1", LengthType.Medium);
            List<Route> routes = new List<Route> { route1, route1 };

            service.Setup(ms => ms.GetPagingRouteList(It.IsAny<int>(), It.IsAny<int>())).ReturnsAsync(routes);

            var controller = new RouteController(service.Object);

            var response = await controller.GetPagingRouteList(1,2);
            var result = response.ToList();

            Assert.Equal(2, result.Count);
        }

        [Fact]
        public void RouteController_GetByKeyword_ReturnsRoutes()
        {
            var service = new Mock<IRouteServices>();

            List<Marker> markers1 = new List<Marker> { new Marker(54.6866, 25.288), new Marker(54.6902, 25.2764) };
            List<double[]> markersC1 = new List<double[]> { new double[] { 54.6866, 25.288 }, new double[] { 54.6902, 25.2764 } };
            var route1 = new Route(3.2, markers1, markersC1, name: "Route1", LengthType.Medium);
            List<Route> routes = new List<Route> { route1, route1 };

            service.Setup(ms => ms.SearchRoutes(It.IsAny<string>())).Returns(routes);

            var controller = new RouteController(service.Object);

            var response = controller.GetByKeyword("rout");
            var result = response.ToList();

            Assert.Equal(2, result.Count);
        }

        [Fact]
        public async void RouteController_GetStatistics_ReturnsStatistics()
        {
            var service = new Mock<IRouteServices>();

            RoutesCounter routeCounter = new RoutesCounter("Long", 21);
            RoutesCounter routeCounter2 = new RoutesCounter("Short", 50);

            List<RoutesCounter> routes = new List<RoutesCounter> { routeCounter, routeCounter2 };

            service.Setup(ms => ms.GetRoutesNumbers()).ReturnsAsync(routes);

            var controller = new RouteController(service.Object);

            var response = await controller.GetStatistics();
            var result = response.ToList();

            Assert.Equal(2, result.Count);
        }
    }
}
