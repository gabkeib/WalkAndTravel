import 'package:flutter/cupertino.dart';
import 'package:flutter/gestures.dart';
import 'package:flutter/material.dart';
import 'package:snippet_coder_utils/FormHelper.dart';
import 'package:snippet_coder_utils/ProgressHUD.dart';

class RegisterPage extends StatefulWidget {
  const RegisterPage({Key? key}) : super(key: key);

  @override
  _RegisterPageState createState() => _RegisterPageState();
}

class _RegisterPageState extends State<RegisterPage> {
  bool isAPICallProcess = false;
  bool hidePassword = true;
  GlobalKey<FormState> globalFormKey = GlobalKey<FormState>();
  String? email;
  String? password;

  @override
  Widget build(BuildContext context) {
    return SafeArea(
      child: Scaffold(
        backgroundColor: Colors.indigoAccent,
        body: ProgressHUD(
          child: Form(
            key: globalFormKey,
            child: _registerUI(context),
          ),
          inAsyncCall: isAPICallProcess,
          opacity: 0.3,
          key: UniqueKey(),
        ),
      ),
    );
  }

  Widget _registerUI(BuildContext context){
    return SingleChildScrollView(
        child: Column(
          mainAxisAlignment: MainAxisAlignment.start,
          crossAxisAlignment: CrossAxisAlignment.start,
          children: [
            Container(
                width: MediaQuery.of(context).size.width,
                height: MediaQuery.of(context).size.height/6,
                decoration: const BoxDecoration(
                  gradient: LinearGradient(
                    begin: Alignment.topCenter,
                    end: Alignment.bottomCenter,
                    colors: [
                      Colors.white,
                      Colors.greenAccent
                    ],
                  ),
                  borderRadius: BorderRadius.only(
                      bottomLeft: Radius.circular(100),
                      bottomRight: Radius.circular(100)
                  ),
                ),
                child: Column(
                    mainAxisAlignment: MainAxisAlignment.center,
                    children: [
                      Align(
                        alignment: Alignment.center,
                        child: Image.asset(
                            "assets/images/WATLogo.png",
                            width: MediaQuery.of(context).size.width,
                            height: MediaQuery.of(context).size.height/6,
                            fit: BoxFit.contain
                        ),
                      )
                    ]
                )
            ),
            const Padding(
              padding: EdgeInsets.only(
                  left: 20,
                  bottom: 30,
                  top: 20
              ),
              child: Text(
                "Register",
                style:TextStyle(
                    fontWeight: FontWeight.bold,
                    fontSize: 25,
                    color: Colors.white
                ),
              ),
            ),
            FormHelper.inputFieldWidget(
              context,
              const Icon(Icons.person),
              "email",
              "Email",
                  (onValidateVal){
                if(onValidateVal.isEmpty){
                  return "Email can't be empty.";
                }
                return null;
              },
                  (onSavedVal){
                email = onSavedVal;
              },
              borderFocusColor: Colors.white,
              prefixIconColor: Colors.white,
              borderColor: Colors.white,
              textColor: Colors.white,
              hintColor: Colors.white.withOpacity(0.7),
              borderRadius: 10,
            ),
            Padding(
              padding: const EdgeInsets.only(top: 10),
              child: FormHelper.inputFieldWidget(
                context,
                const Icon(Icons.person),
                "password",
                "Password",
                    (onValidateVal){
                  if(onValidateVal.isEmpty){
                    return "Password can't be empty.";
                  }
                  return null;
                },
                    (onSavedVal){
                  password = onSavedVal;
                },
                borderFocusColor: Colors.white,
                prefixIconColor: Colors.white,
                borderColor: Colors.white,
                textColor: Colors.white,
                hintColor: Colors.white.withOpacity(0.7),
                borderRadius: 10,
                obscureText: hidePassword,
                suffixIcon: IconButton(
                  onPressed: (){
                    setState(() {
                      hidePassword = !hidePassword;
                    });
                  },
                  color: Colors.white.withOpacity(0.7),
                  icon: Icon(
                      hidePassword ?  Icons.visibility_off : Icons.visibility
                  ),
                ),
              ),
            ),
            const SizedBox(
              height: 20,
            ),
            Center(
                child: FormHelper.submitButton(
                  "Register",
                      (){},
                  btnColor: Colors.blueAccent,
                  borderColor: Colors.white,
                  txtColor: Colors.white,
                  borderRadius: 10,
                )
            ),
            const SizedBox(height: 20,),

            Align(
              alignment: Alignment.center,
              child: Padding(
                  padding: const EdgeInsets.only(right:25, top: 10),
                  child: RichText(
                      text: TextSpan(
                          style: const TextStyle(
                              color: Colors.grey,
                              fontSize: 14
                          ),
                          children: <TextSpan>[
                            const TextSpan(text: "Already have an account?  "),
                            TextSpan(
                              text: 'Log in',
                              style: const TextStyle(
                                color: Colors.white,
                                decoration: TextDecoration.underline,
                              ),
                              recognizer: TapGestureRecognizer()..onTap = (){
                                Navigator.pushNamed(context, "/login");
                              },
                            ),
                          ]
                      )
                  )
              ),
            )

          ],

        )
    );
  }
}
