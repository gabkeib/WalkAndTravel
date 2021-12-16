using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WalkAndTravel.ClassLibrary
{
    public class POI
    {
        public string Name
        {
            get; set;
        }

        public Marker Marker
        {
            get; set;
        }

        public Amenity Amenity
        {
            get; set;
        }

        public POI(string name, Marker marker, Amenity amenity)
        {
            Name = name;
            Marker = marker;
            Amenity = amenity;
        }

        public POI(string name, double latitude, double longitude, Amenity amenity)
        {
            Name = name;
            Marker = new Marker(latitude, longitude);
            Amenity = amenity;
        }
    }
}
