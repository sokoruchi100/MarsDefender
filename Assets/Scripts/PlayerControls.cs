using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerControls : MonoBehaviour {

    [Header("Input Actions")]
    [Tooltip("Edit key binding")]
    [SerializeField] private InputAction movement;
    [SerializeField] private InputAction firing;
    
    [Header("Laser Array")]
    [Tooltip("Add laser game objects")]
    [SerializeField] private GameObject[] laserArray;

    [Header("Movement Speed")]
    [Tooltip("Determines how fast the ship moves")]
    [SerializeField] private float moveSpeed;
    
    [Header("Position Range")]
    [Tooltip("Determines range of ship's position")]
    [SerializeField] private float xRange;
    [SerializeField] private float yRange;

    [Header("Position Rotation Factor")]
    [Tooltip("Changes how much you rotate based upon position")]
    [SerializeField] private float positionPitchFactor;
    [SerializeField] private float positionYawFactor;

    [Header("Input Rotation Factor")]
    [Tooltip("Changes rotation when moving")]
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