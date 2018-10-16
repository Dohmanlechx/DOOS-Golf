using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ScoreBoard : MonoBehaviour
{
    public Scores scores;

    //private static bool created = false;
    private static int lastCourseIndex;

    /*
    private void Awake()
    {
        DontDestroyOnLoad(gameObject);
        created = true;
        Debug.Log("Awake:" + gameObject);
    } */

    public List<TextMeshPro> player1TextMeshs = new List<TextMeshPro>();
    /*
    public List<TextMeshPro> player2Holes = new List<TextMeshPro>();
    public List<TextMeshPro> player3Holes = new List<TextMeshPro>();
    public List<TextMeshPro> player4Holes = new List<TextMeshPro>();
    */
    //public PlayerHandler playerHandler;
    //private int amountPlayers;

    public bool isContinue;

    private void Start()
    {
        scores = FindObjectOfType<Scores>();
        UpdateScoreBoard();
        //amountPlayers = playerHandler.getPlayerAmount();
    }

    /*
    public void SetScore(int course, int score)
    {
        TextMeshPro test = player1Holes[course];

        test.SetText(score.ToString());

    }
    */

    private void UpdateScoreBoard()
    {
        if (scores != null)
        {
            for (int i = 0; i <= player1TextMeshs.Count -1; i++)
            {
                int[] myScores = scores.GetScores();
                player1TextMeshs[i].SetText(myScores[i].ToString());
            }

            player1TextMeshs[19].SetText(scores.GetTotalShotsCount().ToString());
        }
    }

    public static void SetLastCourseIndex(int index)
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
