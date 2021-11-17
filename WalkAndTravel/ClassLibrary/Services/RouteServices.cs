using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WalkAndTravel.ClassLibrary.Repositories;

namespace WalkAndTravel.ClassLibrary.Services
{
    public class RouteServices : IRouteServices
    {

        private IRouteRepository _routeRepository;

        public RouteServices(IRouteRepository routeRepository)
        {
            _routeRepository = routeRepository;
        }

        public Route GetRandomPOIRoute()
        {
            return _routeRepository.GetRandomPOIRoute();
        }

        public async Task<IEnumerable<Route>> GetRoutes()
        {
            var routes = await _routeRepository.GetRoutes();
            routes.Sort();
            return routes.Select(route => route
            ).ToArray();
        }

        public Route GetRandomRoute()
        {
            return _routeRepository.GetRandomRoute();
        }

        public int SaveNewRoute(RouteMinimal routes)
        {
            return _routeRepository.SaveNewRoute(routes);
        }

        
    }
}
