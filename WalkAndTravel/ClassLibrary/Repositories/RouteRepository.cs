using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WalkAndTravel.ClassLibrary.Models;
using WalkAndTravel.DataAccess;

namespace WalkAndTravel.ClassLibrary.Repositories
{
    public class RouteRepository : IRouteRepository
    {
        public Route GetRandomPOIRoute()
        {
            var rng = new Random();
            var route = new SightseeingRoute(new POISelector(), new Marker(54.6859564, 25.2861464), rng.Next(3, 10));
            route.GenerateRoute();
            route.Coordinates = Route.MarkersListToArray(route.Markers);
            //Log(this, new ClassLibrary.Logging.LogEventArgs("Generate route", "Sightseeing", "NoName"));
            return route;
        }

        public Route GetRandomRoute()
        {
            var rng = new Random();
            var route = new CityRoute(new Marker(54.6859564, 25.2861464), rng.Next(3, 10));
            Func<double> bearingCalculator = delegate () { return rng.Next(30, 359) + rng.NextDouble(); };
            if (rng.Next(1, 2) == 2)
            {
                bearingCalculator = () => rng.Next(1, 4) * 90;
            }
            route.GenerateRoute(bearingCalculator);
            route.Coordinates = Route.MarkersListToArray(route.Markers);
            //Log(this, new ClassLibrary.Logging.LogEventArgs("Generate route", "City", "NoName"));
            return route;
        }

        public async Task<List<Route>> GetRoutes()
        {
            var routes = await GetRoutesAsync();
            return routes;
        }

        private List<Route> RoutesSelector()
        {
            List<Route> routes = new();
            using (var context = new DataContext())
            {

                foreach (var route in context.Routes.ToList())
                {
                    route.Markers = new List<Marker>();

                    foreach (var marker in context.Markers)
                    {
                        if (marker.RouteId == route.RouteId)
                        {
                            route.Markers.Add(marker);
                        }
                    }
                    route.Coordinates = Route.MarkersListToArray(route.Markers);
                    routes.Add(route);

                }
            }

            return routes;
        }

        private async Task<List<Route>> GetRoutesAsync()
        {
            List<Route> routes = await Task.Run(() => RoutesSelector());

            return routes;
        }

        public async Task<List<Route>> GetPagingRouteList(int page, int elements)
        {
            return await Task.Run(() => PagingRouteList(page, elements));
        }

        private List<Route> PagingRouteList(int page, int elements)
        {
            List<Route> routes = new();
            using (var context = new DataContext())
            {
                var currentRoutes = context.Routes.Skip(page * elements).Take(elements).ToList();
                foreach (var route in currentRoutes)
                {
                    route.Markers = new List<Marker>();

                    foreach (var marker in context.Markers)
                    {
                        if (marker.RouteId == route.RouteId)
                        {
                            route.Markers.Add(marker);
                        }
                    }
                    route.Coordinates = Route.MarkersListToArray(route.Markers);
                    routes.Add(route);

                }
            }
            return routes;
        }

        public int SaveNewRoute(RouteMinimal routes)
        {
            System.Diagnostics.Debug.WriteLine("here");
            System.Diagnostics.Debug.WriteLine(routes);
            Route newRoute = new Route();
            newRoute.Name = routes.Name;
            newRoute.Coordinates = new List<double[]>();
            newRoute.Markers = new List<Marker>();
            for (int i = 0; i < routes.Route.Length; i += 2)
            {
                Marker marker = new(latitude: routes.Route[i], longitude: routes.Route[i + 1]);
                newRoute.Markers.Add(marker);
                newRoute.Coordinates.Add(new double[] { routes.Route[i], routes.Route[i + 1] });
            }
            newRoute.Length = 2.3;
            newRoute.PickLengthType();
            using (var context = new DataContext())
            {
                int id = 0;
                context.Routes.Add(newRoute);
                try
                {
                    context.SaveChanges();
                }
                catch (DbUpdateException e)
                {
                    return -1;
                }

                foreach (var route in context.Routes)
                {
                    if (route.Name == newRoute.Name)
                    {
                        id = route.RouteId;
                        break;
                    }
                }

                //Log(this, new ClassLibrary.Logging.LogEventArgs("Save route", "Custom", newRoute.Name));
                return id;
            }
        }

        public int DeleteRoute(int Id)
        {
            using (var context = new DataContext())
            {
                var routeDelete = context.Routes.FirstOrDefault(e => e.RouteId == Id);
                context.Routes.Remove(routeDelete);
                foreach (var markerDelete in context.Markers)
                {
                    if (markerDelete.RouteId == Id)
                    {
                        context.Markers.Remove(markerDelete);
                    }
                }
                context.SaveChanges();
            }
            return 0;
        }


        public async Task<List<RoutesCounter>> GetRoutesNumbers()
        {
            return await Task.Run(() => CalculateRoutesNumbers());
        }
        private List<RoutesCounter> CalculateRoutesNumbers()
        {
            List<RoutesCounter> routeNumbers = new();
            using (var context = new DataContext())
            {
                var results = context.Routes.GroupBy(l => l.Type).Select(lg => new { Type = lg.Key, Routes = lg.Count() });
                foreach (var x in results)
                {
                    var type = x.Type.ToString();
                    routeNumbers.Add(new RoutesCounter(type, x.Routes));
                }

            }
            return routeNumbers;
        }
    }
}
