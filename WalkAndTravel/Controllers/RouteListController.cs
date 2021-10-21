using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WalkAndTravel.ClassLibrary;

namespace WalkAndTravel.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class RouteListController : ControllerBase
    {
     

        public List<Marker> generateRoute()
        {
            List<Marker> list = new();
            list.Add(new Marker(lat: 54.6866, lng: 25.2865));
            list.Add(new Marker(lat: 54.6902, lng: 25.2764));

            return list;
        }

        public List<double[]> generateRouteCoordinates()
        {
            return Route.MarkersListToArray(generateRoute()); 
        }

        [HttpGet("GetRandomPOIRoute")]
        public Route GetRandomPOIRoute()
        {
            var rng = new Random();
            var route = new SightseeingRoute(new Marker(54.6859564, 25.2861464), rng.Next(3, 10));
            route.GenerateRoute();
            route.Coordinates = Route.markersListToArray(route.Markers);
            return route;
        }

        [HttpGet("GetRandomRoute")]
        public Route GetRandomRoute()
        {
            var rng = new Random();
            var route = new CityRoute(new Marker(54.6859564, 25.2861464), rng.Next(3,10));
            route.GenerateRoute();
            route.Coordinates = Route.markersListToArray(route.Markers);
            return route;
        }


        [HttpGet]
        public IEnumerable<Route> Get()
        {
            List<Route> routes = RoutesIO.ReadRoutesFromFile<Route>("Data/routes.json");
            /*var routes1 = new List<Route>();
            foreach(var route in routes)
            {
                route.Coordinates = Route.MarkersListToArray(route.Markers);
                routes1.Add(route);
            }*/

            routes.Sort();
            return routes.Select(route => route
            ).ToArray();
        }
    }
}
