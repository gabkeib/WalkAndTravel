﻿using System;
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

        public void GenerateRoute()
        {
            POIList poiList = new POIList();
            poiList.SelectPOI(Amenity.Restaurant);
            var markers = new List<Marker>();
            markers.Add(StartingPoint);
            for (int i = 0; i < 2; i++)
            {
                markers.Add(poiList[i].Marker);
            }
            markers.Add(StartingPoint);
        }
    }
}