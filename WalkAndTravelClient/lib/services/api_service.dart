import 'dart:convert';

import 'package:http/http.dart' as http;
import 'package:walk_and_travel/config.dart';
import 'package:walk_and_travel/models/farm_request.dart';
import 'package:walk_and_travel/models/login_request.dart';
import 'package:walk_and_travel/models/login_response.dart';
import 'package:walk_and_travel/models/register_request.dart';
import 'package:walk_and_travel/models/register_response.dart';
import 'package:walk_and_travel/models/logout_response.dart';
import 'package:walk_and_travel/models/user_response.dart';
import 'package:walk_and_travel/services/shared_service.dart';

class APIService {
  static var client = http.Client();

  static Future<LoginResponse> login(LoginRequest model) async{
    Map<String, String> requestHeaders = {
      'Content-Type': 'application/json',
    };

    var url = Uri.http(Config.apiURL, Config.loginAPI);

    var response = await client.post(
      url,
      headers: requestHeaders,
      body: jsonEncode(model.toJson()),
    );

    if(response.statusCode == 200){
      await SharedService.setLoginDetails(loginResponse((response.body)));
      print(response.body);
      return loginResponse(response.body);
    } else{
      return loginResponse(response.body);
    }
  }

  static Future<RegisterResponse> register(RegisterRequest model) async{
    Map<String, String> requestHeaders = {
      'Content-Type': 'application/json',
    };

    var url = Uri.http(Config.apiURL, Config.registerAPI);

    var response = await client.post(
      url,
      headers: requestHeaders,
      body: jsonEncode(model.toJson()),
    );
    return registerResponse(response.body);
  }

  static Future<UserResponse> getUser(String token) async{
    var loginDetails = await SharedService.loginDetails();

    Map<String, String> requestHeaders = {
      'Content-Type': 'application/json',
      //'Authorization': 'Basic ${loginDetails!.item1}'
    };

    var url = Uri.parse("http://10.0.2.2:5000/user/user/$token");

    http.Response response = await http.get(url);

      return userResponse(response.body);
  }

  static Future<bool> gainExp(FarmRequest model) async{
    Map<String, String> requestHeaders = {
      'Content-Type': 'application/json',
    };

    var url = Uri.http(Config.apiURL, Config.expAPI);

    var response = await client.post(
      url,
      headers: requestHeaders,
      body: jsonEncode(model.toJson()),
    );

    if(response.statusCode == 200){
      print(response.body);
      return true;
    } else{
      print(response.body);
      return false;
    }
  }

  static Future<LogoutResponse> logout() async{
    //var loginDetails = await SharedService.loginDetails();

    Map<String, String> requestHeaders = {
      'Content-Type': 'application/json',
    };

    var url = Uri.http(Config.apiURL, Config.logoutAPI);

    var response = await client.post(
      url,
      headers: requestHeaders,
    );

    return logoutResponse(response.body);
  }
}