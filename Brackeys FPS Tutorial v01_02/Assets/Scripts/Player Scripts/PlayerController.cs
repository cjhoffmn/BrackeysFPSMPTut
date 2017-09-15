using System;
using UnityEngine;


[RequireComponent(typeof(Animator))]
[RequireComponent(typeof(PlayerMotor))]
[RequireComponent(typeof(ConfigurableJoint))]

public class PlayerController : MonoBehaviour
{
    // Variables
    [SerializeField]
    private float movespeed = 6f;
    [SerializeField]
    public float turnspeed = 5f;
    [SerializeField]
    private float thrusterForce = 1500f;
    [SerializeField]
    private float thrusterFuelBurnRate = 1.5f;
    [SerializeField]
    private float thrusterFuelRegen = 0.3f;

    private float thrusterFuelAmount = 1f;

    [SerializeField]
    private float boosterMultiple = 2f;

    public float GetThrusterFuelAmount()
    {
        return thrusterFuelAmount;
    }

    [SerializeField]
    LayerMask environmentMask;

    [Header("Config Joint (Spring) Options:")]

    [SerializeField]
    private float jointSpringAmnt = 20f;
    [SerializeField]
    private float jointMaxForceAmtAmt = 40f;
    [SerializeField]
    float RayDownOffset = 0f;

    // References (Component Caching)
    private PlayerMotor motor;
    private ConfigurableJoint configjoint;
    private Animator animator;
    //For mouselock
    private bool showingMouse = true;

    private void Start()
    {
        motor = GetComponent<PlayerMotor>();
        configjoint = GetComponent<ConfigurableJoint>();
        SetJointSettings(jointSpringAmnt);
        animator = GetComponent<Animator>();

        // Create basic mouselock:
        showingMouse = false;
        //Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
    }

    private void Update()
    {
        if (PauseMenu.isOn == true)
        {
            motor.Move(Vector3.zero);
            motor.Rotate(Vector3.zero);
            motor.RotateCamera(0f);
            motor.ApplyThruster(Vector3.zero);
            if (Cursor.lockState != CursorLockMode.None)
            {
                Cursor.lockState = CursorLockMode.None;
            }
            return;
        }

        if (Cursor.lockState != CursorLockMode.Locked)
        {
            Cursor.lockState = CursorLockMode.Locked;
        }

        // Setting Target position for Spring for correct
        // Physics action when flying over objects.
        RaycastHit _hit;
        bool _hitTrue;

        //Debug.DrawRay(transform.position, Vector3.down, Color.red, 3f);
        //Debug.DrawRay(_hit.transform.position, Vector3.forward, Color.blue, 3f);

        Vector3 adjustedDownVect = transform.position + new Vector3(0f, RayDownOffset);
        //Debug.Log("Act "+  transform.position.x +"," + transform.position.y + "," + transform.position.z);
        //Debug.Log("Adj "+adjustedDownVect.x + "," + adjustedDownVect.y + "," + adjustedDownVect.z);

        _hitTrue = Physics.Raycast(adjustedDownVect, Vector3.down, out _hit, 100f, environmentMask);
        if (_hitTrue)
        {
            //Debug.DrawRay(adjustedDownVect, Vector3.down, Color.red, 3f);
            //Debug.DrawRay(_hit.transform.position, Vector3.forward, Color.blue, 3f);
            //Debug.Log(_hit.transform.name+" @ "+_hit.transform.position);

            configjoint.targetPosition = new Vector3(0f, -_hit.point.y, 0f);
        }
        else
        {
            configjoint.targetPosition = new Vector3(0f, 0f, 0f);
        }


        //calculate movement velocity as 3d vector
        float _xMov = Input.GetAxis("Horizontal");
        float _zMov = Input.GetAxis("Vertical");

        //Speeds up movemnent if boosters applied
        float _movespeedwboost = movespeed;

        // Create basic Mouslock
        if (Input.GetKeyDown(KeyCode.L) && showingMouse == false)
        {
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
            showingMouse = true;
        }
        else if (Input.GetKeyDown(KeyCode.L) && showingMouse == true)
        {
            Cursor.lockState = CursorLockMode.Locked;
            //Cursor.visible = false;
            showingMouse = false;
        }


        if (Input.GetKey(KeyCode.LeftShift) || Input.GetKey(KeyCode.RightShift))
        {
            _movespeedwboost = movespeed * boosterMultiple;

        }

        Vector3 _movehorizontal = transform.right * _xMov;
        Vector3 _movevertical = transform.forward * _zMov;

        //Final movement vector
        Vector3 _velocity = (_movehorizontal + _movevertical) * _movespeedwboost;

        // Apply Movement
        motor.Move(_velocity);
        //_movespeedwboost = movespeed;

        //Animating Movement
        animator.SetFloat("ForwardVelocity", _zMov);

        // Calculate rotation as 3d vector for turning player - not camera look
        float _yRot = Input.GetAxisRaw("Mouse X");
        Vector3 _rotation = new Vector3(0, _yRot, 0) * turnspeed;

        // Apply movement rotation
        motor.Rotate(_rotation);

        // Calculate Camera Rotation
        float _xRot = Input.GetAxisRaw("Mouse Y");
        //replaced:  Vector3 _cameraRotation = new Vector3(_xRot, 0, 0) * turnspeed;
        float _cameraRotationX = _xRot * turnspeed;
        // Apply camera rotation
        motor.RotateCamera(_cameraRotationX);

        // Define Thruster Force   
        Vector3 _thrusterForce = Vector3.zero;



        // Regular Jet Thrust
        if (Input.GetButton("Jump") && thrusterFuelAmount > 0f)
        {
            thrusterFuelAmount -= thrusterFuelBurnRate * Time.deltaTime;

            if (thrusterFuelAmount >= 0.025f)
            {
                _thrusterForce = Vector3.up * thrusterForce;
                SetJointSettings(0f);
            }
        }
        else
        {
            thrusterFuelAmount += thrusterFuelRegen * Time.deltaTime;
            SetJointSettings(jointSpringAmnt);
        }

        thrusterFuelAmount = Mathf.Clamp(thrusterFuelAmount, 0f, 1f);

        //determine if this was an initial press of key.

    
        // Apply the thruster force
        motor.ApplyThruster(_thrusterForce);
    }

    //Method that sets the Joint Settings
    private void SetJointSettings(float _jointSpringAmnt)
    {
        // Creates the Struct of a conifgJoint
        configjoint.yDrive = new JointDrive
        {
            positionSpring = _jointSpringAmnt,
            maximumForce = jointMaxForceAmtAmt
        };
    }


}
