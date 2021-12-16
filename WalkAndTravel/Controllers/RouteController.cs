using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WalkAndTravel.ClassLibrary;
using WalkAndTravel.ClassLibrary.Models;
using WalkAndTravel.ClassLibrary.Repositories;
using WalkAndTravel.ClassLibrary.Services;
using WalkAndTravel.DataAccess;


namespace WalkAndTravel.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class RouteController : ControllerBase
    {

        private IRouteServices _routeServices;

        public RouteController(IRouteServices routeServices)
        {
            _routeServices = routeServices;

        }

        [HttpPost("SaveNewRoute")]
        public IActionResult SaveNewRoute([FromBody] RouteMinimal routes)
        {
            var route = _routeServices.SaveNewRoute(routes);
            if(route == -1)
            {
                return BadRequest();
            }
            return Ok(route);
        }

        [HttpGet("RandomPOIRoute")]
        public Route GetRandomPOIRoute()
        {
            return _routeServices.GetRandomPOIRoute();
        }

        [HttpGet("RandomRoute")]
        public Route GetRoutes()
        {
            return _routeServices.GetRandomRoute();
        }

        [HttpGet("Routes/{page?}/{size?}")]
        public Task<IEnumerable<Route>> GetPagingRouteList(int page, int size)
        {
            return _routeServices.GetPagingRouteList(page, size);
        }

        [HttpGet("Route")]
        public async Task<IEnumerable<Route>> Get()
        {
            return await _routeServices.GetRoutes();
        }

        [HttpGet("Search/{id?}")]
        public IEnumerable<Route> GetByKeyword(string id)
        {
            return _routeServices.SearchRoutes(id);
        }

        [HttpDelete("Delete/{id?}")]
        public Route DeleteById(int id)
        {
            return _routeServices.DeleteRoute(id);
        }

        [HttpGet("Statistics")]
        public async Task<IEnumerable<RoutesCounter>> GetStatistics()
        {
            return await _routeServices.GetRoutesNumbers();
        }

        [HttpGet("{id}")]
        public Route GetById(int Id)
        {
            return _routeServices.SearchRouteByID(Id);
        }
    }
}
