using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WalkAndTravel.ClassLibrary
{
    public struct Marker
    {

        private double _lat;
        private double _lng;

        public Marker(double lat, double lng)
        {
            _lat = lat; _lng = lng;
            if (_lat > 90 || _lat < -90)
            {
                throw new Exceptions.IllegalLatLngException("Illegal latitude. Must be in interval [-90, 90]");
            }
            if (_lng > 180 || _lng < -180)
            {
                throw new Exceptions.IllegalLatLngException("Illegal longitude. Must be in interval [-180, 180]");
            }

        }

        public double Latitude
        {
            get { return _lat; }
            set { _lat = value; }
        }

        public double Longitude
        {
            get { return _lng; }
            set { _lng = value; }
        }

    }
}
