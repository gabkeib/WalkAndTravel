using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace WalkAndTravel.ClassLibrary
{
    public class RoutesIO
    {
        public void WriteRouteToFile(List<Route> routes, string path)
        {
            string Json = JsonConvert.SerializeObject(routes);

            File.WriteAllText(path, Json);
        }

        public List<Route> ReadRoutesFromFile(string path)
        {
            List<Route> Routes = new();

            if (File.Exists(path))
            {
                var route = File.ReadAllText(path);
                Routes = JsonConvert.DeserializeObject<List<Route>>(route);
            }

            return Routes;
        }
    }
}
