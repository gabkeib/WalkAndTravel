class RegisterRequest {
  RegisterRequest({
    required this.username,
    required this.password,
    required this.email,
    required this.name,
    required this.surname,
    required this.age,
  });
  late final String username;
  late final String password;
  late final String email;
  late final String name;
  late final String surname;
  late final String age;

  RegisterRequest.fromJson(Map<String, dynamic> json){
    username = json['username'];
    password = json['password'];
    email = json['email'];
    name = json['name'];
    surname = json['surname'];
    age = json['age'];
  }

  Map<String, dynamic> toJson() {
    final _data = <String, dynamic>{};
    _data['username'] = username;
    _data['password'] = password;
    _data['email'] = email;
    _data['name'] = name;
    _data['surname'] = surname;
    _data['age'] = age;
    return _data;
  }
}