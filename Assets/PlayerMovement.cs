using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{

    [SerializeField] Rigidbody rb;
    float forwardInput;
    float turnInput;
    [SerializeField] float MovementMultiplier;
    [SerializeField] float TurnMultiplier;



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
        var tempvar = MoveValue.ReadValue<Vector2>();
        forwardInput = tempvar.y;
        turnInput = tempvar.x;
        
    }


    
    public void PlayerMove()
    {
        //allows the car to move forwards
        rb.AddForce(transform.forward * forwardInput * MovementMultiplier);

        //allows the car to turn and have a drift like feel to it
        rb.AddTorque(0, turnInput * TurnMultiplier, 0);
        rb.AddTorque(-rb.angularVelocity * 0.8f);

        //Caps the speed of the car
        var tempVelocityX = Mathf.Clamp(rb.angularVelocity.x, -4, 4);
        var tempVelocityY = Mathf.Clamp(rb.angularVelocity.y, -4, 4);
        var tempVelocityZ = Mathf.Clamp(rb.angularVelocity.z, -4, 4);
        rb.angularVelocity = new Vector3(tempVelocityX, tempVelocityY, tempVelocityZ);
    }

}
