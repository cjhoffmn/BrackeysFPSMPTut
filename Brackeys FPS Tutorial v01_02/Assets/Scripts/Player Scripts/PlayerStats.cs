using System;
using UnityEngine;
using UnityEngine.UI;


public class PlayerStats : MonoBehaviour
{

    public Text killCount;
    public Text deathCount;

    private void Start()
    {
        if(UserAccountManager.isLoggedIn)
            UserAccountManager.instance.GetData(OnReceivedData);
    }

    void OnReceivedData(string data)
    {
        // Get String with Data
        //string[] playerdatastring = data.Split(char.Parse("/"));
        //killCount.text = playerdatastring[0];
        //deathCount.text = playerdatastring[1];

        if (killCount == null || deathCount == null)
        {
            return;
        }


        killCount.text = "Kills: " + DataTranslator.DataToKills(data).ToString();
        deathCount.text = "Deaths: " + DataTranslator.DataToDeaths(data).ToString();

        Debug.Log(data);

    }

}
