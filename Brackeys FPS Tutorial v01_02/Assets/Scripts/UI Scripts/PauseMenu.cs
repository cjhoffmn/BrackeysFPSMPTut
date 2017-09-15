using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;
using UnityEngine.Networking.Match;
using System.Collections;



public class PauseMenu : MonoBehaviour {
    private NetworkManager netWMgr;

    public static bool isOn = false;

    [SerializeField]
    private Text btnLeaveText;

    private void Start()
    {
        netWMgr = NetworkManager.singleton;
    }

    public void LeaveRoom()
    {
        if (JoinGame.inNetworkGame == true)
        {
            MatchInfo matchinfo = netWMgr.matchInfo;
            netWMgr.matchMaker.DropConnection(matchinfo.networkId, matchinfo.nodeId, 0, netWMgr.OnDropConnection);
            netWMgr.StopHost();
            JoinGame.inNetworkGame = false;
        }
        netWMgr.StopHost();
        JoinGame.inNetworkGame = false;
        return;
    }
}
