using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WalkAndTravel.ClassLibrary.Models;
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

        public async Task<IEnumerable<Route>> GetPagingRouteList(int page, int elements)
        {
            var routes = await _routeRepository.GetPagingRouteList(page, elements);
            routes.Sort();
            return routes.Select(route => route
            ).ToArray();
        }

        public async Task<IEnumerable<RoutesCounter>> GetRoutesNumbers()
        {
            var statistics = await _routeRepository.GetRoutesNumbers();
            statistics.Sort();
            return statistics.ToArray();
        }

        public Route GetRandomRoute()
        {
            return _routeRepository.GetRandomRoute();
        }

        public int SaveNewRoute(RouteMinimal routes)
        {
            return _routeRepository.SaveNewRoute(routes);
        }

        public int DeleteRoute(int id)
        {
            return _routeRepository.DeleteRoute(id);
        }

        public List<Route> SearchRoutes(string keyword)
        {
            return _routeRepository.SearchRoutes(keyword);
        }

        public Route SearchRouteByID(int Id)
        {
            return _routeRepository.SearchRouteByID(Id);
        }

    }
}
