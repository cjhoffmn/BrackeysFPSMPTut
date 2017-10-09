using UnityEngine;
using System.Collections;

[RequireComponent(typeof(Rigidbody))]

public class PlayerMotor : MonoBehaviour
{
    [SerializeField]
    private Camera cam;
    private Vector3 velocity = Vector3.zero;
    private Rigidbody rb;
    //private Vector3 rotation = Vector3.zero;
    private Vector3 rotation = Vector3.zero;
    private float cameraRotationX = 0f;
    private float currentCameraRotationX = 0f;
    private Vector3 thrusterForce = Vector3.zero;

    [SerializeField]
    private float cameraRotationLimit = 85f;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    // Receives movement vector from Contoller
    public void Move(Vector3 _velocity)
    {
        velocity = _velocity;
    }

    // Receives roration vector from Contoller
    public void Rotate(Vector3 _rotation)
    {
        rotation = _rotation;
    }
    // Receives Camera Rotation vector from Contoller

    public void RotateCamera(float _camerarotationX)
    {
        cameraRotationX = _camerarotationX;
    }


    // Get a force vector
    public void ApplyThruster (Vector3 _thrusterForce)
    {
        thrusterForce = _thrusterForce;
        
    }

    //runs every physics movement time
    void FixedUpdate()
    {
        PerformMovement();
        PerformRotation();
    }

    void PerformMovement()
    {
        if (velocity != Vector3.zero)
        {
            rb.MovePosition(rb.position + velocity * Time.fixedDeltaTime);
        }
        if (thrusterForce != Vector3.zero)
        {
            rb.AddForce(thrusterForce * Time.fixedDeltaTime, ForceMode.Acceleration);
        }
    }
    
    void PerformRotation()
    {
        rb.MoveRotation(rb.rotation * Quaternion.Euler(rotation));
        if (cam != null)
        {
            //Replaced: cam.transform.Rotate(-camerarotation);

            // Set Rotation and Clamp it
            currentCameraRotationX -= cameraRotationX;
            currentCameraRotationX = Mathf.Clamp(currentCameraRotationX, -cameraRotationLimit, cameraRotationLimit);

            // Apply rotation Directly
            cam.transform.localEulerAngles = new Vector3(currentCameraRotationX, 0f, 0f); 
        }

    }
}
