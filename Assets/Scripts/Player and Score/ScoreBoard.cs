using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ScoreBoard : MonoBehaviour
{
    //private static bool created = false;
    private static int lastCourseIndex;

    /*
    private void Awake()
    {
        DontDestroyOnLoad(gameObject);
        created = true;
        Debug.Log("Awake:" + gameObject);
    } */

    public List<TextMeshPro> player1Holes = new List<TextMeshPro>();
    public List<TextMeshPro> player2Holes = new List<TextMeshPro>();
    public List<TextMeshPro> player3Holes = new List<TextMeshPro>();
    public List<TextMeshPro> player4Holes = new List<TextMeshPro>();
    //public PlayerHandler playerHandler;
    //private int amountPlayers;

    public bool isContinue;

    private void Start()
    {
        //amountPlayers = playerHandler.getPlayerAmount();
    }

    /*
    public void SetScore(int course, int score)
    {
        TextMeshPro test = player1Holes[course];

        test.SetText(score.ToString());

    }
    */

    public void SetLastCourseIndex(int index)
    {
        lastCourseIndex = index;
    }

    private void OnMouseUp()
    {
        if (isContinue)
        {
            SceneManager.LoadScene(lastCourseIndex);
        }
    }


}
