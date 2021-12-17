/*import 'package:flutter/cupertino.dart';
import 'package:flutter/material.dart';
import 'package:snippet_coder_utils/FormHelper.dart';
import 'package:walk_and_travel/models/logout_response.dart';
import 'package:walk_and_travel/services/api_service.dart';
import 'package:walk_and_travel/services/shared_service.dart';

import '../config.dart';

class UserPage extends StatefulWidget {
  const UserPage({Key? key, required this.tokenMain}) : super(key: key);
  final String tokenMain;
  @override
  _UserPageState createState() => _UserPageState();
}

class _UserPageState extends State<UserPage> {
  @override
  Widget build(BuildContext context) {
    return Scaffold(
      appBar: AppBar(
        title: Text("Logged In"),
        elevation: 0,
        actions:[
          IconButton(
            onPressed: (){
              APIService.logout().then((response) {
                if(response.message == "Success"){
                  SharedService.logout(context);
                } else{
                  FormHelper.showSimpleAlertDialog(
                    context,
                    Config.appName,
                    response.message,
                    "OK",
                        (){
                      Navigator.pop(context);
                    },
                  );
                }
              });
            },
            icon: const Icon(
              Icons.logout,
              color: Colors.black,
            ))
        ]
      ),
      backgroundColor: Colors.grey[200],
      body: userProfile(),
    );
  }
  Widget userProfile(){

    return FutureBuilder(
      future: APIService.getUser(widget.tokenMain),
      builder: (BuildContext context, AsyncSnapshot <String> model){
        if(model.hasData){
          return Center(
            child: Text(model.data!),
          );
        }
        return const Center(
          child: CircularProgressIndicator(),
        );
      }
    );
  }
}*/
