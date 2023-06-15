using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    private const string TAG_SPAWN_AT_RUNTIME = "SpawnAtRuntime";

    [SerializeField] private GameObject explosionVFX;
    [SerializeField] private GameObject hitVFX;
    [SerializeField] private int scorePerHit;
    [SerializeField] private int scoreOnDeath;
    [SerializeField] private int health;

    private ScoreBoard scoreBoard;
    private Rigidbody rigidbody;
    private GameObject parentGameObject;

    private void Start() {
        scoreBoard = FindObjectOfType<ScoreBoard>();
        parentGameObject = GameObject.FindWithTag(TAG_SPAWN_AT_RUNTIME);
    }

    private void OnParticleCollision(GameObject other) {
        ProcessHit();
        
        if (health <= 0) {
            DestroyEnemy();
        }
    }

    private void ProcessHit() {
        ParticleSystem newHit = Instantiate(hitVFX, transform.position, Quaternion.identity).GetComponent<ParticleSystem>();
        newHit.transform.parent = parentGameObject.transform;

        scoreBoard.IncreaseScore(scorePerHit);

        health--;
    }

    private void DestroyEnemy() {
        ParticleSystem newExplosion = Instantiate(explosionVFX, transform.position, Quaternion.identity).GetComponent<ParticleSystem>();
        newExplosion.transform.parent = parentGameObject.transform;

        scoreBoard.IncreaseScore(scoreOnDeath);
        
        Destroy(gameObject);
    }
}
