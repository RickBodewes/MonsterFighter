using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class mainMenu : MonoBehaviour
{
    //  Fired when the play button is pressed
    public void Play()
    {
        SceneManager.LoadScene("play");
    }
}
