using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
// using UnityEngine.InputSystem;

public class PlayerControls : MonoBehaviour
{
    // new input system
    // [SerializeField] InputAction movement;
    // [SerializeField] InputAction fire;
    [Header("General Setup Settings")]
    [Tooltip("How fast ship moves up and down based upon player input")]
    [SerializeField] float controlSpeed = 30f;
    [Tooltip("How far player moves horizontally")][SerializeField] float xRange = 10f;
    [Tooltip("How far player moves vertically")][SerializeField] float yRange = 7f;

    // array of GameObject, lasers inserted
    [Header("Laser gun array")]
    [Tooltip("Add all player lasers here")]
    [SerializeField] GameObject[] lasers;

    [Header("Screen position based tuning")]
    [SerializeField] float positionPitchFactor = -2f;
    [SerializeField] float positionYawFactor = 2f;

    [Header("Player input based tuning")]
    [SerializeField] float controlPitchFactor = -15f;
    [SerializeField] float controlRollFactor = -20f;

    float xThrow, yThrow;

    // void OnEnable()
    // {
    //     // enable new Input System
    //     movement.Enable();
    //     fire.Enable();
    // }

    // void OnDisable()
    // {
    //     // also you need to disable new system also
    //     movement.Disable();
    //     fire.Disable();
    // }

    void Update()
    {
        ProcessTranslation();
        ProcessRotation();
        ProcessFiring();
    }

    void ProcessRotation()
    {
        // it should be localPosition not localRotation
        float pitchDueToPosition = transform.localPosition.y * positionPitchFactor;
        float pitchDueToControl = yThrow * controlPitchFactor;

        float pitch = pitchDueToPosition + pitchDueToControl;
        float yaw = transform.localPosition.x * positionYawFactor;

        float roll = xThrow * controlRollFactor;

        // rotation is hard to configure, because if you rotate on x axis other axis also change but when you try to change y it is not changing
        // here helps Quaternion it helps with rotation
        // Euler - return rotation
        transform.localRotation = Quaternion.Euler(pitch, yaw, roll);
    }

    void ProcessTranslation()
    {
        // float horizontalThrow = movement.ReadValue<Vector2>().x;
        // float verticalThrow = movement.ReadValue<Vector2>().y;

        // we have created these variables in this functions so Encapsulation we cannot get it
        // float xThrow = Input.GetAxis("Horizontal");
        // float yThrow = Input.GetAxis("Vertical");
        xThrow = Input.GetAxis("Horizontal");
        yThrow = Input.GetAxis("Vertical");

        float xOffset = xThrow * controlSpeed * Time.deltaTime;
        float rawXPos = transform.localPosition.x + xOffset;

        float clampedXPos = Mathf.Clamp(rawXPos, -xRange, xRange);

        float yOffset = yThrow * controlSpeed * Time.deltaTime;
        float rawYPos = transform.localPosition.y + yOffset;

        //Mathf.Clamp helps us to make a fixed field for camera
        float clampedYPos = Mathf.Clamp(rawYPos, -yRange, yRange);

        // move ship
        transform.localPosition = new Vector3(clampedXPos, clampedYPos, transform.localPosition.z);
    }

    void ProcessFiring()
    {
        // for new input system
        // if(fire.ReadValue<float>()>0.5){}
        if (Input.GetButton("Fire1"))
        {
            SetLasersBeamsActive(true);
        }
        else
        {
            SetLasersBeamsActive(false);
        }

    }

    void SetLasersBeamsActive(bool isEnabled)
    {
        foreach (GameObject laser in lasers)
        {
            ParticleSystem.EmissionModule emissionModule = laser.GetComponent<ParticleSystem>().emission;
            emissionModule.enabled = isEnabled;
        }
    }
}
