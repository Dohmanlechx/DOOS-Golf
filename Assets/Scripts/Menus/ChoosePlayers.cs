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

    // Setters
    public static int GetAmountPlayers() { return amountPlayers; }

    private void OnMouseUp()
    {
        if (is1)
        {
            LetsGo(1);
        }
        if (is2)
        {
            LetsGo(2);
        }
        if (is3)
        {
            LetsGo(3);
        }
        if (is4)
        {
            LetsGo(4);
        }
        if (isBack)
        {
            SceneManager.LoadScene("Main Menu");
        }
    }

    private void LetsGo(int thisAmountPlayers)
    {
        amountPlayers = thisAmountPlayers;
        PlayerPrefs.SetInt("amountPlayers", amountPlayers);
        SceneManager.LoadScene("Course 1");
    }
}
