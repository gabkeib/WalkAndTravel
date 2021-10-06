using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WalkAndTravel.ClassLibrary
{
    public class Route
    {
        private double length;

        private List<Marker> markers;

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

        public void AddMarker(Marker marker)
        {
            markers.Add(marker);
        }
    }
}
