using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace WalkAndTravel.ClassLibrary
{
    public class Route : IComparable<Route>
    {
        private string _name;

        private double _length;

        private List<Marker> _markers;

        private List<double[]> _coordinates;

        private LengthType _type;

        public Route()
        {
            _coordinates = new List<double[]>();
            _markers = new List<Marker>();
        }
        public Route( double length, List<Marker> markers, List<double[]> coords = null, string name = "None", LengthType type = LengthType.None)
        { _name = name; _length = length; _markers = markers; _coordinates = coords; _type = type; _coordinates = new List<double[]>(); _markers = new List<Marker>(); }

        [Column("Id")]
        public int RouteId { get; set; }

        public string Name
        {
            get { return _name; }
            set { 
                if (NameValidator.isValid(value))
                    _name = value;
            }
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

        [NotMapped]
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
            if (list.Count > 0)
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
            else
            {
                return new List<double[]>();
            }
        }

        public void PickLengthType()
        {
            if(_length <= 0.5)
            {
                _type = LengthType.Short;
            }
            else if(_length < 1.3)
            {
                _type = LengthType.Medium;
            }
            else if(_length < 3)
            {
                _type = LengthType.Long;
            }
            else
            {
                _type = LengthType.VeryLong;
            }
        }

        public int CompareTo(Route other)
        {
            if (_type == other.Type)
            {
                if (_length == other.Length)
                {
                    return _name.CompareTo(other.Name);
                }
                else if(_length < other.Length)
                {
                    return -1;
                }
                else return 1;
            }
            else if(_type < other.Type)
            {
                return -1;
            }
            else return 1;
        }
    }
}
