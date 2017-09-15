using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Player))]
public class PlayerScore : MonoBehaviour
{
    int lastkills = 0;
    int lastdeaths = 0;
    Player player;

    private void Start()
    {
        player = GetComponent<Player>();
        StartCoroutine(SyncScoreLoop());
    }

    private void OnDestroy()
    {
        if (player != null)
        {
            SyncNow();
        }
    }

    IEnumerator SyncScoreLoop()
    {
        while (true)
        {
            yield return new WaitForSeconds(20);
            SyncNow();
        }
    }

    void SyncNow()
    {
        if (UserAccountManager.isLoggedIn)
        {
            UserAccountManager.instance.GetData(OnDataReceived);
        }
    }

    void OnDataReceived(string data)
    {
        if (player.kills <= lastkills && player.deaths <= lastdeaths)
        {
            return;
        }
        int killSinceLast = player.kills - lastkills;
        int deathsSinceLast = player.deaths - lastdeaths;


        if (deathsSinceLast == 0 && killSinceLast == 0)
        {
            return;
        }

        int kills = DataTranslator.DataToKills(data);
        int deaths = DataTranslator.DataToDeaths(data);

        int newkills = killSinceLast + kills;
        int newdeaths = deathsSinceLast + deaths;

        string newData = DataTranslator.ValuesToData(newkills, newdeaths);

        Debug.Log("Syncing " + newData);

        lastkills = player.kills;
        lastdeaths = player.deaths;

        UserAccountManager.instance.SendData(newData);

    }
}
