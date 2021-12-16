

class MinimalRoute {
  String name;
  List<double> route;

  Map<String, dynamic> toJson() {
    return {
      'name': name,
      'route': route,
    };
  }
  MinimalRoute(this.name, this.route);
}