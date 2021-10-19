using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WalkAndTravel.ClassLibrary
{
    public class POIList
    {
        private List<POI> _data;

        private POI[] _poiData = new POI[20];
        public POI this[int index]
        {
            get => _poiData[index];
            set => _poiData[index] = value;
        }

        public POIList()
        {
            _data = new List<POI>();
            _data.Add(new POI("Misterija", new Marker(54.6846882, 25.28233), Amenity.Pub));
            _data.Add(new POI(longitude: 25.2820894, latitude: 54.6800829, amenity:Amenity.Pub, name: "Mojitos"));
            _data.Add(new POI("Carre", new Marker(54.6872226, 25.2820945), Amenity.Restaurant));
            _data.Add(new POI("Grey", 54.6845579, 25.2898092, Amenity.Restaurant));
            _data.Add(new POI("Balti drambliai", 54.6815214, 25.2808191, Amenity.Restaurant));
            _data.Add(new POI("Elska Coffee", 54.6848498, 25.2771526, Amenity.Cafe));
        }

        public void SelectPOI(Amenity amenity = Amenity.Undefined)
        {
            if (amenity.Equals(Amenity.Undefined))
            {
                var index = 0;
                foreach (var poi in _data)
                {
                    _poiData[index] = poi;
                    if (index >= _poiData.Length)
                    {
                        break;
                    }
                }
            }
            else
            {
                var filteredData =
                    from poiOb in _data
                    where poiOb.Amenity == amenity
                    select poiOb;

                var index = 0;
                foreach (var poi in filteredData)
                {
                    _poiData[index] = poi;
                    if (index >= _poiData.Length)
                    {
                        break;
                    }
                }
            }
        }
    }
}
