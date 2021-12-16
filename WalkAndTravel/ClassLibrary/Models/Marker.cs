using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WalkAndTravel.ClassLibrary
{
    public class Marker
    {

        private double _lat;
        private double _lng;

        public Marker(double latitude, double longitude)
        {
            _lat = latitude; _lng = longitude;
            if (_lat > 90 || _lat < -90)
            {
                throw new Exceptions.IllegalLatLngException("Illegal latitude. Must be in interval [-90, 90]");
            }
            if (_lng > 180 || _lng < -180)
            {
                throw new Exceptions.IllegalLatLngException("Illegal longitude. Must be in interval [-180, 180]");
            }

        }

        public int Id { get; set; }

        public int RouteId { get; set; }

        public double Latitude
        {
            get { return _lat; }
            set {
                if (value> 90 || value < -90)
                {
                    throw new Exceptions.IllegalLatLngException("Illegal latitude. Must be in interval [-90, 90]");
                }
                else _lat = value; 
            }
        }

        public double Longitude
        {
            get { return _lng; }
            set {
                if (value > 180 || value < -180)
                {
                    throw new Exceptions.IllegalLatLngException("Illegal longitude. Must be in interval [-180, 180]");
                }
                else 
                    _lng = value; 
            }
        }

    }
}
