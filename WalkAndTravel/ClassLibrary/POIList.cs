using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WalkAndTravel.ClassLibrary
{
    public class POIList
    {

        public int Length => _poiData.Length;

        private POI[] _poiData = new POI[20];
        public POI this[int index]
        {
            get => _poiData[index];
            set => _poiData[index] = value;
        }

    }
}
