using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] private GameObject explosionVFX;
    [SerializeField] private Transform parent;
    [SerializeField] private int score;

    private ScoreBoard scoreBoard;

    private void Start() {
        scoreBoard = FindObjectOfType<ScoreBoard>();
    }

    private void OnParticleCollision(GameObject other) {
        ProcessHit();
        DestroyEnemy();
    }

    private void ProcessHit() {
        scoreBoard.IncreaseScore(score);
    }

    private void DestroyEnemy() {
        ParticleSystem newExplosion = Instantiate(explosionVFX, transform.position, Quaternion.identity).GetComponent<ParticleSystem>();
        newExplosion.transform.parent = parent;
        newExplosion.Play();
        Destroy(gameObject);
    }
}
