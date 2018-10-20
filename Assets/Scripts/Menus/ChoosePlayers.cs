using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ChoosePlayers : MonoBehaviour
{

    // Public variables
    public bool is1;
    public bool is2;
    public bool is3;
    public bool is4;
    public bool isBack;
    int amountPlayers;

    //TODO: Gör om siffrorna för scenerna till ett index så att man senare kan ladda in sin senaste bana.
    private void OnMouseUp()
    {
        if (is1)
        {
            amountPlayers = 1;
            SceneManager.LoadScene("Course 1");
        }
        if (is2)
        {
            amountPlayers = 2;
            SceneManager.LoadScene("Course 1");
        }
        if (is3)
        {
            amountPlayers = 3;
            SceneManager.LoadScene("Course 1");
        }
        if (is4)
        {
            amountPlayers = 4;
            SceneManager.LoadScene("Course 1");
        }
        if (isBack)
        {
            SceneManager.LoadScene("Main Menu");
        }
    }
}
