using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ChoosePlayers : MonoBehaviour
{

    // Public variables
    /*
    public bool is1;
    public bool is2;
    public bool is3;
    public bool is4;
    public bool isBack; */

    public Button m_btn1, m_btn2, m_btn3, m_btn4;

    // Private variables
    private static int amountPlayers;

    // Setter
    public static int GetAmountPlayers() { return amountPlayers; }

    private void Start()
    {
        Button btn1 = m_btn1.GetComponent<Button>();
        Button btn2 = m_btn2.GetComponent<Button>();
        Button btn3 = m_btn3.GetComponent<Button>();
        Button btn4 = m_btn4.GetComponent<Button>();

        btn1.onClick.RemoveAllListeners();
        btn2.onClick.RemoveAllListeners();
        btn3.onClick.RemoveAllListeners();
        btn4.onClick.RemoveAllListeners();

        btn1.onClick.AddListener(OnePlayer);
        btn2.onClick.AddListener(TwoPlayers);
        btn3.onClick.AddListener(ThreePlayers);
        btn4.onClick.AddListener(FourPlayers);
    }

    private void OnePlayer()
    {
        amountPlayers = 1;
        SceneManager.LoadScene("Course 1");
    }

    private void TwoPlayers()
    {
        amountPlayers = 2;
        SceneManager.LoadScene("Course 1");
    }

    private void ThreePlayers()
    {
        amountPlayers = 3;
        SceneManager.LoadScene("Course 1");
    }

    private void FourPlayers()
    {
        amountPlayers = 4;
        SceneManager.LoadScene("Course 1");
    }
}
