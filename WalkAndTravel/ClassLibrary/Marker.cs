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

        public Marker(double lat, double lng)
        {
            _lat = lat; _lng = lng;
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
