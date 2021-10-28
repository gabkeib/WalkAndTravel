using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WalkAndTravel.ClassLibrary
{
    public class POISelector
    {
        public POIList SelectPOI(Amenity amenity = Amenity.Undefined)
        {
            var data = new POIData();
            var poiData = new POIList();
            if (amenity.Equals(Amenity.Undefined))
            {
                var index = 0;
                foreach (var poi in data)
                {
                    poiData[index] = (POI) poi;
                    index++;
                    if (index >= poiData.Length)
                    {
                        break;
                    }
                }
            }
            else
            {
                var filteredData =
                    from poiOb in data
                    where poiOb.Amenity == amenity
                    select poiOb;

                var index = 0;
                foreach (var poi in filteredData)
                {
                    poiData[index] = (POI) poi;
                    index++;
                    if (index >= poiData.Length)
                    {
                        break;
                    }
                }
            }
            return poiData;
        }
    }
}
