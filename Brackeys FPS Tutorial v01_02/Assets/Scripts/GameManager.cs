using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;


public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    public MatchSettings matchsettings;

    [SerializeField]
    private GameObject SceneCamera;

    public delegate void OnPlayerKilledCallback(string player, string source);
    public OnPlayerKilledCallback onPlayerKilledCallBack;

    private void Awake()
    {
        if(instance != null)
        {
            Debug.Log("more than one game manager in scene");
        }
        else
        {
            instance = this;
        }
    }

    public void SetSceneCameraActive (bool isActive)
    {
        if (SceneCamera == null)
            return;
        SceneCamera.SetActive(isActive);
    }


    #region PlayerTrackingandSetup

    private const string PLAYER_ID_PREFIX = "Player ";

    private static Dictionary<string, Player> players = new Dictionary<string, Player>();

    public static void RegisterPlayer(string _netID, Player _player)
    {
        string _playerID = PLAYER_ID_PREFIX + _netID;
        players.Add(_playerID, _player);
        _player.transform.name = _playerID;
    }

    public static void DeRegisterPlayer(string _playerID)
    {
        players.Remove(_playerID);
    }

    public static Player GetPlayer(string _playerID)
    {
        return players[_playerID];
    }

    public static Player[] GetAllPlayers()
    {
        return players.Values.ToArray();
    }

    //void OnGUI()
    //{
    //    GUILayout.BeginArea(new Rect(200, 200, 200, 500));
    //    GUILayout.BeginVertical();

    //    foreach (string _PlayerID in players.Keys) 
    //    {
    //        GUILayout.Label(_PlayerID + " - " + players[_PlayerID].transform.name);
    //    }

    //    GUILayout.EndVertical();
    //    GUILayout.EndArea();
    //}

    #endregion



}
