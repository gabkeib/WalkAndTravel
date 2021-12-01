import 'dart:convert';

import 'package:flutter_map/flutter_map.dart';
import 'package:http/http.dart' as http;
import 'package:flutter/material.dart';

import 'package:latlong2/latlong.dart';
import 'package:maps_toolkit/maps_toolkit.dart' show PolygonUtil;
import 'package:walk_and_travel/models/route.dart' as a;


class OSMMap extends StatefulWidget {
  const OSMMap({Key? key, required this.currentRoute, required this.creationMode, required this.callbackRoute, required this.callbackSaveRoute}) : super(key: key);

  final Function(a.Route) callbackRoute;
  final Function(a.Route) callbackSaveRoute;
  final a.Route currentRoute;
  final bool creationMode;


  @override
  _OSMMapState createState() => _OSMMapState();
}


class _OSMMapState extends State<OSMMap> {
  a.Route? lastRoute;
  List<Marker> markers = [];
  List<Polyline> polylines = [];

  @override
  initState() {
    super.initState();
    prepareRoute();
  }

  void prepareRoute() {
    if (lastRoute != widget.currentRoute) {
      prepareRouteMarkers();
    }
  }

  String _getCoordinatesString(){
    String s = "";
    for (var marker in markers){
      var lng = marker.point.longitude;
      var lat = marker.point.latitude;
      s += lng.toString();
      s += ',';
      s += lat.toString();
      s += ';';
    }
    if (s.isNotEmpty) {
      s = s.substring(0, s.length - 1);
    }
    return s;
  }

  void prepareRouteMarkers() {
    lastRoute = widget.currentRoute;
    if (widget.currentRoute.coordinates.length > 1) {
      markers = [];
      String coordinates = "";
      var route = widget.currentRoute;
      print(route);
      for (var coords in widget.currentRoute.coordinates) {
        var m = Marker(
          width: 80.0,
          height: 80.0,
          point: coords,
          builder: (ctx) =>
              Container(
                child: FlutterLogo(),
              ),
        );
        markers.add(m);
        coordinates += (coords.longitude).toString();
        coordinates += ',';
        coordinates += (coords.latitude).toString();
        coordinates += ';';
      }
      if (coordinates.isNotEmpty) {
        coordinates = coordinates.substring(0, coordinates.length - 1);
      }
      prepareRoutePolyline(coordinates);
    }
    else {
      if (widget.currentRoute.coordinates.isEmpty) {
        markers.clear();
        polylines.clear();
      }
    }
  }

  Future prepareRoutePolyline(String coordinates) async {
    var urlString = "http://router.project-osrm.org/route/v1/foot/${coordinates}?overview=simplified";
    var url = Uri.parse(urlString);
    http.Response response = await http.get(url);
    Map<String, dynamic> value = jsonDecode(response.body);
    var polylinePoints = PolygonUtil.decode(value['routes'][0]['geometry']);
    List<LatLng> newRoute = [];
    for (var x in polylinePoints) {
      newRoute.add(LatLng(x.latitude, x.longitude));
    }
    setState(() {
      polylines.clear();
      polylines.add(
          Polyline(
              points: newRoute,
              strokeWidth: 5,
              color: Colors.blue
          ));
    });
  }

  void _addMarker(LatLng latlng){
    print(latlng);
    var m = Marker(
      width: 80.0,
      height: 80.0,
      point: latlng,
      builder: (ctx) =>
          Container(
            child: FlutterLogo(),
          ),
    );
    setState((){
      markers.add(m);
      List<LatLng> coordinates = [];
      for (var marker in markers){
        coordinates.add(LatLng(marker.point.latitude, marker.point.longitude));
      }
      a.Route route = a.Route("Name", 3.4, coordinates, 2);
      widget.callbackRoute(route);
      if (markers.length > 1) {
        prepareRoutePolyline(_getCoordinatesString());
      }
    });
  }

  @override
  Widget build(BuildContext context) {
    prepareRoute();
    return FlutterMap(
      options: MapOptions(
        center: LatLng(54.68585, 25.28647),
        zoom: 15.0,
        onTap: (widget.creationMode) ? (x, point) => _addMarker(point) : null,
      ),
      layers: [
        TileLayerOptions(
          urlTemplate: "https://{s}.tile.openstreetmap.org/{z}/{x}/{y}.png",
          subdomains: ['a', 'b', 'c'],
          attributionBuilder: (_) {
            return Text("© OpenStreetMap contributors");
          },
        ),
        MarkerLayerOptions(
            markers: markers
        ),
        PolylineLayerOptions(
            polylines: polylines
        )
      ],

    );
  }
}