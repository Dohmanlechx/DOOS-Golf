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

    //TODO: Gör om siffrorna för scenerna till ett index så att man senare kan ladda in sin senaste bana.
    private void OnMouseUp()
    {
        if (isContinue){
            SceneManager.LoadScene("Daaaavid, here continue should be yes?");
        }
        if (isChallenge){
            SceneManager.LoadScene("Challenge 1");
        }
        if (isQuit){
            Application.Quit();
        }
        if (isNewGame){
            SceneManager.LoadScene("ChoosePlayers");
        }
    }
    // Update is called once per frame
    private void Update()
    {

    }
}
