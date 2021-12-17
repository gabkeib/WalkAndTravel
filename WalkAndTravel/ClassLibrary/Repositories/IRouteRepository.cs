using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WalkAndTravel.ClassLibrary.DTO;
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

        Task<int> SaveNewRoute(SaveDto dto);

        Route DeleteRoute(int id);

        List<Route> SearchRoutes(string keyword);

        public Route SearchRouteByID(int Id);

        public List<Route> SearchRoutesByAuthorID(int Id);
    }
}
