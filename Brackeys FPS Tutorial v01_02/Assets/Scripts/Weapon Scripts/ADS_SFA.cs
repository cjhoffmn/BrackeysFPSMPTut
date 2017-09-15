using System.Collections;
using UnityEngine;
using UnityEngine.Networking;

public class ADS_SFA : NetworkBehaviour
{
    public Vector3 ADSPosSFA, ADSRotSFA;
    private Vector3 HFPosSFA, HFScaleSFA, ADSScaleSFA;
    private Quaternion HFRotSFA;
    public float ADSRate, ADSRetRate, ADSFOV, HFScaleFactor;
    private float HFFOV, ADSTurnSpeedFactor;
    public Camera mainCam;
    public Animator animADS;
    public WeaponManager weaponManager;
    private PlayerController playerController;
    public bool isADS;
    private float origTurnSpeed;

    private void Start()
    {
        HFScaleSFA = transform.localScale;
        HFPosSFA = transform.localPosition;
        HFRotSFA = transform.localRotation;
        ADSScaleSFA = HFScaleSFA * HFScaleFactor;
        ADSRotSFA = new Vector3(ADSRotSFA.x, ADSRotSFA.y, ADSRotSFA.z);
        ADSTurnSpeedFactor = .35f;
        playerController = transform.parent.transform.parent.GetComponent<PlayerController>();
        origTurnSpeed = playerController.turnspeed;
        mainCam = gameObject.transform.parent.GetComponent<Camera>();
        weaponManager = transform.parent.transform.parent.GetComponent<WeaponManager>();
        animADS = weaponManager.GetCurrentGraphics().GetComponent<Animator>();
        HFFOV = mainCam.fieldOfView;
    }

    void Update()
    {

        if (Input.GetButton("Fire2"))
        {
            if (weaponManager.isClippedOut == true)
            {
                //animADS.enabled = true;
                mainCam.fieldOfView = Mathf.Lerp(mainCam.fieldOfView, HFFOV, ADSRetRate * Time.deltaTime);
                animADS.SetBool("isClippedOut", true);
                return;
            }

            AimDownSights();
        }
        else
        {
            if (transform.localPosition == HFPosSFA)
            {
                return;
            }

            ReturntoHF();
        }
    }
    private void AimDownSights()
    {
        if (transform.localPosition == ADSPosSFA)
        {
            return;
        }
            transform.localPosition = Vector3.Slerp(transform.localPosition, ADSPosSFA, ADSRate * Time.deltaTime);
            transform.localScale = Vector3.Slerp(transform.localScale, ADSScaleSFA, ADSRate * Time.deltaTime);
            transform.localRotation = Quaternion.Euler(ADSRotSFA);
            mainCam.fieldOfView = Mathf.Lerp(mainCam.fieldOfView, ADSFOV, ADSRate * Time.deltaTime);
            playerController.turnspeed = origTurnSpeed * ADSTurnSpeedFactor;
    }
    

    void ReturntoHF()
    {
        if(transform.localPosition == HFPosSFA)
            return;

        transform.localPosition = Vector3.Slerp(transform.localPosition, HFPosSFA, ADSRetRate * Time.deltaTime);
        transform.localScale = Vector3.Slerp(transform.localScale, HFScaleSFA, ADSRetRate * Time.deltaTime);
        transform.localRotation = HFRotSFA;
        mainCam.fieldOfView = Mathf.Lerp(mainCam.fieldOfView, HFFOV, ADSRetRate * Time.deltaTime);
        playerController.turnspeed = origTurnSpeed;
    }
}
