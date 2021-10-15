using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WalkAndTravel.ClassLibrary
{
    public class SightseeingRoute : Route
    {
        public int DistanceFromStart
        {
            get;
        }

        public Marker StartingPoint
        {
            get; set;
        }

        public SightseeingRoute(Marker startingPoint, int distanceFromPoint)
        {
            StartingPoint = startingPoint;
            DistanceFromStart = distanceFromPoint;
        }
    }
}
