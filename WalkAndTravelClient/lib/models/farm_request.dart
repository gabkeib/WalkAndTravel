class FarmRequest {
  FarmRequest({
    required this.email,
    required this.exp,
  });
  late final String email;
  late final String exp;

  FarmRequest.fromJson(Map<String, dynamic> json){
    email = json['email'];
    exp = json['exp'];
  }

  Map<String, dynamic> toJson() {
    final _data = <String, dynamic>{};
    _data['email'] = email;
    _data['exp'] = exp;
    return _data;
  }
}