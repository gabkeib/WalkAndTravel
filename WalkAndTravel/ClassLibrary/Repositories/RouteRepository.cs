using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WalkAndTravel.ClassLibrary.DTO;
using WalkAndTravel.ClassLibrary.Models;
using WalkAndTravel.DataAccess;
using WalkAndTravel.Models;

namespace WalkAndTravel.ClassLibrary.Repositories
{
    public class RouteRepository : IRouteRepository
    {
        private DataContext context;
        public RouteRepository(DataContext context)
        {
            this.context = context;
        }
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
            return routes;
        }

        public async Task<int> SaveNewRoute(SaveDto dto)
        {
            if(dto.AuthorId == 0)
            {
                var user = await context.Users.FirstOrDefaultAsync();
                if (user == null)
                {
                    Random random = new();
                    const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
                    string randomPass = new string(Enumerable.Repeat(chars, 20)
                    .Select(s => s[random.Next(s.Length)]).ToArray());
                
                    User user1 = new User {
                        Email = "FirstEmail",
                        Age = 1,
                        Surname = "1",
                        Username = "First",
                        Level = 1,
                        Exp = 10,
                        Password = BCrypt.Net.BCrypt.HashPassword(randomPass)
                    };
                    context.Users.Add(user1);
                    context.SaveChanges();
                    var userId = await context.Users.FirstOrDefaultAsync(e => e.Email == user1.Email);
                    dto.AuthorId = userId.Id;
                }
                else
                {
                    var userId = await context.Users.FirstOrDefaultAsync(e => e.Email == user.Email);
                    dto.AuthorId = userId.Id;
                }
            }
            Route newRoute = new Route();
            newRoute.Name = dto.Name;
            newRoute.Coordinates = new List<double[]>();
            newRoute.Markers = new List<Marker>();
            double totalLength = 0;
            for (int i = 0; i < dto.Coords.Length; i += 2)
            {

                var sCoord = new Marker(dto.Coords[i], dto.Coords[i + 1]);
                if (i + 2 < dto.Coords.Length)
                {
                    var eCoord = new Marker(dto.Coords[i + 2], dto.Coords[i + 3]);
                    totalLength += MarkersCalculator.CalculateDistanceBetweenMarkers(sCoord, eCoord);
                }

                Marker marker = new(latitude: dto.Coords[i], longitude: dto.Coords[i + 1]);
                newRoute.Markers.Add(marker);
                newRoute.Coordinates.Add(new double[] { dto.Coords[i], dto.Coords[i + 1] });
            }

            newRoute.Length = totalLength;
            newRoute.AuthorId = dto.AuthorId;
            newRoute.PickLengthType();
            int id = 0;
            context.Routes.Add(newRoute);
            try
            {
                context.SaveChanges();
            }
            catch (DbUpdateException)
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

        public Route DeleteRoute(int Id)
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
            return routeDelete;
        }


        public async Task<List<RoutesCounter>> GetRoutesNumbers()
        {
            return await Task.Run(() => CalculateRoutesNumbers());
        }
        private List<RoutesCounter> CalculateRoutesNumbers()
        {
            List<RoutesCounter> routeNumbers = new();
            var results = context.Routes.GroupBy(l => l.Type).Select(lg => new { Type = lg.Key, Routes = lg.Count() });
            foreach (var x in results)
            {
                var type = x.Type.ToString();
                routeNumbers.Add(new RoutesCounter(type, x.Routes));
            }
            return routeNumbers;
        }

        public List<Route> SearchRoutes(string keyword)
        {
           if(keyword == null)
            {
                return RoutesSelector();
            }
            List<Route> routes = new();
            var searchRoute = context.Routes.Where(e => e.Name.ToLower().Trim().Contains(keyword.ToLower().Trim())).ToList();
            foreach(var route in searchRoute)
            {
                route.Markers = new List<Marker>();
                foreach (var marker in context.Markers)
                {
                    if (route.RouteId == marker.RouteId)
                    {
                        route.Markers.Add(marker);
                    }
                }
                routes.Add(route);
                route.Coordinates = Route.MarkersListToArray(route.Markers);
            }
            return routes;
        }

        public Route SearchRouteByID(int Id)
        {
            Route searchRoute = new();
            searchRoute = context.Routes.FirstOrDefault(e => e.RouteId == Id);
            searchRoute.Markers = new List<Marker>();
            foreach(var marker in context.Markers)
            {
                if(searchRoute.RouteId == marker.RouteId)
                {
                    searchRoute.Markers.Add(marker);
                }
            }
            searchRoute.Coordinates = Route.MarkersListToArray(searchRoute.Markers);
            return searchRoute;
        }

        public List<Route> SearchRoutesByAuthorID(int Id)
        {
            List<Route> routes = new();

            foreach (var route in context.Routes.ToList())
            {
                if (route.AuthorId == Id)
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


    }
}
