using UnityEngine;
using UnityEngine.Networking;
using System.Collections;


public class WeaponManager : NetworkBehaviour
{
    [SerializeField]
    private string weaponLayerName = "Weapon";

    private string remoteLayerName;

    [SerializeField]
    private Transform weaponHolder;

    [SerializeField]
    private PlayerWeapon primaryWeapon;

    private PlayerWeapon currentWeapon;
    private WeaponGraphics currentGraphics;
    private AudioManager audioManager;

    public bool isReloading = false;
    public bool isFiring = false;
    public bool isClippedOut = false;
    public Animator animWPN;

    void Start ()
    {
        EquipWeapon(primaryWeapon);
        //currentGraphics = GetCurrentGraphics();
        remoteLayerName = PlayerSetup.remoteLayerName;
        animWPN = currentGraphics.GetComponent<Animator>();
        audioManager = FindObjectOfType<AudioManager>().GetComponent<AudioManager>();
    }

    //public void EquipWeapon(PlayerWeapon _weaapon)  BEFORE adjustment for call from respawn
    public void EquipWeapon(PlayerWeapon _weapon = null)
    {
        if (_weapon == null)
            _weapon = primaryWeapon;

        currentWeapon = _weapon;

        GameObject _weaponIns = Instantiate(_weapon.wpnGraphics, weaponHolder.position, weaponHolder.rotation);
        _weaponIns.transform.transform.SetParent(weaponHolder);

       currentGraphics = _weaponIns.GetComponent <WeaponGraphics>();

        if(currentGraphics == null)
        {
            Debug.Log("Graphics are missing on Weapon: " + _weaponIns);
        }
        
        if (isLocalPlayer)
        {
            Util.SetLayerRecursively(_weaponIns, LayerMask.NameToLayer(weaponLayerName));
        }

        if (!isLocalPlayer)
        {
            Util.SetLayerRecursively(_weaponIns, LayerMask.NameToLayer(remoteLayerName));
        }
    }
    // Update is called once per frame

    public PlayerWeapon GetCurrentWeapon()
    {
        return currentWeapon;
    }
    public WeaponGraphics GetCurrentGraphics()
    {
        return currentGraphics;
    }
    public void Reload()
    {
        if (isReloading)
        {
            return;
        }
        StartCoroutine(ReloadDelay());
    }

    public void ClippedOut()
    {
        CmdOnClippedOut();
    }

    public void Firing()
    {
        if (isFiring)
        {
            return;
        }

        StartCoroutine(FiringDelay());
    }
    private IEnumerator ReloadDelay()
    {

        //Debug.Log("Reloading...");
        isReloading = true;

        CmdOnReload();

        yield return new WaitForSeconds(currentWeapon.reloadTime);
        isReloading = false;

        currentWeapon.bullets = currentWeapon.maxBullets;
        
    }
    private IEnumerator FiringDelay()
    {
        isFiring = true;
        CmdOnFiring();

        yield return new WaitForSeconds(currentWeapon.GetFiringTime());

        isFiring = false;
    }
    [Command]
    void CmdOnReload ()
    {
        RpconReload();
    }

    [ClientRpc]
    void RpconReload()
    {
        if (animWPN != null)
        {
            animWPN.SetTrigger("isReloading");
            animWPN.SetBool("isClippedOut", false);
                StartCoroutine(PlayReloadSound(.625f));
            isClippedOut = false;
        }
    }

    private IEnumerator PlayReloadSound(float _Waittime)
    {
            audioManager.Play("sndStartReload", gameObject, .75f);
        yield return new WaitForSeconds(_Waittime);
            audioManager.Play("sndReload", gameObject, .75f);
    }




    [Command]
    void CmdOnClippedOut()
    {
        RpconClippedOut();
    }

    [ClientRpc]
    void RpconClippedOut()
    {
        if (animWPN != null)
        {
            animWPN.SetBool("isClippedOut", true);
            isClippedOut = true;
            audioManager.Play("sndClippedOut", gameObject, .75f);
        }
    }

    [Command]
    void CmdOnFiring()
    {
        RpconFiring();
    }

    [ClientRpc]
    void RpconFiring()
    {
        if (animWPN != null)
        {
            animWPN.SetTrigger("isFiring");
        }
    }
}
