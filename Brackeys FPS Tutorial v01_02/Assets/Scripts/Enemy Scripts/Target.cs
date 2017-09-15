using UnityEngine;
using UnityEngine.Networking;
using System.Collections;

[RequireComponent(typeof(PlayerSetup))]
public class Target : NetworkBehaviour
{
    [SyncVar]
    private bool _isDead = false;
    public bool IsDead
    {
        get { return _isDead; }
        protected set { _isDead = value; }
    }


    [SerializeField]
    private int maxhealth = 100;

    [SyncVar]
    private int currenthealth = 100;
    
    
    [SerializeField]
    private Behaviour[] disableOnDeath;
    private bool[] wasEnabled;

    [SerializeField]
    private GameObject[] disableGameObjOnDeath;


    [SerializeField]
    private GameObject deathEffect;

    private bool firstSetup = true;



    public void PlayerSetup()
    {
        if (isLocalPlayer)
        {
        GameManager.instance.SetSceneCameraActive(false);
        //GetComponent<PlayerSetup>().playerUIInstance.SetActive(true);
        }

        CmdBroadcastNewPlayerSetup();
    }

    [Command]
    private void CmdBroadcastNewPlayerSetup()
    {
    RpcSetupPlayeronAllClients();
    }

    [ClientRpc]
    private void RpcSetupPlayeronAllClients()
    {
        if (firstSetup)
        {
            wasEnabled = new bool[disableOnDeath.Length];
            for (int i = 0; i < wasEnabled.Length; i++)
            {
                wasEnabled[i] = disableOnDeath[i].enabled;
            }

            firstSetup = false;
        }
    SetDefaults();
    }



    #region Testing KillKey
    private void Update()
    {
        if (!isLocalPlayer)
            return;
        if (Input.GetKeyDown(KeyCode.K))
        {
            RpcTakeDamage(9999);
        }
    }
    #endregion

    [ClientRpc]
    public void RpcTakeDamage(int _amount)
    {
        if (_isDead)
            return;

        currenthealth -= _amount;
        Debug.Log(transform.name + " now has " + currenthealth + " health.");
        if (currenthealth <= 0)
        {
            Die();
        }
    }
        
    private void Die()
    { 
        IsDead = true;
        // Disable Components on Death
        for (int i = 0; i < disableOnDeath.Length; i++)
        {
            disableOnDeath[i].enabled = false;
        }

        // Disable Game Objects
        for (int i = 0; i < disableGameObjOnDeath.Length; i++)
        {
            disableGameObjOnDeath[i].SetActive(false);
        }

        //Disable Collider
        Collider _col = GetComponent<Collider>();
        if (_col != null)
        {
            _col.enabled = false;
        }

        //Spawn Death Effect
        GameObject _gfxInst = (GameObject)Instantiate(deathEffect, transform.position, Quaternion.identity);
        Destroy(_gfxInst, 3f);
 
       

        //Switch to Scene Camera
        if (isLocalPlayer)
        {
            GameManager.instance.SetSceneCameraActive(true);
            //GetComponent<PlayerSetup>().playerUIInstance.SetActive(false);
        }


        Debug.Log(transform.name + " is DEAD!");

        // Call Respawn method

        StartCoroutine(Respawn());
    }

    private IEnumerator Respawn()
    {
        yield return new WaitForSeconds(GameManager.instance.matchsettings.respwanTime);

        //SetDefaults();
        //GameManager.instance.SetSceneCameraActive(false);
        //GetComponent<PlayerSetup>().playerUIInstance.SetActive(true);

        Transform _spawnPoint = NetworkManager.singleton.GetStartPosition();
        transform.position = _spawnPoint.position;
        transform.rotation = _spawnPoint.rotation;

        yield return new WaitForSeconds(0.1f);

        // Added to make sure respawns get a primary weapon:
        GetComponent<WeaponManager>().EquipWeapon();

        PlayerSetup();
        Debug.Log("Player " + transform.name + " has respawned");
    }

    public void SetDefaults()
    {
        IsDead = false;
        currenthealth = maxhealth;

        //Enable Components
        for (int i = 0; i < disableOnDeath.Length; i++)
        {
            disableOnDeath[i].enabled = wasEnabled[i];
        }
        // Enable GameObjects
        for (int i = 0; i < disableGameObjOnDeath.Length; i++)
        {
            disableGameObjOnDeath[i].SetActive(true);
        }

        Collider _col = GetComponent<Collider>();
            if (_col != null)
                _col.enabled = true;
        }
    

}
