using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ChoosePlayers : MonoBehaviour
{

    // Public variables

    public bool is1;
    public bool is2;
    public bool is3;
    public bool is4;
    public bool isBack;

    // Private variables
    private static int amountPlayers;

    // Setter
    public static int GetAmountPlayers() { return amountPlayers; }

    private void OnMouseUp()
    {
        if (is1)
        {
            amountPlayers = 1;
            PlayerPrefs.SetInt("amountPlayers", 1);
            SceneManager.LoadScene("Course 1");
        }
        if (is2)
        {
            amountPlayers = 2;
            PlayerPrefs.SetInt("amountPlayers", 2);
            SceneManager.LoadScene("Course 1");
        }
        if (is3)
        {
            amountPlayers = 3;
            PlayerPrefs.SetInt("amountPlayers", 3);
            SceneManager.LoadScene("Course 1");
        }
        if (is4)
        {
            amountPlayers = 4;
            PlayerPrefs.SetInt("amountPlayers", 4);
            SceneManager.LoadScene("Course 1");
        }
        if (isBack)
        {
            SceneManager.LoadScene("Main Menu");
        }
    }
}
