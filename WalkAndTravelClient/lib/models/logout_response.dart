import 'dart:convert';

LogoutResponse logoutResponse (String str) =>
    LogoutResponse.fromJson(json.decode(str));

class LogoutResponse {
  LogoutResponse({
    required this.message,
  });
  late final String message;

  LogoutResponse.fromJson(Map<String, dynamic> json){
    message = json['message'];
  }

  Map<String, dynamic> toJson() {
    final _data = <String, dynamic>{};
    _data['message'] = message;
    return _data;
  }
}