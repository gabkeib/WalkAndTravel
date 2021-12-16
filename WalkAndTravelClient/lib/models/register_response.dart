import 'dart:convert';

RegisterResponse registerResponse (String str) =>
    RegisterResponse.fromJson(json.decode(str));

class RegisterResponse {
  RegisterResponse({
    required this.message,
  });
  late final String message;

  RegisterResponse.fromJson(Map<String, dynamic> json){
    message = json['message'];
  }

  Map<String, dynamic> toJson() {
    final _data = <String, dynamic>{};
    _data['message'] = message;
    return _data;
  }
}