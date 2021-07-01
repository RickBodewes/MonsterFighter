using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class endScreenScoreHandler : MonoBehaviour
{
    // this class will output the scores on the endsreen and also calculate the highscore
    public Text score;
    public Text highScore;

    // Start is called before the first frame update
    void Start()
    {
        // outputting the score to screen
        score.text = "You scored: " + PlayerPrefs.GetInt("score", 0).ToString();

        // calculating the highscore
        if (PlayerPrefs.GetInt("score") > PlayerPrefs.GetInt("highScore", 0))
        {
            PlayerPrefs.SetInt("highScore", PlayerPrefs.GetInt("score"));
        }

        // outputting the highscore to screen
        highScore.text = "Highscore: " + PlayerPrefs.GetInt("highScore", 0).ToString();
    }

    // Update is called once per frame
    void Update()
    {
        // when escape is pressed load home menu
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            SceneManager.LoadScene("mainMenu");
        }
    }
}
