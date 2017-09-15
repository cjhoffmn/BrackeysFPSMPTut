using UnityEngine;
using UnityEngine.Networking;

[RequireComponent(typeof(Player))]
[RequireComponent(typeof(PlayerController))]
public class TargetSetup : NetworkBehaviour
{
    [SerializeField]
    Behaviour[] componentsToDisable;

    [SerializeField]
    string remoteLayerName = "RemotePlayer";

    [SerializeField]
    string localLayerName = "LocalPlayer";

    [SerializeField]
    string dontDrawLayerName = "DontDraw";

    [SerializeField]
    GameObject playerGraphics;

    //[SerializeField]
    //GameObject playerUIPrefab;

    //[HideInInspector]
    //public GameObject playerUIInstance;


    private void Start()
    {
        if (!isLocalPlayer)
        {
            DisableComponents();
            AssignRemoteLayer();
        }
        else
        {
            //we are the local player  -REVIEW THIS?  WHY IS IT HERE?
            AssignLocalLayer();

            //Disable the player Graphics for Local Player
            SetLayerRecursively(playerGraphics, LayerMask.NameToLayer(dontDrawLayerName));

            // Create PlayerUI
            //playerUIInstance = Instantiate(playerUIPrefab);
            //playerUIInstance.name = playerUIPrefab.name;

            //configure PlayerUI
                //PlayerUI ui = playerUIInstance.GetComponent<PlayerUI>();
                //if (ui == null)
                //    Debug.Log("Missing PlayerUI on PlayerUI Prefab");

                //ui.SetController(GetComponent<PlayerController>());

                GetComponent<Player>().SetupPlayer();
        }


    }

    void SetLayerRecursively(GameObject obj, int newLayer)
    {
        obj.layer = newLayer;

        if (obj.name != "Thrusters")
        {
            foreach (Transform child in obj.transform)
            {
                SetLayerRecursively(child.gameObject, newLayer);
            }
        }
        else
        {
            obj.layer = LayerMask.NameToLayer(localLayerName);

            foreach (Transform child in obj.transform)
            {
                SetLayerRecursively(child.gameObject, LayerMask.NameToLayer(localLayerName));
            }
        }
    }

    public override void OnStartClient()
    {
        base.OnStartClient();

        string _netID = GetComponent<NetworkIdentity>().netId.ToString();
        Player _player = GetComponent<Player>();

        GameManager.RegisterPlayer (_netID, _player);

    }

    void AssignRemoteLayer()
    {
        gameObject.layer = LayerMask.NameToLayer(remoteLayerName);
    }

    void AssignLocalLayer()
    {
        gameObject.layer = LayerMask.NameToLayer(localLayerName);
    }

    void DisableComponents()
    {
        for (int i = 0; i < componentsToDisable.Length; i++)
        {
            componentsToDisable[i].enabled = false;
        }
    }

    private void OnDisable()
    {
        //Destroy(playerUIInstance);

        GameManager.instance.SetSceneCameraActive(true);

        GameManager.DeRegisterPlayer(transform.name);
    }
}
