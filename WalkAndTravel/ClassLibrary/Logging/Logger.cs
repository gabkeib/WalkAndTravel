using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WalkAndTravel.ClassLibrary.Logging
{
    public class Logger
    {
        public static void Log(object sender, LogEventArgs e)
        {
            string log = $"Date: {DateTime.Now}, Action type: {e.ActionType}, Route type: {e.RouteType}, RouteName: {e.RouteName}";
            System.Diagnostics.Debug.WriteLine(log);
        }
    }
}
