using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    //setting values and rigidbody to the car 
    [SerializeField] Rigidbody rb;
    float forwardInput;
    float turnInput;
    public float MovementMultiplier = 1000f;
    public float TurnMultiplier = 3f;
    public float DownforceMultiplier = 10f;



    //fixed update so the movement and keeping the car upright more consistently 
    private void FixedUpdate()
    {
        PlayerMove();
        KeepCarUpright();
    }

    void KeepCarUpright()
    {
        Vector3 currentRotation = rb.rotation.eulerAngles;


        Quaternion targetRotation = Quaternion.Euler(0, currentRotation.y, 0);


        rb.MoveRotation(Quaternion.Slerp(rb.rotation, targetRotation, Time.fixedDeltaTime * 5f));
    }


    public void OnMove(InputAction.CallbackContext MoveValue)
    {
        //moves the car along a vector 2 x,y way not Z
        var tempvar = MoveValue.ReadValue<Vector2>();
        forwardInput = tempvar.y;
        turnInput = tempvar.x;

    }



    public void PlayerMove()
    {

        // adds forward movement
        rb.AddForce(transform.forward * forwardInput * MovementMultiplier, ForceMode.Acceleration);

        //chatgpt to do something with lateral velocity
        Vector3 forwardVel = Vector3.Dot(rb.velocity, transform.forward) * transform.forward;
        Vector3 lateralVel = Vector3.Dot(rb.velocity, transform.right) * transform.right;
        rb.velocity = forwardVel + lateralVel * 0.2f;

        // Apply turning based on speed
        float speedFactor = Mathf.Clamp01(rb.velocity.magnitude / 10f);
        float turnAmount = turnInput * TurnMultiplier * speedFactor;
        Quaternion deltaRot = Quaternion.Euler(0f, turnAmount, 0f);
        rb.MoveRotation(rb.rotation * deltaRot);

        // Add downforce to simulate grip
        rb.AddForce(-transform.up * rb.velocity.magnitude * DownforceMultiplier);

        //Caps the speed of the car
        var tempVelocityX = Mathf.Clamp(rb.angularVelocity.x, -4, 4);
        var tempVelocityY = Mathf.Clamp(rb.angularVelocity.y, -4, 4);
        var tempVelocityZ = Mathf.Clamp(rb.angularVelocity.z, -4, 4);
        rb.angularVelocity = new Vector3(tempVelocityX, tempVelocityY, tempVelocityZ);
    }

}