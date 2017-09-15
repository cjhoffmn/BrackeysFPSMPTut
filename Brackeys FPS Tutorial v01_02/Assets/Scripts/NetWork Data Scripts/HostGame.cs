using UnityEngine;
using UnityEngine.Networking;

public class HostGame : MonoBehaviour {

    [SerializeField]
    private uint roomSize = 4;

    private string roomName;

    private NetworkManager networkManager;


    private void Start()
    {
        networkManager = NetworkManager.singleton;
        if(networkManager.matchMaker == null)
        {
            networkManager.StartMatchMaker();
            Debug.Log("Matchmaker was Started");
        }
    }

    public void SetRoomName(string _name)
    {
        roomName = _name;
        Debug.Log("roomName variable was set to: " + roomName);
    }

    public void CreateRoom()
    {

        Debug.Log("Create Room was Called");
        if (roomName != "" && roomName != null)
        {
            Debug.Log("Creating Room: " + roomName + " with " + roomSize + " players.");
            // Create room
            networkManager.matchMaker.CreateMatch(roomName, roomSize, true, "", "", "", 0, 0, networkManager.OnMatchCreate);
            JoinGame.inNetworkGame = true;
        }
    }

    


}
