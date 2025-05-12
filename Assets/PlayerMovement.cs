using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{

    [SerializeField] Rigidbody rb;
    Vector3 moveInput;
    [SerializeField] float MovementMultiplier;



    private void FixedUpdate()
    {
        PlayerMove();
        
    }

    public void OnMove(InputAction.CallbackContext MoveValue)
    {
        var tempvar = MoveValue.ReadValue<Vector2>();
        moveInput = new Vector3(tempvar.x, 0, tempvar.y);
        
    }

    public void PlayerMove()
    {
        rb.AddForce(moveInput * MovementMultiplier);
    }
}
