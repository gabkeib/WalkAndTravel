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
            list.Add(new Marker(54.6866, 25.2865));
            list.Add(new Marker(54.6902, 25.2764));

            return list;
        }

        public List<double[]> generateRouteCoordinates()
        {
            return Route.markersListToArray(generateRoute()); 
        }


        [HttpGet]
        public IEnumerable<Route> Get()
        {
            var rng = new Random();
            return Enumerable.Range(1, 3).Select(index => new Route
            {
                Name = "Route" + index,
                Markers = generateRoute(),
                Coordinates = generateRouteCoordinates(),
                Length = (rng.Next(1, 5) + rng.NextDouble())
            }
            ).ToArray();
        }
    }
}
