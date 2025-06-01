using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem; 
using UnityEngine.UI;

public class NitroBoost : MonoBehaviour
{

    public InputActionReference boostAction; 


    public float normalSpeed = 10f;
    public float boostMultiplier = 2f;
    public float maxNitro = 5f;
    public float nitroRegenCooldown = 5f;
    public Slider nitroBar;

    private float currentNitro;
    private float cooldownTimer;
    private bool isBoosting;
    private bool isCooldown;
    private Rigidbody rb;

    private void OnEnable()
    {
        boostAction.action.Enable();
    }

    private void OnDisable()
    {
        boostAction.action.Disable();
    }


    void Start()
    {
        rb = GetComponent<Rigidbody>();
        currentNitro = maxNitro;
        nitroBar.maxValue = maxNitro;
        nitroBar.value = maxNitro;
    }

    void Update()
    {
        HandleInput();
        HandleCooldown();
        UpdateUI();
    }

    void FixedUpdate()
    {
        float speed = isBoosting ? normalSpeed * boostMultiplier : normalSpeed;
        rb.velocity = transform.forward * speed;
    }

    void HandleInput()
    {
        if (boostAction.action.ReadValue<float>() > 0.5f && currentNitro > 0 && !isCooldown)
        {
            isBoosting = true;
            currentNitro -= Time.deltaTime;

            if (currentNitro <= 0)
            {
                currentNitro = 0;
                StartCooldown();
            }
        }
        else
        {
            if (isBoosting)
            {
                StartCooldown();
            }

            isBoosting = false;
        }

    }


    void StartCooldown()
    {
        isCooldown = true;
        cooldownTimer = nitroRegenCooldown;
    }

    void HandleCooldown()
    {
        if (isCooldown)
        {
            cooldownTimer -= Time.deltaTime;
            if (cooldownTimer <= 0f)
            {
                isCooldown = false;
                currentNitro = maxNitro;
            }
        }
    }

    void UpdateUI()
    {
        if (nitroBar != null)
        {
            nitroBar.value = currentNitro;
        }
    }
}



