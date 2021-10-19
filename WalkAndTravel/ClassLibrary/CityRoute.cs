using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WalkAndTravel.ClassLibrary
{
    public class CityRoute : Route
    {
        public bool GetToStart
        {
            get; set;
        }
        public int DistanceFromStart
        {
            get; set;
        }

        public Marker StartingPoint
        {
            get; set;
        }

        public int MarkersCount
        {
            get; set;
        }

        public CityRoute(Marker startingPoint, int distanceFromPoint, int markersCount = 3, bool getToStart = true)
        {
            StartingPoint = startingPoint;
            DistanceFromStart = distanceFromPoint;
            MarkersCount = markersCount;
            GetToStart = getToStart;
        }

        public void GenerateRoute()
        {
            var averageDistance = (double)DistanceFromStart / (double)MarkersCount;
            var rnd = new Random();
            var bearing = rnd.Next(30, 359) + rnd.NextDouble();
            var current = StartingPoint;
            var markers = new List<Marker>() { current };
            for (int i = 0; i < MarkersCount; i++)
            {
                current = current.CalculateNextMarker(averageDistance, bearing);
                markers.Add(current);
                bearing = rnd.Next(30, 359) + rnd.NextDouble();
            }
            if (GetToStart)
            {
                markers.Add(StartingPoint);
            }
            Markers = markers;
        }
    }
}
