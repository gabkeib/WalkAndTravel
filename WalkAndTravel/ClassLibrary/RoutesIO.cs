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

        /*
         * Writes a list of routes to json file
         */

        public static void WriteRoutesToFile<T>(T routes, string path)
        {
            using (var streamWriter = new StreamWriter(path))
            {
                var json = JsonConvert.SerializeObject(routes);
                streamWriter.Write(json);
            }
        }

        /*
         * Reads from a json file and returns routes list 
         */

        public static List<T> ReadRoutesFromFile<T>(string path)
        {
            List<T> Routes = new();

            if (File.Exists(path))
            {
                using (var streamReader = new StreamReader(path))
                {
                    var json = streamReader.ReadToEnd();
                    Routes = JsonConvert.DeserializeObject<List<T>>(json);
                }
            }

            return Routes;
        }
    }
}
