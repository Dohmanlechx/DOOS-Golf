using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    // Public variables
    public bool isContinue;
    public bool isNewGame;
    public bool isChallenge;
    public bool isQuit;

    // Private variables
    private static int lastCourseIndex;

    private void OnMouseUp()
    {
        if (isContinue)
        {
            SceneManager.LoadScene(PlayerPrefs.GetInt("lastPlayedCourse"));
        }
        if (isNewGame)
        {
            Scores.NewGame();
            SceneManager.LoadScene("Choose Players");
        }
        if (isChallenge)
        {
            SceneManager.LoadScene("Challenge 1");
        }
        if (isQuit)
        {
            Application.Quit();
        }
    }
}
