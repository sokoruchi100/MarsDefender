using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicPlayer : MonoBehaviour
{
    private void Awake() {
        int amountOfMusicPlayers = FindObjectsOfType<MusicPlayer>().Length;
        if (amountOfMusicPlayers > 1) {
            Destroy(gameObject);
        } else {
            DontDestroyOnLoad(gameObject);
        }
    }
}
