

import 'package:latlong2/latlong.dart';

class Route {
  String name;
  num? length;
  List<LatLng> coordinates;
  int type;

  Route(this.name, this.length, this.coordinates, this.type);
}