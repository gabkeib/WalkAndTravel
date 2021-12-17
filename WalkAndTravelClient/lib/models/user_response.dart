import 'dart:convert';

UserResponse userResponse (String str) =>
    UserResponse.fromJson(json.decode(str));

class UserResponse {
  UserResponse({
    required this.id,
    required this.name,
    required this.surname,
    required this.username,
    required this.email,
    required this.level,
    required this.exp,
    required this.age,
  });
  late final int id;
  late final String name;
  late final String surname;
  late final String username;
  late final String email;
  late final int level;
  late final int exp;
  late final int age;

  UserResponse.fromJson(Map<String, dynamic> json){
    id = json['id'];
    name = json['name'];
    surname = json['surname'];
    username = json['username'];
    email = json['email'];
    level = json['level'];
    exp = json['exp'];
    age = json['age'];
  }

  Map<String, dynamic> toJson() {
    final _data = <String, dynamic>{};
    _data['id'] = id;
    _data['name'] = name;
    _data['surname'] = surname;
    _data['username'] = username;
    _data['email'] = email;
    _data['level'] = level;
    _data['exp'] = exp;
    _data['age'] = age;
    return _data;
  }
}