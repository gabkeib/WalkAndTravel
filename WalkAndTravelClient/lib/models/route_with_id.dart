class RouteWithId {
  String name;
  int authorId;
  List<double> coords;

  Map<String, dynamic> toJson() {
    return {
      'name': name,
      'authorId' : authorId,
      'coords': coords,
    };
  }
  RouteWithId(this.name, this.authorId, this.coords);
}