using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerControls : MonoBehaviour {

    [SerializeField] private InputAction movement;
    [SerializeField] private InputAction firing;
    [SerializeField] private GameObject[] laserArray;
    [SerializeField] private float moveSpeed;
    [SerializeField] private float xRange;
    [SerializeField] private float yRange;
    [SerializeField] private float positionPitchFactor;
    [SerializeField] private float positionYawFactor;
    [SerializeField] private float inputPitchFactor;
    [SerializeField] private float inputRollFactor;

    private float xThrow;
    private float yThrow;

    private void OnEnable() {
        movement.Enable();
        firing.Enable();
    }

    private void OnDisable() {
        movement.Disable();
        firing.Disable();
    }

    //Handles all Player inputs
    private void Update() {
        ProcessTranslation();
        ProcessRotation();
        ProcessFiring();
    }

    private void ProcessFiring() {
        SetLasersActive(firing.IsPressed());
    }

    private void ProcessRotation() {
        float positionPitch = transform.localPosition.y * positionPitchFactor;
        float inputPitch = yThrow * inputPitchFactor;
        float pitch = positionPitch + inputPitch;

        float yaw = transform.localPosition.x * positionYawFactor;
        
        float roll = xThrow * inputRollFactor;

        transform.localRotation = Quaternion.Euler(pitch, yaw, roll);
    }

    private void ProcessTranslation() {
        xThrow = movement.ReadValue<Vector2>().x;
        yThrow = movement.ReadValue<Vector2>().y;

        float xOffset = xThrow * moveSpeed * Time.deltaTime;
        float rawXPos = transform.localPosition.x + xOffset;
        float clampedXPos = Mathf.Clamp(rawXPos, -xRange, xRange);

        float yOffset = yThrow * moveSpeed * Time.deltaTime;
        float rawYPos = transform.localPosition.y + yOffset;
        float clampedYPos = Mathf.Clamp(rawYPos, -yRange, yRange);

        transform.localPosition = new Vector3
            (clampedXPos,
            clampedYPos,
            transform.localPosition.z);
    }

    private void SetLasersActive(bool isActive) {
        foreach (GameObject laser in laserArray) {
            ParticleSystem.EmissionModule em = laser.GetComponent<ParticleSystem>().emission;
            em.enabled = isActive;
        }
    }
}