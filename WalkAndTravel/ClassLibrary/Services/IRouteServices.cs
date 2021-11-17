using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WalkAndTravel.ClassLibrary.Services
{
    public interface IRouteServices
    {
        Task<IEnumerable<Route>> GetRoutes();

        Route GetRandomRoute();

        Route GetRandomPOIRoute();

        int SaveNewRoute(RouteMinimal routes);
    }
}
