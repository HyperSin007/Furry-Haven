using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Tpcontroller : MonoBehaviour
{
    //inputs
    private PlayerMovementTP playerActions;
    private InputAction move;

    //movement fields
    private Rigidbody rb;
    [SerializeField]
    private float movementForce = 3f;
    [SerializeField]
    private float jumpForce = 5f;
    [SerializeField]
    private float maxSpeed = 3f;
    private Vector3 forceDetection = Vector3.zero;

    [SerializeField]
    private Camera playerCamera;
    //private Vector3 forceDirection;

    // Start is called before the first frame update
    private void Awake()
    {
        rb = this.GetComponent<Rigidbody>();
        playerActions = new PlayerMovementTP();
    }
    private void OnEnable()
    {
        playerActions.Player.Jump.started += DoJump;
        move = playerActions.Player.Move;
        playerActions.Player.Enable();
    }
    private void OnDisable()
    {
        playerActions.Player.Jump.started -= DoJump;
    }
 

    private void FixedUpdate()
    {
        forceDetection += move.ReadValue<Vector2>().x * GetCameraRight(playerCamera) * movementForce;
        forceDetection += move.ReadValue<Vector2>().y * GetCameraForward(playerCamera) * movementForce;

        rb.AddForce(forceDetection, ForceMode.Impulse);
        forceDetection = Vector3.zero;

     //   if(rb.velocity.y > 0f)
     //
       //     rb.velocity += Vector3.down * Physics.gravity.y * Time.fixedDeltaTime;
     //   Vector3 horizontalVelocity = rb.velocity;
     //   horizontalVelocity.y = 0;
     //   if(horizontalVelocity.sqrMagnitude > maxSpeed * maxSpeed)
      //      rb.velocity = horizontalVelocity.normalized * maxSpeed + Vector3.up * rb.velocity.y;

        LookAt();
    }

    private void LookAt()
    {
        Vector3 direction = rb.velocity;
        direction.y = 0f;

        if (move.ReadValue<Vector2>().sqrMagnitude > 0.1f && direction.sqrMagnitude > 0.1f)
            this.rb.rotation = Quaternion.LookRotation(direction, Vector3.up);
        else
            rb.angularVelocity = Vector3.zero;
    }

    private Vector3 GetCameraForward(Camera playerCamera)
    {
        Vector3 cameraForward = playerCamera.transform.forward;
        cameraForward.y = 0;
        return cameraForward.normalized;
    }

    private Vector3 GetCameraRight(Camera playerCamera)
    {
        Vector3 cameraRight = playerCamera.transform.right;
        cameraRight.y = 0;
        return cameraRight.normalized;
    }

    private void DoJump(InputAction.CallbackContext obj)
    {
        if(IsGrounded())
        {
            forceDetection += Vector3.up * jumpForce;
        }
    }
    private bool IsGrounded()
    {
        Ray ray = new Ray(this.transform.position + Vector3.up * 0.25f, Vector3.down);
        if(Physics.Raycast(ray, out RaycastHit hit, 0.3f))
            return true;
        else
            return false;
       
    }

    
}
