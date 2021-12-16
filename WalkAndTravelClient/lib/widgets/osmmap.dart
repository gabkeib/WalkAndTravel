import 'dart:convert';

import 'package:flutter_map/flutter_map.dart';
import 'package:flutter_map_marker_popup/flutter_map_marker_popup.dart';
import 'package:http/http.dart' as http;
import 'package:flutter/material.dart';
import 'package:flutter/foundation.dart';
import 'package:geocoding/geocoding.dart';

import 'package:latlong2/latlong.dart';
import 'package:maps_toolkit/maps_toolkit.dart' show PolygonUtil;
import 'package:walk_and_travel/models/route.dart' as a;
import 'package:walk_and_travel/models/poi.dart';


class OSMMap extends StatefulWidget {
  const OSMMap({Key? key, required this.currentRoute, required this.creationMode, required this.callbackRoute, required this.callbackSaveRoute, required this.centerPosition}) : super(key: key);

  final Function(a.Route) callbackRoute;
  final Function(a.Route) callbackSaveRoute;
  final a.Route currentRoute;
  final LatLng centerPosition;
  final bool creationMode;


  @override
  _OSMMapState createState() => _OSMMapState();
}


class _OSMMapState extends State<OSMMap> {
  List<Marker> markers = [];
  List<Polyline> polylines = [];
  List<POI> poiList = [];
  List<Marker> pois = [];
  LatLng centerPos = LatLng(54.68585, 25.28647);

  final PopupController _popupLayerController = PopupController();
  final MapController _mapController = MapController();

  /*
  For getting POI
  https://overpass-api.de/api/interpreter?data=[out:json];(node[~"^(tourism)$"~"."](around:500,54.68585,25.28647);-node["tourism"~"hotel|hostel|guest_house|apartment|motel"](around:500,54.68585,25.28647););out;
   */

  @override
  initState() {
    super.initState();


    prepareRoute();
  }

  void prepareRoute() {
   // print(widget.centerPosition);
    print("Test");
    print(widget.centerPosition);
    print(centerPos);
    if (centerPos != widget.centerPosition){

      setState(() {
        centerPos = widget.centerPosition;
        _mapController.move(centerPos, _mapController.zoom);
      });

    }
    List<LatLng> list = convertToLatLng(markers);
    if (!listEquals(list,widget.currentRoute.coordinates)) {
      prepareRouteMarkers();
    }

  }

  List<LatLng> convertToLatLng(List<Marker> list){
    List<LatLng> newList = [];
    for (var marker in list){
      newList.add(marker.point);
    }
    return newList;
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
    print("testinPrep");
   // print(widget.currentRoute.coordinates);
   // print(lastRoute?.coordinates);
   // print(DateTime.now().millisecondsSinceEpoch);

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
              const Icon(
                Icons.place,
                color: Colors.red,
                size: 45,
              )
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
    //print(widget.currentRoute.coordinates);
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

  Future _getPOIAroundMarker(LatLng coordinates) async {
    var urlString = "https://overpass-api.de/api/interpreter?data=[out:json];(node[~%22^(tourism)%24%22~%22.%22](around:500,${coordinates.latitude},${coordinates.longitude});-node[%22tourism%22~%22hotel|hostel|guest_house|apartment|motel%22](around:500,${coordinates.latitude},${coordinates.longitude}););out 20;";
    var url = Uri.parse(urlString);
    http.Response response = await http.get(url);
    Map<String, dynamic> value = jsonDecode(utf8.decode(response.bodyBytes));
    List<POI> points = [];
    for (var x in value["elements"]){
      print(x);
      String? name = x["tags"]["name"];
      String amenity = x["tags"]["tourism"];
      LatLng coordinates = LatLng(x["lat"], x["lon"]);
      POI poi = POI(name, amenity, coordinates);
      points.add(poi);
    }
    setState(() {
      for (var poi in points){
        Marker marker = Marker(
            point: poi.coordinates,
            width: 60.0,
            height: 60.0,
            builder: (BuildContext context) {
                switch (poi.amenity){
                  case "attraction":
                      return const Icon(
                        Icons.attractions,
                        color: Colors.brown,
                        size: 45,
                      );
                    break;
                  case "museum":
                    return const Icon(
                      Icons.museum,
                      color: Colors.brown,
                      size: 45,
                    );
                    break;
                  case "artwork":
                    return const Icon(
                      Icons.palette,
                      color: Colors.brown,
                      size: 45,
                    );
                    break;
                  default : {
                    return const Icon(
                      Icons.place,
                      color: Colors.brown,
                      size: 45,
                    );
                  }
                }
            }
        );
        pois.add(marker);
        poiList.add(poi);
      }
    });
  }

  Future<String?> _geocodeMarker(LatLng marker) async{
    List<Placemark> placemarks = await placemarkFromCoordinates(marker.latitude, marker.longitude);
    var place = placemarks[0];
    return (place.street);
  }

  void _addMarker(LatLng latlng){
    var m = Marker(
      width: 80.0,
      height: 80.0,
      point: latlng,
      builder: (ctx) =>
        const Icon(
          Icons.place,
          color: Colors.red,
          size: 45,
        ),
    );
    setState((){
      _geocodeMarker(latlng);
      markers.add(m);
      int id = 0;
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

  void _deleteMarker(Marker marker){
    setState(() {
      List<LatLng> coordinates = [];
      for (var _marker in markers){
        if (_marker != marker) {
          coordinates.add(LatLng(_marker.point.latitude, _marker.point.longitude));
        }
      }
      markers.removeWhere((element) => element == marker);
      a.Route route = a.Route("Name", 3.4, coordinates, 2);
      widget.callbackRoute(route);
      _popupLayerController.hideAllPopups();
      if (markers.length > 1) {
        prepareRoutePolyline(_getCoordinatesString());
      }
      else {
        polylines = [];
      }
    });
  }

  Future<dynamic> createPOIAlertDialog(BuildContext context){

    return showDialog(context: context, builder: (context) {
      return AlertDialog(
        title: Text("Do you want to erase all POIs?"),
        actions: <Widget>[
          MaterialButton(
              elevation: 5.0,
              child: Text('No'),
              onPressed: () {

                Navigator.of(context).pop();
              }
          ),
          MaterialButton(
              elevation: 5.0,
              child: Text('Yes'),
              onPressed: () {
                setState(() {
                  poiList.clear();
                  pois.clear();
                });
                Navigator.of(context).pop();
              }
          ),
        ],
      );
    });
  }

  void _clearPOI(){
    createPOIAlertDialog(context);
  }

  Future<String> _findPOIname(Marker marker) async{
    String name = "-";
    print("Here");
      for (var x in poiList) {
        if (marker.point == x.coordinates) {
          name = x.name ?? name;
        }
      }
    return name;
  }

  Widget _deleteMarkerWidget(Marker marker){
    return Card(
      child: TextButton(
        onPressed: () => _deleteMarker(marker),
        child: const Text(
          'Delete marker'
        )
      )
    );
  }

  Widget _infoMarkerWidget(Marker marker){
    return Card(
        child: Column(
            mainAxisSize: MainAxisSize.min,
          children: [
            FutureBuilder<String?>(
            future: _findPOIname(marker),
            builder: (BuildContext context, AsyncSnapshot<String?> snapshot) {
              if (snapshot.hasData){
                return Text("Name: " + (snapshot.data ?? "-"));
              }
              else {
                return Text("Name: -");
              }
            }),
            TextButton(
            child: Text(
                'Get Points Of Interest around here'
              ),
          onPressed: () => _getPOIAroundMarker(marker.point),
        ),
        ]
        )
    );
  }

  @override
  Widget build(BuildContext context) {
    prepareRoute();
    return FlutterMap(
      mapController: _mapController,
      options: MapOptions(
        center: centerPos,
        zoom: 15.0,
        onTap: (widget.creationMode) ? (x, point) => _addMarker(point) : (x, point) => _popupLayerController.hideAllPopups(),
        onLongPress: (x, point) => _clearPOI(),
      ),
      children: [
          TileLayerWidget(
            options: TileLayerOptions(
              urlTemplate: 'https://{s}.tile.openstreetmap.org/{z}/{x}/{y}.png',
              subdomains: ['a', 'b', 'c'],
              attributionBuilder: (_) {
                return Text("© OpenStreetMap contributors");
              },
            ),
          ),
        PolylineLayerWidget(options:  PolylineLayerOptions(
            polylines: polylines
        )),
          PopupMarkerLayerWidget(
            options: PopupMarkerLayerOptions(
              popupController: _popupLayerController,
              markers: [...markers, ...pois],
              markerRotateAlignment:
              PopupMarkerLayerOptions.rotationAlignmentFor(AnchorAlign.top),
              popupBuilder: (BuildContext context, Marker marker)
                  {
                   return ((widget.creationMode) ? _deleteMarkerWidget(marker) : _infoMarkerWidget(marker));
                  }
            ),
          ),


        ],
    );
  }
}