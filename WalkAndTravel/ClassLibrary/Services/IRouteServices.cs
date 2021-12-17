using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WalkAndTravel.ClassLibrary.DTO;
using WalkAndTravel.ClassLibrary.Models;

namespace WalkAndTravel.ClassLibrary.Services
{
    public interface IRouteServices
    {
        Task<IEnumerable<Route>> GetRoutes();

        Task<IEnumerable<Route>> GetPagingRouteList(int page, int elements);

        Task<IEnumerable<RoutesCounter>> GetRoutesNumbers();

        Route GetRandomRoute();

        Route GetRandomPOIRoute();

        Task<int> SaveNewRoute(SaveDto dto);

        Task<int> SaveNewRoute(RouteMinimal route);

        Route DeleteRoute(int id);

        List<Route> SearchRoutes(string keyword);

        public Route SearchRouteByID(int Id);

        public List<Route> SearchRoutesByAuthorID(int Id);
    }
}
