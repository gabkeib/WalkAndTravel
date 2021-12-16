import 'package:latlong2/latlong.dart';

class POI {
  String? name;
  LatLng coordinates;
  String amenity;
  POI(this.name, this.amenity, this.coordinates);
}