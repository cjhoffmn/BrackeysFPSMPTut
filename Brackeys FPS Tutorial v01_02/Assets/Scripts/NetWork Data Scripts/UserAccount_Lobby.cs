using UnityEngine.UI;
using UnityEngine;

public class UserAccount_Lobby : MonoBehaviour {

	public Text usernameText;

    private void Start()
    {
        if (UserAccountManager.isLoggedIn)
        {
            usernameText.text = UserAccountManager.playerUsername;
        }
    }
    public void Logout()
    {
        if (UserAccountManager.isLoggedIn)
        {
            UserAccountManager.instance.LogOut();
        }
    }
}
