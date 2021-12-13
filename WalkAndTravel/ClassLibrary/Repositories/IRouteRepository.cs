using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WalkAndTravel.ClassLibrary.Models;

namespace WalkAndTravel.ClassLibrary.Repositories
{
    public interface IRouteRepository
    {
        Task<List<Route>> GetRoutes();

        Task<List<Route>> GetPagingRouteList(int page, int elements);

        Task<List<RoutesCounter>> GetRoutesNumbers();

        Route GetRandomRoute();

        Route GetRandomPOIRoute();

        int SaveNewRoute(RouteMinimal routes);

        int DeleteRoute(int id);
    }
}
