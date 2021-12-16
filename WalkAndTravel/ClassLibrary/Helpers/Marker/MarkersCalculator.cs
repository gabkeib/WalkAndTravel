using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WalkAndTravel.ClassLibrary
{
    public static class MarkersCalculator
    {
        private const double EarthRadius = 6378.1;
        public static Marker CalculateNextMarker(this Marker marker, double distance, double bearing)
        {
            Marker newMarker;
            bearing = bearing % 360.0;
            bearing = bearing.ToRadians();
            var lat1 = marker.Latitude.ToRadians();
            var lng1 = marker.Longitude.ToRadians();
            var lat2 = Math.Asin(Math.Sin(lat1) * Math.Cos(distance / EarthRadius) + Math.Cos(lat1) * Math.Sin(distance / EarthRadius) * Math.Cos(bearing));
            var lng2 = lng1 + Math.Atan2(Math.Sin(bearing) * Math.Sin(distance / EarthRadius) * Math.Cos(lat1), Math.Cos(distance / EarthRadius) - Math.Sin(lat1) * Math.Sin(lat2));
            lat2 = lat2.ToDegrees();
            lng2 = lng2.ToDegrees();
            newMarker = new Marker(lat2, lng2);
            return newMarker;
        }

        public static double CalculateDistanceBetweenMarkers(this Marker startMarker, Marker endMarker)
        {
            var lat1 = startMarker.Latitude.ToRadians();
            var lat2 = endMarker.Latitude.ToRadians();
            var deltaLat = (startMarker.Latitude - endMarker.Latitude).ToRadians();
            var deltaLng = (startMarker.Longitude - endMarker.Longitude).ToRadians();
            var a = Math.Sin(deltaLat / 2) * Math.Sin(deltaLat / 2) + Math.Cos(lat1) * Math.Cos(lat2) * Math.Sin(deltaLng / 2) * Math.Sin(deltaLng / 2);
            var c = 2 * Math.Atan2(Math.Sqrt(a), Math.Sqrt(1.0 - a));
            return EarthRadius * c;
        }

        public static double ToRadians(this double degrees)
        {
            return (Math.PI / 180.0) * degrees;
        }

        public static double ToDegrees(this double radians)
        {
            return (180.0 / Math.PI) * radians;
        }

    }
}
