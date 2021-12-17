import 'dart:convert';

import 'package:geocoding/geocoding.dart';
import 'package:geojson/geojson.dart';
import 'package:http/http.dart' as http;
import 'package:flutter/material.dart';

import 'package:latlong2/latlong.dart';
import 'package:material_floating_search_bar/material_floating_search_bar.dart';
import 'package:sliding_up_panel/sliding_up_panel.dart';
import 'package:walk_and_travel/pages/login_page.dart';
import 'package:walk_and_travel/pages/my_home_page.dart';
import 'package:walk_and_travel/pages/my_home_page2.dart';
import 'package:walk_and_travel/pages/registration_page.dart';
import 'package:walk_and_travel/pages/user_page.dart';

import 'package:walk_and_travel/services/shared_service.dart';
import 'package:walk_and_travel/widgets/osmmap.dart';
import 'models/route.dart' as a;
import 'models/location_marker.dart';
import 'models/minimal_route.dart' as b;
String tokenFromBack = "";

Widget _defaultHome = const MyHomePage(title: 'Walk And Travel');
void main() async{
  WidgetsFlutterBinding.ensureInitialized();
  /*bool _result = await SharedService.isLoggedIn();
  if(_result){
    _defaultHome = UserPage(tokenMain: tokenFromBack);
  }else{
    _defaultHome = const MyHomePage(title: 'Walk And Travel');
  }*/
  runApp(const MyApp());
}

class MyApp extends StatelessWidget {
  const MyApp({Key? key}) : super(key: key);

  callbackToken(token){
    tokenFromBack = token;
    }
  // This widget is the root of your application.
  @override
  Widget build(BuildContext context) {
    return MaterialApp(
      title: 'Walk And Travel',
      theme: ThemeData(
        // This is the theme of your application.
        primarySwatch: Colors.blue,
      ),
      routes: {
        '/': (context) => _defaultHome,
        '/login' : (context) => LoginPage(callbackToken:callbackToken),
        '/register' : (context) => const RegisterPage(),
        '/user' : (context) => MyHomePage2(tokenMain: tokenFromBack),
      },
    );
  }
}
