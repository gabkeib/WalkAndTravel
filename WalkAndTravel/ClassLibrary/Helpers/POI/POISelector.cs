using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WalkAndTravel.ClassLibrary
{
    public class POISelector : IPOISelector
    {
        private Lazy<POIData> _data;
        public POISelector()
        {
            _data = new Lazy<POIData>(() =>
            {
                return new POIData();
            });
        }

        public POIData Data
        {
            get
            {
                return _data.Value;
            }
        }
        public POIList SelectPOI(Amenity amenity = Amenity.Undefined)
        {
            var poiData = new POIList();
            if (amenity.Equals(Amenity.Undefined))
            {
                var index = 0;
                foreach (var poi in Data)
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
                    from poiOb in Data
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
