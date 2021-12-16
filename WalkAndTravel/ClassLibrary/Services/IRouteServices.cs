using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
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

        int SaveNewRoute(RouteMinimal routes);

        Route DeleteRoute(int id);

        List<Route> SearchRoutes(string keyword);

        public Route SearchRouteByID(int Id);
    }
}
