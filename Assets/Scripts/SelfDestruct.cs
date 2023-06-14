using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelfDestruct : MonoBehaviour
{
    [SerializeField] private float timeTillDestroy;
    private void Start() {
        Destroy(gameObject, timeTillDestroy);
    }
}
