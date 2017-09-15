using UnityEngine;
using UnityEngine.Networking;
using System.Collections;


[RequireComponent(typeof(WeaponManager))]
public class PlayerShoot : NetworkBehaviour
{
    private const string PLAYER_TAG = "Player";
    private const string TARGET_TAG = "Target";

    [SerializeField]
    private Camera cam;
    
    [SerializeField]
    private LayerMask mask;
    private PlayerWeapon currentWeapon;
    private WeaponManager weaponManager;
    private ADS_SFA adsSFA;
    private AudioManager audioManager;

    // Use this for initialization
    void Start()
    {
        if (cam == null)
        {
            Debug.LogError("Playershoot: No Camera Loaded");
            this.enabled = false;
        }
        weaponManager = GetComponent<WeaponManager>();
        audioManager = FindObjectOfType<AudioManager>().GetComponent<AudioManager>();
    }

    void Update()
    {
        currentWeapon = weaponManager.GetCurrentWeapon();

        if (PauseMenu.isOn == true) return;

        if (currentWeapon.bullets < currentWeapon.maxBullets)
        {
            //if (Input.GetKeyDown(KeyCode.R))
            if (Input.GetButtonDown("Reload"))
            {
                weaponManager.Reload();
                return;
            }
        }

        if (currentWeapon.fireRate <= 0f)
        {
            //if (Input.GetButtonDown("Fire1") || Input.GetAxisRaw("Fire1J") == 1f)
            if (Input.GetButtonDown("Fire1"))
            {
                Shoot();
            }
        }
        else
        {
            if (Input.GetButtonDown("Fire1"))
            {
                InvokeRepeating("Shoot", 0f, 1f / currentWeapon.fireRate);
            }
            else if (Input.GetButtonUp("Fire1"))
            {
                CancelInvoke("Shoot");
            }

            #region Joystick
            //if (Input.GetAxisRaw("Fire1J") == 1f)
            //{
            //    InvokeRepeating("Shoot", 0f, 1f / currentWeapon.fireRate);
            //}
            //else if (Input.GetAxisRaw("Fire1J") == 0f)
            //{
            //    CancelInvoke("Shoot");
            //}
            #endregion

        }
    }

    //called on server when player shoots
    [Command]
    void CmdOnShoot()
    {
        RpcDoShootEffect();
    }
    //Is called on all clients when something is hit and takes in hit point and normal of surface
    [Command]
    void CmdOnHit(Vector3 _pos, Vector3 _normal, string _taghit)
    {
        RpcDoHitEffect(_pos, _normal, _taghit);
    }

    //Is called on all clients when something is hit and takes in hit point and normal of surface
    [ClientRpc]
    void RpcDoHitEffect(Vector3 _pos, Vector3 _normal, string _taghit)
    {
        //Debug.Log("RpcDoHitEffect was Called")
        
        GameObject _hitEffect = Instantiate(weaponManager.GetCurrentGraphics().hitEffectPrefab, _pos, Quaternion.LookRotation(_normal));
        if (_taghit == PLAYER_TAG)
        {
            audioManager.Play("sndPlayerHit", _hitEffect, .75f);
        }
        audioManager.Play("sndHit", _hitEffect, .5f);
        Destroy(_hitEffect, 1.25f);
    }


    //Is called on all clients when shooting
    [ClientRpc]
    void RpcDoShootEffect()
    {
        //Debug.Log("RPCDoShoot was Called");
        weaponManager.GetCurrentGraphics().muzzleFlash.Play();
        GameObject wpnSoundSource = GameObject.Find("FirePoint");
        audioManager.Play("sndBlaster", wpnSoundSource, .5f);
        
    }

    [Client]
    void Shoot()
    {
        if (!isLocalPlayer || weaponManager.isReloading || weaponManager.isClippedOut)
        {
            return;
        }

        if (currentWeapon.bullets <= 0)
        {
            //Debug.Log("Out of Bullets");
            weaponManager.ClippedOut();
            StartCoroutine(AutoReload());
            //weaponManager.Reload();
            return;
        }


        //calls shoot method on the server
        currentWeapon.bullets--;

        Debug.Log(transform.name + " remaining bullets = " + currentWeapon.bullets);

        weaponManager.Firing();
        Recoil();
        CmdOnShoot();

        //Debug.Log("Shoot!");
        RaycastHit _hit;
        if (Physics.Raycast(cam.transform.position, cam.transform.forward, out _hit, currentWeapon.range, mask))
        {
            // We hit something
            //Debug.Log("We hit " + _hit.collider.name);

            if (_hit.collider.tag == PLAYER_TAG || _hit.collider.tag == TARGET_TAG)
            {
                CmdPlayerShot(_hit.collider.name, currentWeapon.damage, transform.name);
                //Debug.Log("We hit " + _hit.collider.name);
            }

            CmdOnHit(_hit.point, _hit.normal, _hit.collider.tag);
            
        }
    }

    private void Recoil()
    {
        Quaternion origCamRotation = cam.transform.localRotation;

        cam.transform.localRotation = Quaternion.Slerp(cam.transform.localRotation, Quaternion.Euler(-20, 0, 0), .5f * Time.deltaTime);
    }

    IEnumerator AutoReload()
    {
        yield return new WaitForSeconds(1.5f);
        if (currentWeapon.bullets != currentWeapon.maxBullets)
        {
        weaponManager.Reload();
        }
    }

    [Command]
    void CmdPlayerShot(string _PlayerID, int _damage, string _sourceID)
    {
        //Debug.Log(_PlayerID + "has been shot");

        Player _player = GameManager.GetPlayer(_PlayerID);
        _player.RpcTakeDamage(_damage, _sourceID);

    }
}
