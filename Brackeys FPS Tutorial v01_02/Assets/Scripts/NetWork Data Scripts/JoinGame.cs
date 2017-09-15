using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;
using UnityEngine.Networking.Match;


public class JoinGame : MonoBehaviour {

        List<GameObject> roomList = new List<GameObject>();
    [SerializeField]
        private Text status;
    [SerializeField]
        private GameObject roomListItemPrefab;
    [SerializeField]
        private Transform roomListParent;
    [SerializeField]
        private string roomPassword;

        private NetworkManager networkManager;

        public static bool inNetworkGame = false;

	// Use this for initialization
	void Start ()
        {
            networkManager = NetworkManager.singleton;
            if (networkManager.matchMaker == null)
            {
                networkManager.StartMatchMaker();
                ClearRoomList();
                RefreshRoomList();
            }
        }
	
    public void RefreshRoomList()
        {
            ClearRoomList();
            if (networkManager.matchMaker == null)
            {
                networkManager.StartMatchMaker();
            }
            status.text = "Loading...";
            networkManager.matchMaker.ListMatches(0, 20, "", true, 0, 0, OnMatchList);
        }
	
    public void OnMatchList(bool success, string extendedInfo, List<MatchInfoSnapshot> matchList)
        {
            status.text = "";

                if (!success || matchList == null)
            {
                status.text = "Couldn't retreive room list";
                return;
            }

            foreach (MatchInfoSnapshot match in matchList)
            {
                GameObject _roomListItemGO = Instantiate(roomListItemPrefab);
                _roomListItemGO.transform.SetParent(roomListParent);

                RoomListItem _roomListItem = _roomListItemGO.GetComponent<RoomListItem>();
                if(_roomListItem != null)
                {
                    _roomListItem.Setup(match, JoinRoom);
                }

                roomList.Add(_roomListItemGO);
            }

            if (roomList.Count == 0)
            {
                status.text = "No Rooms Available Now.";
            }
        }

    void ClearRoomList()
        {
            for (int i = 0; i < roomList.Count; i++)
            {
                Destroy(roomList[i]);
            }
            roomList.Clear();
        }

    public void JoinRoom(MatchInfoSnapshot _match)
        {
            networkManager.matchMaker.JoinMatch(_match.networkId, "", "", "", 0, 0, networkManager.OnMatchJoined);
            StartCoroutine(WaitForJoin());
            inNetworkGame = true;
        }

    IEnumerator WaitForJoin()
        {
        ClearRoomList();
        int countdown = 7;
        while (countdown > 0)
        {
            status.text = "Joining... (" + countdown + " secs )";
            countdown--;
        yield return new WaitForSeconds(1);
            MatchInfo matchinfo = networkManager.matchInfo;

            if (matchinfo != null)
            {
            networkManager.matchMaker.DropConnection(matchinfo.networkId, matchinfo.nodeId, 0, networkManager.OnDropConnection);
            networkManager.StopHost();
            inNetworkGame = false;

            }
        }

        //Failed to Connect
            status.text = "Failed - Retrying....";
        yield return new WaitForSeconds(1);
            RefreshRoomList();
    }
    
}
