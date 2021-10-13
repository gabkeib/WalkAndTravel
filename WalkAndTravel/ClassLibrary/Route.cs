using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WalkAndTravel.ClassLibrary
{
    public class Route
    {
        private string _name;

        private double length;

        private List<Marker> markers;

        private List<double[]> coordinates;

        public string Name
        {
            get { return _name; }
            set { _name = value; }
        }

        public double Length
        {
            get { return length; }
            set { length = value; }
        }

        public List<Marker> Markers
        {
            get { return markers; }
            set { markers = value; }
        }

        public List<double[]> Coordinates
        {
            get { return coordinates; }
            set { coordinates = value; }
        }

        public void AddMarker(Marker marker)
        {
            markers.Add(marker);
            AddCoordinate(marker);
        }

        public void AddCoordinate(Marker marker)
        {
            double lat = marker.Latitude;
            double lng = marker.Longitude;
            double[] coord = new double[] { lat, lng };
            coordinates.Add(coord);
        }


        public static List<double[]> markersListToArray(List<Marker> list)
        {
            var newList = new List<double[]>();
            foreach (var marker in list)
            {
                double lat = marker.Latitude;
                double lng = marker.Longitude;
                double[] coord = new double[] { lat, lng };
                newList.Add(coord);
            }
            return newList;
        }
    }
}
