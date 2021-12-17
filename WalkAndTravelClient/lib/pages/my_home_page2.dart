import 'dart:convert';

import 'package:flutter/cupertino.dart';
import 'package:flutter/material.dart';
import 'package:geocoding/geocoding.dart';
import 'package:latlong2/latlong.dart';
import 'package:material_floating_search_bar/material_floating_search_bar.dart';
import 'package:sliding_up_panel/sliding_up_panel.dart';
import 'package:snippet_coder_utils/FormHelper.dart';
import 'package:walk_and_travel/models/experience_gainer.dart';
import 'package:walk_and_travel/models/location_marker.dart';
import 'package:walk_and_travel/models/route_with_id.dart';
import 'package:walk_and_travel/models/route_with_id.dart';
import 'package:walk_and_travel/models/route_with_id.dart';
import 'package:walk_and_travel/models/route_with_id.dart';
import 'package:walk_and_travel/services/api_service.dart';
import 'package:walk_and_travel/services/shared_service.dart';
import 'package:walk_and_travel/widgets/osmmap.dart';
import '../config.dart';
import '../models/route.dart' as a;
import '../models/minimal_route.dart' as b;
import 'package:http/http.dart' as http;

class MyHomePage2 extends StatefulWidget {
  const MyHomePage2({Key? key, required this.tokenMain}) : super(key: key);
  final String tokenMain;

  @override
  State<MyHomePage2> createState() => _MyHomePage2State();
}

class _MyHomePage2State extends State<MyHomePage2> {
  int userId = 6;
  String username = "User";
  String email = "";
  int level = 0;
  int exp = 0;
  int _EndExp = 0;
  final List<LocationMarker> _locations = [];
  final List<a.Route> _routes = [];
  a.Route _currentRoute = a.Route(
      "newRoute", 2.3, [LatLng(54.6866, 25.2865), LatLng(54.6902, 25.2764)], 0);
  final PanelController _panelController = PanelController();
  bool createRouteWindow = false;
  LatLng centerPosition = LatLng(54.68585, 25.28647);
  String searchString = "";

  final FloatingSearchBarController _floatingSearchBarController = FloatingSearchBarController();

  @override
  void initState() {
    super.initState();
    setUserDetails();
    getRoutes();
  }

  void _incrementCounter() {
    setState(() {
      getRoutes();
    });
  }

  void _getRandomRoute() async {
    var url = Uri.parse("http://10.0.2.2:5000/route/randomroute");
    http.Response response = await http.get(url);
    var value = jsonDecode(response.body);
    String? routeName = value['name'];
    num length = value['length'];
    List<LatLng> coordinates = [];
    for (var marker in value['coordinates']) {
      LatLng coordinate = LatLng(marker[0], marker[1]);
      coordinates.add(coordinate);
    }
    int type = value['type'];
    a.Route route = a.Route("routeName", length, coordinates, type);
    _updateCurrentRoute(route);
  }

  void _updateCurrentRoute(a.Route route) {
    setState(() {
      _currentRoute = route;
    });
  }

  void getRoutes() async {
    var url = Uri.parse("http://10.0.2.2:5000/route/Author/$userId");
    http.Response response = await http.get(url);
    //print(response.body);
    var value = jsonDecode(response.body);
    var elements = value.length;
    setState(() {
      _routes.clear();
      for (var i = 0; i < elements; i++) {
        String routeName = value[i]['name'];
        num length = value[i]['length'];
        List<LatLng> coordinates = [];
        for (var marker in value[i]['coordinates']) {
          LatLng coordinate = LatLng(marker[0], marker[1]);
          coordinates.add(coordinate);
        }
        int type = value[i]['type'];
        a.Route route = a.Route(routeName, length, coordinates, type);
        _routes.add(route);
      }
    });
  }

  void sendRoute(a.Route route) async {
    List<double> convCoordinates = [];
    for (var pos in route.coordinates){
      convCoordinates.add(pos.latitude);
      convCoordinates.add(pos.longitude);
    }
    print(convCoordinates);
    RouteWithId newRoute = RouteWithId(route.name, userId, convCoordinates);
    print(newRoute.coords);
    var url = Uri.parse("http://10.0.2.2:5000/route/SaveRouteWithId");
    http.Response response = await http.post(
        url,
        headers: <String, String>{
          'Accept': 'application/json',
          'Content-Type': 'application/json'
        },
        body: jsonEncode(newRoute)
    );
    print('Response status: ${response.statusCode}');
  }

  void sendExp() async {
    ExperienceGainer experienceGainer = ExperienceGainer(email, 50);
    var url = Uri.parse("http://10.0.2.2:5000/user/farm");
    http.Response response = await http.post(
        url,
        headers: <String, String>{
          'Accept': 'application/json',
          'Content-Type': 'application/json'
        },
        body: jsonEncode(experienceGainer)
    );
    print('Response status: ${response.statusCode}');
  }

  void searchRoutes(String searchString) async{
    var url = Uri.parse("http://10.0.2.2:5000/route/Search/$searchString");
    http.Response response = await http.get(url);
    print(response.body);
    var value = jsonDecode(response.body);
    var elements = value.length;
    setState(() {
      _routes.clear();
      for (var i = 0; i < elements; i++) {
        String routeName = value[i]['name'];
        num length = value[i]['length'];
        List<LatLng> coordinates = [];
        for (var marker in value[i]['coordinates']) {
          LatLng coordinate = LatLng(marker[0], marker[1]);
          coordinates.add(coordinate);
        }
        int type = value[i]['type'];
        a.Route route = a.Route(routeName, length, coordinates, type);
        _routes.add(route);
      }
    });
  }

  void _exitCreationWindow() {
    setState(() {
      createRouteWindow = false;
    });
  }

  void _changeSlidingWindow() {
    _panelController.open();
    setState(() {
      _currentRoute = a.Route(
          "newRoute", 0, [], 0);
      createRouteWindow = true;
    });
  }

  Widget _buildList() {
    return ListView.builder(
        itemCount: _routes.length * 2,
        itemBuilder: (BuildContext _context, int i) {
          if (i.isOdd) {
            return const Divider();
          }
          final int index = i ~/ 2;
          return _buildRow(_routes[index], index);
        });
  }

  Widget _buildRow(a.Route route, int index) {
    return ListTile(
      title: Text(route.name),
      subtitle: Text(
          "Lenght: ${route.length}, number of waypoints: ${route.coordinates.length}"),
      onTap: () => _updateCurrentRoute(route),
    );
  }

  Widget _buildListCreator() {
    return ReorderableListView.builder(
      itemCount: _currentRoute.coordinates.length,
      itemBuilder: (BuildContext _context, int i) {
        final int index = i;
        return _buildRowCreator(_currentRoute.coordinates[index], index);
      },
      onReorder: (int oldIndex, int newIndex) {
        setState(() {
          if (oldIndex < newIndex) {
            newIndex -= 1;
          }
          final LatLng item = _currentRoute.coordinates.removeAt(oldIndex);
          _currentRoute.coordinates.insert(newIndex, item);
        });
      },);
  }

  Future<String?> _geocodeMarker(LatLng marker) async{
    List<Placemark> placemarks = await placemarkFromCoordinates(marker.latitude, marker.longitude);
    var place = placemarks[0];
    return (place.street);
  }


  Widget _buildRowCreator(LatLng marker, int index) {
    return ListTile(
      key: Key('$index'),
      leading: IconButton(
          icon: const Icon(
              Icons.close,
              color: Colors.red
          ),
          onPressed: () {
            setState(() {
              _currentRoute.coordinates.removeAt(index);
            });
          }
      ),
      title: FutureBuilder<String?>(
          future: _geocodeMarker(marker),
          builder: (BuildContext context, AsyncSnapshot<String?> snapshot) {
            if (snapshot.hasData){
              return Text("ID: $index, Marker: " + (snapshot.data ?? "${marker.longitude} ${marker.latitude}"));
            }
            else {
              return Text("ID: $index, Marker: ${marker.longitude} ${marker.latitude}");
            }
          }

      ),
      trailing: const Icon(
        Icons.drag_indicator_rounded,
      ),
      //subtitle: Text("Length: ${route.length}, number of waypoints: ${route.coordinates.length}"),
      //onTap: () => _updateCurrentRoute(route),
    );
  }

  void saveRoute (){
    setState(() {
      sendRoute(_currentRoute);
      _routes.add(_currentRoute);
    });
  }



  Widget _creatingRouteWindow() {

    Future<dynamic> createAlertDialog(BuildContext context){
      TextEditingController controller = TextEditingController();

      return showDialog(context: context, builder: (context) {
        return AlertDialog(
          title: Text("Enter route name"),
          content: TextField(
            controller: controller,
          ),
          actions: <Widget>[
            MaterialButton(
                elevation: 5.0,
                child: Text('Submit'),
                onPressed: () {
                  Navigator.of(context).pop(_currentRoute.name = controller.text.toString());

                }
            )
          ],
        );
      });
    }


    final ButtonStyle style =
    ElevatedButton.styleFrom(textStyle: const TextStyle(fontSize: 20));

    return Column(
        mainAxisAlignment: MainAxisAlignment.center,
        children: <Widget>[
          const SizedBox(
            height: 72,
            child: Padding(
              padding: EdgeInsets.all(7.0),
              child: Text("Create route", style: TextStyle(fontSize: 40)),
            ),
          ),
          SizedBox(height: 120, child:_buildListCreator()),
          ElevatedButton(
            style: style,
            onPressed: () => _getRandomRoute(),
            child: const Text("Generate random route"),
          ),
          ElevatedButton(
            style: style,
            onPressed: () => createAlertDialog(context).then((onValue){
              _currentRoute.name = onValue;
              saveRoute();
            }),
            child: const Text("Save new route"),
          ),
          ElevatedButton(
            style: style,
            onPressed: () => _exitCreationWindow(),
            child: const Text("Exit from route"),
          ),
        ]);
  }


  Widget _browsingWindow() {
    return Column(
      mainAxisSize: MainAxisSize.max,
      mainAxisAlignment: MainAxisAlignment.start,
      crossAxisAlignment: CrossAxisAlignment.start,
      children: [
        const SizedBox(height: 10),
        Padding(
          padding: const EdgeInsets.symmetric(horizontal: 15.0),
          child: TextField(
            onChanged: (value) {
              setState(() {
                searchRoutes(value);
              });
            },
            decoration: const InputDecoration(
              labelText: 'Search',
              suffixIcon: Icon(Icons.search),
            ),
          ),
        ),
        const SizedBox(height: 10),
        Expanded(
          child: FutureBuilder(
            initialData: const SizedBox(
              height: 80,
              child: Padding(
                padding: EdgeInsets.all(1.0),
                child: Text("Browse routes", style: TextStyle(fontSize: 40)),
              ),
            ), builder: (BuildContext context, AsyncSnapshot<SizedBox> snapshot) { return SizedBox(height: 150.0, child: _buildList());  },
          ),
        ),
      ],
    );
  }

  callbackRoute(updatedRoute){
    setState(() {
      _currentRoute = updatedRoute;
    });
  }

  callbackSaveRoute(routeToSave){
    setState(() {
      sendRoute(routeToSave);
      _routes.add(routeToSave);
    });
  }

  Widget buildFloatingSearchBar() {
    final isPortrait = MediaQuery.of(context).orientation == Orientation.portrait;

    void _getLocations(String query) async{
      var url = Uri.parse("https://photon.komoot.io/api/?q=" + query + "&limit=7");
      var response = await http.get(url);
      print(response.body);
      _locations.clear();
      Map<String, dynamic> value = jsonDecode(response.body);
      for (var x in value['features']){
        var coords = x['geometry']['coordinates'];
        LatLng place = LatLng(coords[1], coords[0]);
        String name = x['properties']['name'];
        String country = x['properties']['country'];
        _locations.add(LocationMarker(name, country, place));
      }
    }

    return FloatingSearchBar(
      controller: _floatingSearchBarController,
      hint: 'Search...',
      scrollPadding: const EdgeInsets.only(top: 16, bottom: 56),
      transitionDuration: const Duration(milliseconds: 800),
      transitionCurve: Curves.easeInOut,
      physics: const BouncingScrollPhysics(),
      axisAlignment: isPortrait ? 0.0 : -1.0,
      openAxisAlignment: 0.0,
      width: isPortrait ? 600 : 500,
      debounceDelay: const Duration(milliseconds: 500),
      onQueryChanged: (query) {
        // Call your model, bloc, controller here.
      },
      onSubmitted: (query){
        print(query);
        setState(() {
          _getLocations(query);
        });

      }
      ,
      // Specify a custom transition to be used for
      // animating between opened and closed stated.
      transition: CircularFloatingSearchBarTransition(),
      actions: [
        FloatingSearchBarAction(
          showIfOpened: false,
          child: CircularButton(
            icon: const Icon(Icons.place),
            onPressed: () {},
          ),
        ),
        FloatingSearchBarAction.searchToClear(
          showIfClosed: false,
        ),
      ],
      builder: (context, transition) {
        return ClipRRect(
            borderRadius: BorderRadius.circular(8),
            child: Material(
              color: Colors.white,
              elevation: 4.0,
              child: Column(
                mainAxisSize: MainAxisSize.min,
                children: _locations.map((location) {
                  return InkWell(
                      child: Container(height: 112, child: Text(location.name)),
                      onTap: () => setState(() { centerPosition = location.coordinates; _floatingSearchBarController.close();})


                  );
                }).toList(),
              ),
            ));
      },
    );
  }

  void setUserDetails(){
    setState(() {
      APIService.getUser(widget.tokenMain).then((response) {
        level = response.level;
        exp = response.exp;
        email = response.email;
        username = response.username;
        _EndExp = response.level*125;
        userId = response.id;
    });

    });
  }

  @override
  Widget build(BuildContext context) {
    BorderRadiusGeometry roundingRadius = const BorderRadius.only(
      topLeft: Radius.circular(24.0),
      topRight: Radius.circular(24.0),
    );

    final ButtonStyle style =
    ElevatedButton.styleFrom(fixedSize: const Size(0, 10), textStyle: const TextStyle(fontSize: 20));

    return Scaffold(
      appBar: AppBar(
          title: Text("User Page"),
          actions: <Widget>[
            IconButton(
                onPressed: (){
                  APIService.logout().then((response) {
                    if(response.message == "Success"){
                      SharedService.logout(context);
                    } else{
                      FormHelper.showSimpleAlertDialog(
                        context,
                        Config.appName,
                        response.message,
                        "OK",
                            (){
                          Navigator.pop(context);
                        },
                      );
                    }
                  });
                },
                icon: const Icon(
                  Icons.logout,
                  color: Colors.black,
                ))
          ]
      ),
      drawer: Drawer(
        // Add a ListView to the drawer. This ensures the user can scroll
        // through the options in the drawer if there isn't enough vertical
        // space to fit everything.
        child: ListView(
          // Important: Remove any padding from the ListView.
          padding: EdgeInsets.zero,
          children: [
            DrawerHeader(
                decoration: BoxDecoration(
                  color: Colors.blue,
                ),
                child: Center(
                  child: Text('$username', style: TextStyle(fontStyle: FontStyle.italic, color: Colors.white, fontSize: 30)),
                )
            ),
            LinearProgressIndicator(
              //value: controller.progressPercent,
              backgroundColor: Colors.grey[200],
            ),
            Container(
              child: Center(
                child: Text('Level $level', style: TextStyle(fontStyle: FontStyle.italic, color: Colors.black, fontSize: 30)),
              ),
              padding: const EdgeInsets.all(8),
            ),
            LinearProgressIndicator(
              //value: controller.progressPercent,
              backgroundColor: Colors.grey[200],
            ),
            Container(
              child: Center(
                child: Text('$exp/$_EndExp exp', style: Theme.of(context).textTheme.headline6,),
              ),
              padding: const EdgeInsets.all(8),
            ),
            ElevatedButton(
              style: style,
              onPressed: () { sendExp(); setUserDetails ;},
              child: const Text("Get experience"),
            ),
          ],
        ),
      ),
        body: SlidingUpPanel(
          borderRadius: roundingRadius,
          controller: _panelController,
          maxHeight: 350.0,
          collapsed: Container(
            decoration: BoxDecoration(borderRadius: roundingRadius),
          ),
          panel: Center(

              child: (!createRouteWindow)
                  ? _browsingWindow()
                  : _creatingRouteWindow()),
          body: Stack(
              fit: StackFit.expand,
              children: [
                OSMMap(
                  currentRoute: _currentRoute, creationMode: createRouteWindow, callbackRoute: callbackRoute, callbackSaveRoute: callbackSaveRoute, centerPosition: centerPosition,
                ),
                buildFloatingSearchBar(),
              ]
          ),
      ),
      floatingActionButton: (!createRouteWindow)
          ? FloatingActionButton(
        onPressed: () => _changeSlidingWindow(),
        tooltip: 'Increment',
        child: const Icon(Icons.add),
      )
          : null,
    );
  }
}
