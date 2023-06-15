using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ScoreBoard : MonoBehaviour
{
    private int score;
    private TMP_Text scoreText;

    private void Start() {
        scoreText = GetComponent<TMP_Text>();
        scoreText.text = "START";
    }

    public void IncreaseScore(int amountToIncrease) {
        score += amountToIncrease;
        scoreText.text = score.ToString();
    }
}
