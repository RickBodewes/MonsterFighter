using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class scoreManager : MonoBehaviour
{
    // this class will keep track of the players current score
    public Text scoreTracker;
    private int score;

    // Start is called before the first frame update
    void Start()
    {
        score = 0;
        PlayerPrefs.SetInt("score", score);
    }

    // When score is increased also update it in player prefs
    public void scoreIncrease(int amount)
    {
        score += amount;
        PlayerPrefs.SetInt("score", score);

        // also update the score on screen
        scoreTracker.text = "Score: " + PlayerPrefs.GetInt("score", 0).ToString();
    }
}
