using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WalkAndTravel.ClassLibrary
{
    public class Route
    {
        private double length;

        private List<Marker> markers = new();

        public void SetLength(double length)
        {
            this.length = length;
        }

        public double GetLength()
        {
            return this.length;
        }

        public void SetMarkers(List<Marker> markers)
        {
            this.markers = markers;
        }

        public List<Marker> GetMarkers()
        {
            return this.markers;
        }

        public void AddMarker(Marker marker)
        {
            markers.Add(marker);
        }
    }
}
