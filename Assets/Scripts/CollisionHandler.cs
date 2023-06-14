using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CollisionHandler : MonoBehaviour
{
    private const string RELOAD_SCENE_FUNCTION = "ReloadScene";
    [SerializeField] private float loadDelay;
    [SerializeField] private ParticleSystem explosionVFX;

    private void OnTriggerEnter(Collider other) {
        StartDeathSequence();
    }

    private void StartDeathSequence() {
        explosionVFX.Play();
        GetComponent<MeshRenderer>().enabled = false;
        GetComponent<BoxCollider>().enabled = false;
        GetComponent<PlayerControls>().enabled = false;
        Invoke(RELOAD_SCENE_FUNCTION, loadDelay);
    }

    private void ReloadScene() {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
