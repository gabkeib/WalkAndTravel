using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WalkAndTravel.ClassLibrary.Repositories
{
    public interface IRouteRepository
    {
        Task<List<Route>> GetRoutes();

        Route GetRandomRoute();

        Route GetRandomPOIRoute();

        int SaveNewRoute(RouteMinimal routes);
    }
}
