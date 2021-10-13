using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WalkAndTravel.ClassLibrary
{
    public class Route
    {
        private string _name;

        private double _length;

        private List<Marker> _markers;

        private List<double[]> _coordinates;

        private LengthType _type;

        public string Name
        {
            get { return _name; }
            set { _name = value; }
        }

        public double Length
        {
            get { return _length; }
            set { _length = value; }
        }

        public List<Marker> Markers
        {
            get { return _markers; }
            set { _markers = value; }
        }

        public List<double[]> Coordinates
        {
            get { return _coordinates; }
            set { _coordinates = value; }
        }

        public LengthType Type
        {
            get { return _type; }
            set { _type = value; }
        }

        public void AddMarker(Marker marker)
        {
            _markers.Add(marker);
            AddCoordinate(marker);
        }

        public void AddCoordinate(Marker marker)
        {
            double lat = marker.Latitude;
            double lng = marker.Longitude;
            double[] coord = new double[] { lat, lng };
            _coordinates.Add(coord);
        }


        public static List<double[]> MarkersListToArray(List<Marker> list)
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

        public LengthType PickLengthType()
        {
            if(_length <= 0.5)
            {
                return LengthType.Short;
            }
            else if(_length < 1.3)
            {
                return LengthType.Medium;
            }
            else if(_length < 3)
            {
                return LengthType.Long;
            }
            else
            {
                return LengthType.VeryLong;
            }
        }
    }
}
