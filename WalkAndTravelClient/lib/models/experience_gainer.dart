class ExperienceGainer {
  String email;
  int exp;

  Map<String, dynamic> toJson() {
    return {
      'email': email,
      'exp': exp,
    };
  }
  ExperienceGainer(this.email, this.exp);
}