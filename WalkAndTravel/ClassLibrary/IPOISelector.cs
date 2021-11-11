using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WalkAndTravel.ClassLibrary
{
    public interface IPOISelector
    {
        public POIList SelectPOI(Amenity amenity = Amenity.Undefined);
    }
}
