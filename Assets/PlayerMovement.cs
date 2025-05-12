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
        
    }

    public void OnMove(InputAction.CallbackContext MoveValue)
    {
        var tempvar = MoveValue.ReadValue<Vector2>();
        forwardInput = tempvar.y;
        turnInput = tempvar.x;
        
    }


    
    public void PlayerMove()
    {
        rb.AddForce(transform.forward * forwardInput * MovementMultiplier);
        rb.AddTorque(0, turnInput * TurnMultiplier, 0);
        rb.AddTorque(-rb.angularVelocity * 0.8f);

    }

}
