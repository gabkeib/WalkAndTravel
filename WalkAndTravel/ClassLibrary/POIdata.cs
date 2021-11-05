using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WalkAndTravel.ClassLibrary
{
    public class POIData : IEnumerable<POI>
    {
        private List<POI> _data;
        public POIData()
        {
            _data = new List<POI>
            {
                new POI("Misterija", new Marker(54.6846882, 25.28233), Amenity.Pub),
                new POI(longitude: 25.2820894, latitude: 54.6800829, amenity: Amenity.Pub, name: "Mojitos"),
                new POI("Carre", new Marker(54.6872226, 25.2820945), Amenity.Restaurant),
                new POI("Grey", 54.6845579, 25.2898092, Amenity.Restaurant),
                new POI("Balti drambliai", 54.6815214, 25.2808191, Amenity.Restaurant),
                new POI("Elska Coffee", 54.6848498, 25.2771526, Amenity.Cafe)
            };
        }

        
        IEnumerator<POI> IEnumerable<POI>.GetEnumerator()
        {
            return ((IEnumerable<POI>)_data).GetEnumerator();
        }

        public IEnumerator GetEnumerator()
        {
            return ((IEnumerable)_data).GetEnumerator();
        }
    }
}
