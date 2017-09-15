#region MyTry at Brackeys Code

////This script controls the UI in the Database Control (Free) demo scene
////It uses database control to login, register and send and recieve data

//using UnityEngine;
//using System; //allows string.Split to be used with SplitStringOptions.none
//using System.Collections;
//using UnityEngine.UI; //Added from Appeals
//using DatabaseControl;//This line is always needed for any C# script using the database control requests. See PDF documentation for more information
////use 'import DatabaseControl;' if you are using JS

//public class LoginMenu : MonoBehaviour {
//	////These variables are set in the Inspector:
	
//	//they are enabled and disabled to show and hide the different parts of the UI
//	public GameObject login_object;
//	public GameObject register_object;
//	public GameObject loading_object;
	
//	//these are the login input fields:
//	public UnityEngine.UI.InputField input_login_username;
//	public UnityEngine.UI.InputField input_login_password;
	
//	//these are the register input fields:
//	public UnityEngine.UI.InputField input_register_username;
//	public UnityEngine.UI.InputField input_register_password;
//	public UnityEngine.UI.InputField input_register_confirmPassword;
	
//	//red error UI Texts:
//	public UnityEngine.UI.Text login_error;
//	public UnityEngine.UI.Text register_error;
	
//	////These variables cannot be set in the Inspector:
	
//	//the part of UI currently being shown
//	// 0 = login, 1 = register, 2 = logged in, 3 = loading
//	int part = 0;
//	//scene starts showing login
	
//	bool isDatabaseSetup = true;

//    #region Appeals Adjst
//    //Called at the very start of the game
//    void Awake()
//    {
//        ResetAllUIElements();
//    }

//    //Called by Button Pressed Methods to Reset UI Fields
//    void ResetAllUIElements()
//    {
//        //This resets all of the UI elements. It clears all the strings in the input fields and any errors being displayed
//        input_login_username.text = "";
//        input_login_password.text = "";
//        input_register_username.text = "";
//        input_register_password.text = "";
//        input_register_confirmPassword.text = "";
//        blankErrors();
//    }
//#endregion

//    void Start () {

//        //this checks whether the database is setup.It is used to prevent errors for users who try to use the demos
//        //without having setup a database.
//        //You don't need to use this bool as it will work without it as long as the database has been setup
//        TextAsset datafile = Resources.Load("DCF_RuntimeData") as TextAsset;
//            string[] splitdatafile = datafile.text.Split(new string[] { "-" }, StringSplitOptions.None);
//            if (splitdatafile[0] == "0")
//            {
//                isDatabaseSetup = false;
//                Debug.Log("These demos will not work out of the box. You need to setup a database first for it to work. Please read the Setup section of the PDF for more information");
//            }
//            else
//            {
//                isDatabaseSetup = true;
//            }

//            //sets error Texts string to blank
//            blankErrors();
//	}

//	void Update () {

//		if (isDatabaseSetup == true) {

//			//enables and disables the defferent objects to show correct part
//			if (part == 0) {
//				login_object.gameObject.SetActive (true);
//				register_object.gameObject.SetActive (false);
//				loading_object.gameObject.SetActive (false);
//			}
//			if (part == 1) {
//				login_object.gameObject.SetActive (false);
//				register_object.gameObject.SetActive (true);
//				loading_object.gameObject.SetActive (false);
//			}
//			if (part == 2) {
//                // Logged in - already transitioned to a new scene
//			}
//			if (part == 3) {
//				login_object.gameObject.SetActive (false);
//				register_object.gameObject.SetActive (false);
//				loading_object.gameObject.SetActive (true);
//			}

//		}
		
//	}

//	void blankErrors () {
//		//blanks all error texts when part is changed e.g. login > Register
//		login_error.text = "";
//		register_error.text = "";
//	}
	
//	public void login_Register_Button () { //called when the 'Register' button on the login part is pressed
//		part = 1; //show register UI
//		blankErrors();
//	}
	
//	public void register_Back_Button () { //called when the 'Back' button on the register part is pressed
//		part = 0; //goes back to showing login UI
//		blankErrors();
//	}
	
//	public void data_LogOut_Button () { //called when the 'Log Out' button on the data part is pressed
//		part = 0; //goes back to showing login UI
//        UserAccountManager.instance.LogOut();
//        blankErrors();
//	}

//	public void login_login_Button ()
//    { //called when the 'Login' button on the login part is pressed

//		if (isDatabaseSetup == true)
//        {
		
//			//check fields aren't blank
//			if ((input_login_username.text != "") && (input_login_password.text != ""))
//            {
			
//				//check fields don't contain '-' (if they do, login request will return with error and take longer)
//				if ((input_login_username.text.Contains ("-")) || (input_login_password.text.Contains ("-")))
//                {
//					//string contains "-" so return error
//					login_error.text = "Unsupported Symbol '-'";
//					input_login_password.text = ""; //blank password field
//				} else
//                {
//					//ready to send request
//					StartCoroutine (sendLoginRequest (input_login_username.text, input_login_password.text)); //calls function to send login request
//					part = 3; //show 'loading...'
//				}
			
//			} else
//            {
//				//one of the fields is blank so return error
//				login_error.text = "Field Blank!";
//				input_login_password.text = ""; //blank password field
//			}
		
//		}
		
//	}
	
//	IEnumerator sendLoginRequest (string username, string password)
//    {

//		//if (isDatabaseSetup == true)
//        //{
//			IEnumerator e = DCF.Login (username, password);
//			while (e.MoveNext())
//            {
//                Debug.Log("Yielded: " + e.Current);
//                yield return e.Current;
//			}
//        //WWW returned = e.Current as WWW;
//        string returned = e.Current as string;
//        //if (returned.text == "Success")
//            if (returned == "Success")
//            {
//            //Password was correct
//            //blankErrors ();
//                ResetAllUIElements();
//				part = 2; //show logged in UI
			
//				//blank username field
//				//AT input_login_username.text = ""; //password field is blanked at the end of this function, even when error is returned

//                UserAccountManager.instance.LogIn(username, password);
//			}
//        //if (returned.text == "incorrectUser")
//        if (returned == "incorrectUser")
//        {
//				//Account with username not found in database
//				login_error.text = "Username not found";
//				part = 0; //back to login UI
//			}
//        //AT if (returned.text == "incorrectPass") {
//            if (returned == "incorrectPass")
//            {
//            //Account with username found, but password incorrect
//            part = 0; //back to login UI
//				login_error.text = "Incorrect Password";
//			}
//        //AT if (returned.text == "ContainsUnsupportedSymbol") {
//        //One of the parameters contained a - symbol
//            if (returned == "ContainsUnsupportedSymbol")
//            {
//            part = 0; //back to login UI
//				login_error.text = "Unsupported Symbol '-'";
//			}

//        //AT if (returned.text == "Error") {
//            if (returned == "Error")
//            {
//            //Account Not Created, another error occurred
//            part = 0; //back to login UI
//			login_error.text = "Database Error. Try again later.";
//			}
		
//			//blank password field
//			input_login_password.text = "";

//		//}
//	}

//	public void register_register_Button () { //called when the 'Register' button on the register part is pressed

//		if (isDatabaseSetup == true) {
		
//			//check fields aren't blank
//			if ((input_register_username.text != "") && (input_register_password.text != "") && (input_register_confirmPassword.text != "")) {
			
//				//check username is longer than 4 characters
//				if (input_register_username.text.Length > 4) {
				
//					//check password is longer than 6 characters
//					if (input_register_password.text.Length > 6) {
					
//						//check passwords are the same
//						if (input_register_password.text == input_register_confirmPassword.text) {
						
//							if ((input_register_username.text.Contains ("-")) || (input_register_password.text.Contains ("-")) || (input_register_confirmPassword.text.Contains ("-"))) {
							
//								//string contains "-" so return error
//								register_error.text = "Unsupported Symbol '-'";
//								input_login_password.text = ""; //blank password field
//								input_register_confirmPassword.text = "";
							
//							} else {
							
//								//ready to send request
//								StartCoroutine (sendRegisterRequest (input_register_username.text, input_register_password.text, "Hello World!")); //calls function to send register request
//								part = 3; //show 'loading...'
//							}
						
//						} else {
//							//return passwords don't match error
//							register_error.text = "Passwords don't match!";
//							input_register_password.text = ""; //blank password fields
//							input_register_confirmPassword.text = "";
//						}
					
//					} else {
//						//return password too short error
//						register_error.text = "Password too Short";
//						input_register_password.text = ""; //blank password fields
//						input_register_confirmPassword.text = "";
//					}
				
//				} else {
//					//return username too short error
//					register_error.text = "Username too Short";
//					input_register_password.text = ""; //blank password fields
//					input_register_confirmPassword.text = "";
//				}
			
//			} else {
//				//one of the fields is blank so return error
//				register_error.text = "Field Blank!";
//				input_register_password.text = ""; //blank password fields
//				input_register_confirmPassword.text = "";
//			}

//		}
		
//	}
	
//	IEnumerator sendRegisterRequest (string username, string password, string data) {

//		if (isDatabaseSetup == true) {
		
//			IEnumerator ee = DCF.RegisterUser(username, password, data);
//			while(ee.MoveNext()) {
//				yield return ee.Current;
//			}
//			WWW returnedd = ee.Current as WWW;
			
//			if (returnedd.text == "Success") {
//				//Account created successfully
				
//				blankErrors();
//				part = 2; //show logged in UI
				
//				//blank username field
//				input_register_username.text = ""; //password field is blanked at the end of this function, even when error is returned

//                UserAccountManager.instance.LogIn(username, password);
//			}
//			if (returnedd.text == "usernameInUse") {
//				//Account Not Created due to username being used on another Account
//				part = 1;
//				register_error.text = "Username Unavailable. Try another.";
//			}
//			if (returnedd.text == "ContainsUnsupportedSymbol") {
//				//Account Not Created as one of the parameters contained a - symbol
//				part = 1;
//				register_error.text = "Unsupported Symbol '-'";
//			}
//			if (returnedd.text == "Error") {
//				//Account Not Created, another error occurred
//				part = 1;
//				login_error.text = "Database Error. Try again later.";
//			}
			
//			input_register_password.text = "";
//			input_register_confirmPassword.text = "";

//		}
//	}

	
	
	
//}
#endregion



// Appeals Code

//===========================
//Class: Login Menu
//===========================

using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using DatabaseControl; // << Remember to add this reference to your scripts which use DatabaseControl

public class LoginMenu : MonoBehaviour
{
    //All public variables bellow are assigned in the Inspector

    //These are the GameObjects which are parents of groups of UI elements. The objects are enabled and disabled to show and hide the UI elements.
    public GameObject loginParent;
    public GameObject registerParent;
    public GameObject loadingParent;
    private GameObject activeParent;

    //These are all the InputFields which we need in order to get the entered usernames, passwords, etc
    public InputField Login_UsernameField;
    public InputField Login_PasswordField;
    public InputField Register_UsernameField;
    public InputField Register_PasswordField;
    public InputField Register_ConfirmPasswordField;
    public Button btnRegister_Register;
    public Button btnRegister_Back;
    public Button btnLogin_Login; 

    //These are the UI Texts which display errors
    public Text Login_ErrorText;
    public Text Register_ErrorText;

    //Called at the very start of the game
    void Awake()
    {
        ResetAllUIElements();
    }

    private void Start()
    {
        ActivateLoginInputBox();
        activeParent = loginParent;
        //btnRegister_Register = GameObject.Find("btnRegister").GetComponent<Button>();
        //btnRegister_Back = GameObject.Find("btnBack").GetComponent<Button>();

    }

    private void Update()
    {
        //Creates Tabbing for the fields.  Move this to its own method - WatchForTab();  Include Shift-Tab
        if (activeParent == loginParent)
        {
            if (Input.GetKeyDown(KeyCode.Tab))
            {
                if (Login_UsernameField.isFocused)
                {
                    Login_PasswordField.ActivateInputField();
                    Login_PasswordField.Select();
                }
                else if (Login_PasswordField.isFocused)
                {
                    btnLogin_Login.Select();
                    
                }
                else
                {
                    Login_UsernameField.ActivateInputField();
                    Login_UsernameField.Select();
                }
            }
        }  else 
            {
                if (Input.GetKeyDown(KeyCode.Tab))
                {
                    if (Register_UsernameField.isFocused) // Move to Password
                    {
                        Register_PasswordField.ActivateInputField();
                        Register_PasswordField.Select();
                    }
                    else if (Register_PasswordField.isFocused) // Move to Confirm
                    {
                        Register_ConfirmPasswordField.ActivateInputField();
                        Register_ConfirmPasswordField.Select();
                    }
                    else if (Register_ConfirmPasswordField.isFocused)  //Move to Register Button
                    {
                        btnRegister_Register.Select();
                    }
                else
                    {
                        Register_UsernameField.ActivateInputField();
                        Register_UsernameField.Select();
                    }
            }


            }
        
    }

    private void ActivateLoginInputBox()
    {
        Login_UsernameField.ActivateInputField();
        Login_UsernameField.Select();
    }

    private void ActivateRegInputField()
    {
        if (Register_UsernameField.text == "")
        {
            Register_UsernameField.ActivateInputField();
            Register_UsernameField.Select();
        }
        else
        {
            Register_ConfirmPasswordField.ActivateInputField();
            //Register_ConfirmPasswordField.Select();
        }
    }


    //Called by Button Pressed Methods to Reset UI Fields
    void ResetAllUIElements()
    {
        //This resets all of the UI elements. It clears all the strings in the input fields and any errors being displayed
        Login_UsernameField.text = "";
        Login_PasswordField.text = "";
        Register_UsernameField.text = "";
        Register_PasswordField.text = "";
        Register_ConfirmPasswordField.text = "";
        
        BlankErrors();


    }

    void BlankErrors()
    {
        //blanks all error texts when part is changed e.g. login > Register
        Login_ErrorText.text = "";
        Register_ErrorText.text = "";
    }

    //Called by Button Pressed Methods. These use DatabaseControl namespace to communicate with server.
    IEnumerator LoginUser(string username, string password)
    {
        IEnumerator e = DCF.Login(username, password); // << Send request to login, providing username and password
        while (e.MoveNext())
        {
            yield return e.Current;
        }
        string response = e.Current as string; // << The returned string from the request

        if (response == "Success")
        {
            //Username and Password were correct. Stop showing 'Loading...' and show the LoggedIn UI. And set the text to display the username.
            ResetAllUIElements();
            //          loadingParent.gameObject.SetActive(false);
            //			backGround.gameObject.SetActive(false);
            // loggedInParent.gameObject.SetActive(true);
            UserAccountManager.instance.LogIn(username, password);
        }
        else
        {
            //Something went wrong logging in. Stop showing 'Loading...' and go back to LoginUI
            loadingParent.gameObject.SetActive(false);
            loginParent.gameObject.SetActive(true);
            if (response == "UserError")
            {
                //The Username was wrong so display relevent error message
                Login_ErrorText.text = "Error: Username not Found";
            }
            else
            {
                if (response == "PassError")
                {
                    //The Password was wrong so display relevent error message
                    Login_ErrorText.text = "Error: Password Incorrect";
                }
                else
                {
                    //There was another error. This error message should never appear, but is here just in case.
                    Login_ErrorText.text = "Error: Unknown Error. Please try again later.";
                }
            }
        }
    }

    IEnumerator RegisterUser(string username, string password, string data)
    {
        IEnumerator e = DCF.RegisterUser(username, password, data); // << Send request to register a new user, providing submitted username and password. It also provides an initial value for the data string on the account, which is "Hello World".
        while (e.MoveNext())
        {
            yield return e.Current;
        }
        string response = e.Current as string; // << The returned string from the request

        if (response == "Success")
        {
            //Username and Password were valid. Account has been created. Stop showing 'Loading...' and show the loggedIn UI and set text to display the username.
            ResetAllUIElements();
            //loadingParent.gameObject.SetActive(false);
            UserAccountManager.instance.LogIn(username, password);
        }
        else
        {
            //Something went wrong logging in. Stop showing 'Loading...' and go back to RegisterUI
            loadingParent.gameObject.SetActive(false);
            registerParent.gameObject.SetActive(true);
            if (response == "UserError")
            {
                //The username has already been taken. Player needs to choose another. Shows error message.
                Register_ErrorText.text = "Error: Username Already Taken";
            }
            else
            {
                //There was another error. This error message should never appear, but is here just in case.
                Login_ErrorText.text = "Error: Unknown Error. Please try again later.";
            }
        }
    }

    //UI Button Pressed Methods
    public void Login_LoginButtonPressed()
    {
        //Called when player presses button to Login

        //Get the username and password the player entered
        string username = Login_UsernameField.text;
        string pass = Login_PasswordField.text;
        //Check the lengths of the username and password. (If they are wrong, we might as well show an error now instead of waiting for the request to the server)
        if (username.Length > 3)
        {
            if (pass.Length > 5)
            {
                //Username and password seem reasonable. Change UI to 'Loading...'. Start the Coroutine which tries to log the player in.
                loginParent.gameObject.SetActive(false);
                loadingParent.gameObject.SetActive(true);
                StartCoroutine(LoginUser(username, pass));
            }
            else
            {
                //Password too short so it must be wrong
                Login_ErrorText.text = "Error: Password format Incorrect (length must be > 5)";
            }
        }
        else if (username.Length == 0 && pass.Length == 0)
            Login_ErrorText.text = "Error : Blank Field, Try again please.";
        else
        {
            //Username too short so it must be wrong
            Login_ErrorText.text = "Error: Username format Incorrect (length must be > 3)";
        }
    }

    public void Login_RegisterButtonPressed() //QUAND LE MEC APPUIE SUR LE BOUTON REGISTER(qui n'estpas actif de base, pour ouvrir les elements)
    {
        //Called when the player hits register on the Login UI, so switches to the Register UI
        //ResetAllUIElements();
        loginParent.gameObject.SetActive(false);
        registerParent.gameObject.SetActive(true);
        ActivateRegInputField();
        activeParent = registerParent;
        Register_UsernameField.text = Login_UsernameField.text;
        Register_PasswordField.text = Login_PasswordField.text;

    }

    public void Register_RegisterButtonPressed() //APRES AVOIR REMPLIT LES 3 champs USER PASS CONFIRM ...
    {
        //Called when the player presses the button to register

        //Get the username and password and repeated password the player entered
        string username = Register_UsernameField.text;
        string password = Register_PasswordField.text;
        string confirmedPassword = Register_ConfirmPasswordField.text;

        //Make sure username and password are long enough
        if (username.Length > 3)
        {
            if (password.Length > 5)
            {
                //Check the two passwords entered match
                if (password == confirmedPassword)
                {
                    //Username and passwords seem reasonable. Switch to 'Loading...' and start the coroutine to try and register an account on the server
                    registerParent.gameObject.SetActive(false);
                    loadingParent.gameObject.SetActive(true);
                    StartCoroutine(RegisterUser(username, password, "[KILLS]0/[DEATHS]0"));
                }
                else
                {
                    //Passwords don't match, show error
                    Register_ErrorText.text = "Error: Password's don't Match";
                }
            }
            else
            {
                //Password too short so show error
                Register_ErrorText.text = "Error: Password too Short";
            }
        }
        else
        {
            //Username too short so show error
            Register_ErrorText.text = "Error: Username too Short";
        }
    }

    public void Register_BackButtonPressed() //PAS ENVIE DE MENREGISTRER LOL, DONC REVIENS EN ARRIERE
    {
        //Called when the player presses the 'Back' button on the register UI. Switches back to the Login UI
        ResetAllUIElements();
        loginParent.gameObject.SetActive(true);
        registerParent.gameObject.SetActive(false);
        ActivateLoginInputBox();
        activeParent = loginParent;
    }

    public void LoggedIn_LogoutButtonPressed() // A METTRE UN BOUTON LOGOUT SUR LE MENU PRINCIPALE
    {
        //Called when the player hits the 'Logout' button. Switches back to Login UI and forgets the player's username and password.
        //Note: Database Control doesn't use sessions, so no request to the server is needed here to end a session.
        ResetAllUIElements();
        UserAccountManager.instance.LogOut();
        loginParent.gameObject.SetActive(true);
        //SceneManagement.LoadScene(0);
        // loggedInParent.gameObject.SetActive(false);
    }
}
