using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WalkAndTravel.ClassLibrary
{
    public class Marker
    {
        private double lat;
        private double lng;

        public Marker(double lat, double lng)
        {
            this.lat = lat; this.lng = lng;
        }

        public double Latitude
        {
            get { return lat; }
            set { lat = value; }
        }

        public double Longitude
        {
            get { return lng; }
            set { lng = value; }
        }

    }
}
