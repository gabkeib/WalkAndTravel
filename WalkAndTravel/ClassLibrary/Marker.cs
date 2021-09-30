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

        public void SetLatitude(double lat)
        {
            this.lat = lat;
        }

        public double GetLatitude()
        {
            return this.lat;
        }

        public void SetLongitude(double lng)
        {
            this.lng = lng;
        }

        public double GetLongitude()
        {
            return this.lng;
        }
    }
}
