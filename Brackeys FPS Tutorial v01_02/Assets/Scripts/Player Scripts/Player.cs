using UnityEngine;
using UnityEngine.Networking;
using System.Collections;

[RequireComponent(typeof(PlayerSetup))]
public class Player : NetworkBehaviour
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

    [SyncVar]
    public string username = "Loading...";

    public int kills;
    public int deaths;

    [SerializeField]
    private Behaviour[] disableOnDeath;
    private bool[] wasEnabled;

    [SerializeField]
    private GameObject[] disableGameObjOnDeath;

    [SerializeField]
    private GameObject deathEffect;

    private bool firstSetup = true;

    public void SetupPlayer()
    {
        if (isLocalPlayer)
        {
            GameManager.instance.SetSceneCameraActive(false);
            GetComponent<PlayerSetup>().playerUIInstance.SetActive(true);
            //SetDefaults();
            // DBG: Calls routine to print all HideFlags on Objects in game
            //    Util.DumpAttributestoDebug();
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
    //private void Update()
    //{
    //    if (!isLocalPlayer)
    //        return;
    //    if (Input.GetKeyDown(KeyCode.K))
    //    {
    //        RpcTakeDamage(999, transform.name.ToString());
    //    }
    //}
    #endregion

    [ClientRpc]
    public void RpcTakeDamage(int _amount, string _sourceID)
    {
        if (_isDead)
            return;

        currenthealth -= _amount;
        Debug.Log(transform.name + " now has " + currenthealth + " health.");

        if (currenthealth <= 0)
        {
            Die(_sourceID);
        }
    }

    private void Die(string _sourceID)
    {
        IsDead = true;

        Player sourcePlayer = GameManager.GetPlayer(_sourceID);
        if (sourcePlayer != null)
        {
            sourcePlayer.kills++;
            GameManager.instance.onPlayerKilledCallBack.Invoke(username, sourcePlayer.username);
        }

        deaths++;

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

        if (isLocalPlayer)
        {
            GameManager.instance.SetSceneCameraActive(true);
            GetComponent<PlayerSetup>().playerUIInstance.SetActive(false);

        }

        //Spawn Death Effect
        AudioManager audioManager = FindObjectOfType<AudioManager>().GetComponent<AudioManager>();
        GameObject _gfxInst = Instantiate(deathEffect, transform.position, Quaternion.identity);
        audioManager.Play("sndExplosion", _gfxInst, 1.5f);
        Destroy(_gfxInst, 3f);

        StartCoroutine(Respawn());
    }

    private IEnumerator Respawn()
    {
        yield return new WaitForSeconds(GameManager.instance.matchsettings.respwanTime);

        Transform _spawnPoint = NetworkManager.singleton.GetStartPosition();
        transform.position = _spawnPoint.position;
        transform.rotation = _spawnPoint.rotation;

        yield return new WaitForSeconds(0.15f);

        //Test code to try to get rid of the "firing after respawn" problem:
        Input.ResetInputAxes();

        // Added to make sure respawns get a primary weapon:
        if (GetComponent<WeaponManager>().GetCurrentWeapon() == null)
        {
        GetComponent<WeaponManager>().EquipWeapon();
        }
            Debug.Log("Player " + transform.name + " has started respawn");
        SetupPlayer();
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

        AudioManager audioManager = FindObjectOfType<AudioManager>().GetComponent<AudioManager>();
        audioManager.Play("sndBoosterRegular", gameObject, 300f);
        //ThrusterSounds thrusterSounds = FindObjectOfType<ThrusterSounds>();
        //thrusterSounds.StartRegularBoosterSound();


    }

    public float GetHealthBarAmt()
    {
        return (float)currenthealth / maxhealth;
    }
}
