import 'dart:convert';

import 'package:http/http.dart' as http;
import 'package:flutter/material.dart';

import 'package:latlong2/latlong.dart';
import 'package:sliding_up_panel/sliding_up_panel.dart';
import 'package:walk_and_travel/pages/login_page.dart';
import 'package:walk_and_travel/pages/registration_page.dart';
import 'package:walk_and_travel/widgets/osmmap.dart';
import 'models/route.dart' as a;
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
        primarySwatch: Colors.blue,
      ),
      routes: {
        '/login' : (context) => const LoginPage(),
        '/register' : (context) => const RegisterPage(),
      },
      home: const MyHomePage(title: 'Flutter Demo Home Page'),
    );
  }
}

class MyHomePage extends StatefulWidget {
  const MyHomePage({Key? key, required this.title}) : super(key: key);


  final String title;

  @override
  State<MyHomePage> createState() => _MyHomePageState();
  //State<MyHomePage> createState() => _OSMMap();
}

class _MyHomePageState extends State<MyHomePage> {
  final List<a.Route> _routes = [];
  a.Route _currentRoute = a.Route(
      "newRoute", 2.3, [LatLng(54.6866, 25.2865), LatLng(54.6902, 25.2764)], 0);
  final PanelController _panelController = PanelController();
  bool createRouteWindow = false;

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
          return _buildRow(_routes[index]);
        });
  }

  Widget _buildRow(a.Route route) {
    return ListTile(
      title: Text(route.name),
      subtitle: Text(
          "Lenght: ${route.length}, number of waypoints: ${route.coordinates.length}"),
      onTap: () => _updateCurrentRoute(route),
    );
  }

  Widget _buildListCreator() {
    return ListView.builder(
        itemCount: _currentRoute.coordinates.length * 2,
        itemBuilder: (BuildContext _context, int i) {
          if (i.isOdd) {
            return const Divider();
          }
          final int index = i ~/ 2;
          return _buildRowCreator(_currentRoute.coordinates[index], index);
        });
  }

  Widget _buildRowCreator(LatLng marker, int index) {
    return ListTile(
      title: Text("ID: $index, Marker: ${marker.latitude} ${marker.longitude}"),
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
        body: Center(
            child: OSMMap(
                currentRoute: _currentRoute, creationMode: createRouteWindow, callbackRoute: callbackRoute, callbackSaveRoute: callbackSaveRoute)),
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
