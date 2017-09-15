//#region


////using UnityEngine;
////using System.Collections;
////using DatabaseControl;
////using UnityEngine.SceneManagement;

////public class UserAccountManager : MonoBehaviour {

////	public static UserAccountManager instance;

////	void Awake ()
////	{
////		if (instance != null)
////		{
////			Destroy(gameObject);
////			return;
////		}

////		instance = this;
////		DontDestroyOnLoad(this);
////	}

////	public static string LoggedIn_Username { get; protected set; } //stores username once logged in
////	private static string LoggedIn_Password = ""; //stores password once logged in

////	public static bool IsLoggedIn { get; protected set; }

////	public string loggedInSceneName = "Lobby";
////	public string loggedOutSceneName = "LoginMenu";

////	public delegate void OnDataReceivedCallback(string data);

////	public void LogOut ()
////	{
////		LoggedIn_Username = "";
////		LoggedIn_Password = "";

////		IsLoggedIn = false;

////		Debug.Log("User logged out!");

////		SceneManager.LoadScene(loggedOutSceneName);
////	}

////	public void LogIn(string username, string password)
////	{
////		LoggedIn_Username = username;
////        LoggedIn_Password = password;

////		IsLoggedIn = true;

////		Debug.Log("Logged in as " + username);

////		SceneManager.LoadScene(loggedInSceneName);
////	}

////	public void SendData(string data)
////	{ //called when the 'Send Data' button on the data part is pressed
////		if (IsLoggedIn)
////		{
////			//ready to send request
////			StartCoroutine(sendSendDataRequest(LoggedIn_Username, LoggedIn_Password, data)); //calls function to send: send data request
////		}
////	}

////	IEnumerator sendSendDataRequest(string username, string password, string data)
////	{
////		IEnumerator eee = DatabaseControl.DCF.SetUserData(username, password, data);
////		while (eee.MoveNext())
////		{
////			yield return eee.Current;
////		}
////		WWW returneddd = eee.Current as WWW;
////		if (returneddd.text == "ContainsUnsupportedSymbol")
////		{
////			//One of the parameters contained a - symbol
////			Debug.Log("Data Upload Error. Could be a server error. To check try again, if problem still occurs, contact us.");
////		}
////		if (returneddd.text == "Error")
////		{
////			//Error occurred. For more information of the error, DatabaseControl.DCFLogin could
////			//be used with the same username and password
////			Debug.Log("Data Upload Error: Contains Unsupported Symbol '-'");
////		}
////	}

////	public void GetData(OnDataReceivedCallback onDataReceived)
////	{ //called when the 'Get Data' button on the data part is pressed

////		if (IsLoggedIn)
////		{
////			//ready to send request
////			StartCoroutine(sendGetDataRequest(LoggedIn_Username, LoggedIn_Password, onDataReceived)); //calls function to send get data request
////		}
////	}

////	IEnumerator sendGetDataRequest(string username, string password, OnDataReceivedCallback onDataReceived)
////	{
////		string data = "ERROR";

////		IEnumerator eeee = DatabaseControl.DCF.GetUserData(username, password);
////		while (eeee.MoveNext())
////		{
////			yield return eeee.Current;
////		}
////		WWW returnedddd = eeee.Current as WWW;
////		if (returnedddd.text == "Error")
////		{
////			//Error occurred. For more information of the error, DC.Login could
////			//be used with the same username and password
////			Debug.Log("Data Upload Error. Could be a server error. To check try again, if problem still occurs, contact us.");
////		}
////		else
////		{
////			if (returnedddd.text == "ContainsUnsupportedSymbol")
////			{
////				//One of the parameters contained a - symbol
////				Debug.Log("Get Data Error: Contains Unsupported Symbol '-'");
////			}
////			else
////			{
////				//Data received in returned.text variable
////				string DataRecieved = returnedddd.text;
////				data = DataRecieved;
////			}
////		}

////		if (onDataReceived != null)
////			onDataReceived.Invoke(data);
////	}

////}

//#endregion

//#region MyAttempt

////using UnityEngine;
////using System.Collections;
////using DatabaseControl;

////public class UserAccountManager : MonoBehaviour
////{
////    public static UserAccountManager instance;

////    private void Awake()
////    {
////        if(instance != null)
////        {
////            Destroy(gameObject);
////            return;
////        }
////        instance = this;
////        DontDestroyOnLoad(this);
////    }

////    public static string LoggedIn_Username { get; protected set; } //stores username once logged in
////    private static string LoggedIn_Password = ""; //stores password once logged in
////    public static string LoggedIn_Data { get; protected set; }

////    public static bool IsLoggedIn { get; protected set; }


////    public void LogOut()
////    {
////        LoggedIn_Username = "";
////        LoggedIn_Password = "";

////        IsLoggedIn = false;
////        Debug.Log("UserLoggedOut");
////    }
////    public void LogIn(string _username, string _password)
////    {
////        LoggedIn_Username = _username;
////        LoggedIn_Password = _password;

////        IsLoggedIn = true;
////        Debug.Log("logged in as" + _username);
////    }

////    public void SendData(string _data)
////    { //called when the 'Send Data' button on the data part is pressed
////        if (IsLoggedIn)
////        {
////            //ready to send request
////            StartCoroutine(sendSendDataRequest(LoggedIn_Username, LoggedIn_Password, _data)); //calls function to send: send data request
////        }
////    }

////    IEnumerator sendSendDataRequest(string username, string password, string data)
////    {

    
////        IEnumerator eee = DCF.SetUserData(username, password, data);
////        while (eee.MoveNext())
////        {
////            yield return eee.Current;
////        }
////        WWW returneddd = eee.Current as WWW;
////        if (returneddd.text == "ContainsUnsupportedSymbol")
////        {
////            //One of the parameters contained a - symbol
////            Debug.Log("Data Upload Error. Could be a server error. To check try again, if problem still occurs, contact us.");
////        }
////        if (returneddd.text == "Error")
////        {
////            //Error occurred. For more information of the error, DC.Login could
////            //be used with the same username and password
////            Debug.Log("Data Upload Error: Contains Unsupported Symbol '-'");
////        }

        
////    }

////    public void GetData()
////    { //called when the 'Get Data' button on the data part is pressed

////        if (IsLoggedIn)
////        {
////            //ready to send request
////            StartCoroutine(sendGetDataRequest(LoggedIn_Username, LoggedIn_Password)); //calls function to send get data request
////        }
////    }

////    IEnumerator sendGetDataRequest(string username, string password)
////    {

////        string data = "Error";

////        IEnumerator eeee = DCF.GetUserData(username, password);
////        while (eeee.MoveNext())
////        {
////            yield return eeee.Current;
////        }
////        WWW returnedddd = eeee.Current as WWW;
////        if (returnedddd.text == "Error")
////        {
////            //Error occurred. For more information of the error, DC.Login could
////            //be used with the same username and password
////            Debug.Log("Data Upload Error. Could be a server error. To check try again, if problem still occurs, contact us.");
////        }
////        else
////        {
////            if (returnedddd.text == "ContainsUnsupportedSymbol")
////            {
////                //One of the parameters contained a - symbol
////                Debug.Log("Get Data Error: Contains Unsupported Symbol '-'");
////            }
////            else
////            {
////                //Data received in returned.text variable
////                string DataRecieved = returnedddd.text;
////                data = DataRecieved;
////            }
////        }

////        LoggedIn_Data = data;
       
////    }
////}
//#endregion

////==========================
////Class: UserAccountManager:
////==========================

//using System.Collections;
//using System.Collections.Generic;
//using UnityEngine;
//using DatabaseControl;
//using UnityEngine.SceneManagement;

//public class UserAccountManager : MonoBehaviour
//{

//    public static UserAccountManager instance;

//    void Awake()
//    {
//        if (instance != null)
//        {
//            Destroy(gameObject);
//            return;
//        }

//        instance = this;
//        DontDestroyOnLoad(this);
//    }

//    public static string playerUsername { get; protected set; }
//    public static string playerPassword { get; protected set; }

//    public static string LoggedIn_data { get; protected set; }

//    public static bool isLoggedIn { get; protected set; }

//    public void LogOut()
//    {
//        playerUsername = "";
//        playerPassword = "";
//        isLoggedIn = false;
//        SceneManager.LoadScene(0);
//    }

//    public void LogIn(string _Username, string _Password)
//    {
//        playerUsername = _Username;
//        playerPassword = _Password;
//        isLoggedIn = true;
//        SceneManager.LoadScene(1);
//    }

//    public void SendData(string data)
//    {
//        if (isLoggedIn)
//            StartCoroutine(SetData(data));
//    }

//    IEnumerator SetData(string data)
//    {
//        IEnumerator e = DCF.SetUserData(playerUsername, playerPassword, data); // << Send request to set the player's data string. Provides the username, password and new data string
//        while (e.MoveNext())
//        {
//            yield return e.Current;
//        }
//        string response = e.Current as string; // << The returned string from the request

//        if (response == "Success")
//        {
//            //The data string was set correctly. Goes back to LoggedIn UI
//            Debug.Log("Succes sending data");
//            // loggedInParent.gameObject.SetActive(true);
//        }
//        else
//        {
//            Debug.Log("Error: Unknown Error. Please try again later. Send data problem");
//        }
//    }

//    public void GetData()
//    {
//        //Called when the player hits 'Get Data' to retrieve the data string on their account. Switches UI to 'Loading...' and starts coroutine to get the players data string from the server
//        if (isLoggedIn)
//            StartCoroutine(GetData_numerator());
//    }

//    IEnumerator GetData_numerator()
//    {
//        string data = "ERROR";
//        IEnumerator e = DCF.GetUserData(playerUsername, playerPassword); // << Send request to get the player's data string. Provides the username and password
//        while (e.MoveNext())
//        {
//            yield return e.Current;
//        }
//        string response = e.Current as string; // << The returned string from the request

//        if (response == "Error")
//        {
//            Debug.Log("Error: Unknown Error. Please try again later. GetDataProblem");
//        }
//        else
//        {
//            //The player's data was retrieved. Goes back to loggedIn UI and displays the retrieved data in the InputField
//            data = response;
//        }

//        LoggedIn_data = data;
//    }
//}