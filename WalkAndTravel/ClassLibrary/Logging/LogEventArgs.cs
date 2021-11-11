using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WalkAndTravel.ClassLibrary.Logging
{
    public class LogEventArgs : EventArgs
    {
        public string ActionType { get; set; }
        public string RouteType { get; set; }
        public string RouteName { get; set; }

        public LogEventArgs(string actionType, string routeType, string routeName)
        {
            ActionType = actionType;
            RouteType = routeType;
            RouteName = routeName;
        }
    }
}
