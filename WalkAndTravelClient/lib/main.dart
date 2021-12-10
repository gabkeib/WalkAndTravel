import 'dart:convert';

import 'package:geocoding/geocoding.dart';
import 'package:geojson/geojson.dart';
import 'package:http/http.dart' as http;
import 'package:flutter/material.dart';

import 'package:latlong2/latlong.dart';
import 'package:material_floating_search_bar/material_floating_search_bar.dart';
import 'package:sliding_up_panel/sliding_up_panel.dart';
import 'package:walk_and_travel/widgets/osmmap.dart';
import 'models/route.dart' as a;
import 'models/location_marker.dart';
import 'models/minimal_route.dart' as b;

void main() {
  runApp(const MyApp());
}

class MyApp extends StatelessWidget {
  const MyApp({Key? key}) : super(key: key);

  // This widget is the root of your application.
  @override
  Widget build(BuildContext context) {
    return MaterialApp(
      title: 'Flutter Demo',
      theme: ThemeData(
        // This is the theme of your application.
        //
        // Try running your application with "flutter run". You'll see the
        // application has a blue toolbar. Then, without quitting the app, try
        // changing the primarySwatch below to Colors.green and then invoke
        // "hot reload" (press "r" in the console where you ran "flutter run",
        // or simply save your changes to "hot reload" in a Flutter IDE).
        // Notice that the counter didn't reset back to zero; the application
        // is not restarted.
        primarySwatch: Colors.blue,
      ),
      home: const MyHomePage(title: 'Flutter Demo Home Page'),
    );
  }
}

class MyHomePage extends StatefulWidget {
  const MyHomePage({Key? key, required this.title}) : super(key: key);

  // This widget is the home page of your application. It is stateful, meaning
  // that it has a State object (defined below) that contains fields that affect
  // how it looks.

  // This class is the configuration for the state. It holds the values (in this
  // case the title) provided by the parent (in this case the App widget) and
  // used by the build method of the State. Fields in a Widget subclass are
  // always marked "final".

  final String title;

  @override
  State<MyHomePage> createState() => _MyHomePageState();
  //State<MyHomePage> createState() => _OSMMap();
}

class _MyHomePageState extends State<MyHomePage> {
  final List<LocationMarker> _locations = [];
  final List<a.Route> _routes = [];
  a.Route _currentRoute = a.Route(
      "newRoute", 2.3, [LatLng(54.6866, 25.2865), LatLng(54.6902, 25.2764)], 0);
  final PanelController _panelController = PanelController();
  bool createRouteWindow = false;
  LatLng centerPosition = LatLng(54.68585, 25.28647);

  final FloatingSearchBarController _floatingSearchBarController = FloatingSearchBarController();

  @override
  void initState() {
    super.initState();
    getRoutes();
  }

  void _incrementCounter() {
    setState(() {
      // This call to setState tells the Flutter framework that something has
      // changed in this State, which causes it to rerun the build method below
      // so that the display can reflect the updated values. If we changed
      // _counter without calling setState(), then the build method would not be
      // called again, and so nothing would appear to happen.

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
    var url = Uri.parse("http://10.0.2.2:5000/route/route");
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
      b.MinimalRoute newRoute = b.MinimalRoute(route.name, convCoordinates);
      var url = Uri.parse("http://10.0.2.2:5000/route/SaveNewRoute");
      http.Response response = await http.post(url,
      headers: <String, String>{
          'Accept': 'application/json',
          'Content-Type': 'application/json'
          },
          body: jsonEncode(newRoute)
      );
      print('Response status: ${response.statusCode}');
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
      // Column is also a layout widget. It takes a list of children and
      // arranges them vertically. By default, it sizes itself to fit its
      // children horizontally, and tries to be as tall as its parent.
      //
      // Invoke "debug painting" (press "p" in the console, choose the
      // "Toggle Debug Paint" action from the Flutter Inspector in Android
      // Studio, or the "Toggle Debug Paint" command in Visual Studio Code)
      // to see the wireframe for each widget.
      //
      // Column has various properties to control how it sizes itself and
      // how it positions its children. Here we use mainAxisAlignment to
      // center the children vertically; the main axis here is the vertical
      // axis because Columns are vertical (the cross axis would be
      // horizontal).
      mainAxisAlignment: MainAxisAlignment.center,
      children: <Widget>[
        const SizedBox(
          height: 80,
          child: Padding(
            padding: EdgeInsets.all(1.0),
            child: Text("Browse routes", style: TextStyle(fontSize: 40)),
          ),
        ),
        //Expanded(
        //child:
        SizedBox(height: 150.0, child: _buildList()),
        // ),
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

  @override
  Widget build(BuildContext context) {
    BorderRadiusGeometry roundingRadius = const BorderRadius.only(
      topLeft: Radius.circular(24.0),
      topRight: Radius.circular(24.0),
    );
    // This method is rerun every time setState is called, for instance as done
    // by the _incrementCounter method above.
    //
    // The Flutter framework has been optimized to make rerunning build methods
    // fast, so that you can just rebuild anything that needs updating rather
    // than having to individually change instances of widgets.
    return Scaffold(
      appBar: AppBar(
        // Here we take the value from the MyHomePage object that was created by
        // the App.build method, and use it to set our appbar title.
        title: Text(widget.title),
      ),
      body: SlidingUpPanel(
        borderRadius: roundingRadius,
        controller: _panelController,
        maxHeight: 350.0,
        collapsed: Container(
          decoration: BoxDecoration(borderRadius: roundingRadius),
        ),
        panel: Center(
            // Center is a layout widget. It takes a single child and positions it
            // in the middle of the parent.
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

        // This trailing comma makes auto-formatting nicer for build methods.
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
