using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WalkAndTravel.ClassLibrary
{
    public class SightseeingRoute : Route
    {
        private IPOISelector _poiSelector;
        public int DistanceFromStart
        {
            get;
        }

        public Marker StartingPoint
        {
            get; set;
        }

        public SightseeingRoute(IPOISelector POISelector, Marker startingPoint, int distanceFromPoint)
        {
            StartingPoint = startingPoint;
            DistanceFromStart = distanceFromPoint;
            _poiSelector = POISelector;
        }

        public void GenerateRoute()
        {
            POIList poiList = _poiSelector.SelectPOI();
            System.Diagnostics.Debug.WriteLine(poiList[1].Marker.Latitude);
            var markers = new List<Marker>();
            markers.Add(StartingPoint);
            for (int i = 0; i < 2; i++)
            {
                markers.Add(poiList[i].Marker);
            }
            markers.Add(StartingPoint);
            Markers = markers;
        }
    }
}
