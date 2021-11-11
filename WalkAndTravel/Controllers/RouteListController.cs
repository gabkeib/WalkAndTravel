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

        public event EventHandler<ClassLibrary.Logging.LogEventArgs> Log;

        public RouteListController() 
        {
            Log += ClassLibrary.Logging.Logger.Log;
        }


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

        [HttpPost("SaveNewRoute")]
        public IActionResult SaveNewRoute([FromBody] RouteMinimal routes)
        {
            System.Diagnostics.Debug.WriteLine("here");
            System.Diagnostics.Debug.WriteLine(routes);
            Route newRoute = new Route();
            newRoute.Name = routes.Name;
            newRoute.Coordinates = new List<double[]>();
            for (int i = 0; i < routes.Route.Length; i += 2)
            {
            
                newRoute.Coordinates.Add(new double[] { routes.Route[i], routes.Route[i + 1] });
            }
            newRoute.Length = 2.3;
            List<Route> allRoutes = RoutesIO.ReadRoutesFromFile<Route>("Data/routes.json");
            allRoutes.Add(newRoute);
            RoutesIO.WriteRoutesToFile<Route>(allRoutes, "Data/routes.json");
            Log(this, new ClassLibrary.Logging.LogEventArgs("Save route", "Custom", newRoute.Name));
            return Ok(allRoutes);
        }

        [HttpGet("GetRandomPOIRoute")]
        public Route GetRandomPOIRoute()
        {
            var rng = new Random();
            var route = new SightseeingRoute(new POISelector(), new Marker(54.6859564, 25.2861464), rng.Next(3, 10));
            route.GenerateRoute();
            route.Coordinates = Route.MarkersListToArray(route.Markers);
            Log(this, new ClassLibrary.Logging.LogEventArgs("Generate route", "Sightseeing", "NoName"));
            return route;
        }

        [HttpGet("GetRandomRoute")]
        public Route GetRandomRoute()
        {
            var rng = new Random();
            var route = new CityRoute(new Marker(54.6859564, 25.2861464), rng.Next(3,10));
            Func<double> bearingCalculator = delegate() { return rng.Next(30, 359) + rng.NextDouble(); };
            if (rng.Next(1,2) == 2)
            {
                bearingCalculator = () => rng.Next(1, 4) * 90;
            }
            route.GenerateRoute(bearingCalculator);
            route.Coordinates = Route.MarkersListToArray(route.Markers);
            Log(this, new ClassLibrary.Logging.LogEventArgs("Generate route", "City", "NoName"));
            return route;
        }


        [HttpGet]
        public IEnumerable<Route> Get()
        {
            List<Route> routes = RoutesIO.ReadRoutesFromFile<Route>("Data/routes.json");
            routes.Sort();
            return routes.Select(route => route
            ).ToArray();
        }
    }
}
