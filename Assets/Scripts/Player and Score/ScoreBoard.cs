using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ScoreBoard : MonoBehaviour
{
    // Cached references
    public GameSystem gameSystem;
    public Scores scores;

    // Public variables
    public List<TextMeshPro> player1TextMeshs = new List<TextMeshPro>();
    public bool isContinue;

    // Private variables
    private static int lastCourseIndex;

    /*
    public List<TextMeshPro> player2Holes = new List<TextMeshPro>();
    public List<TextMeshPro> player3Holes = new List<TextMeshPro>();
    public List<TextMeshPro> player4Holes = new List<TextMeshPro>();
    
    public PlayerHandler playerHandler;
    private int amountPlayers;
    */

    private void Start()
    {
        gameSystem = FindObjectOfType<GameSystem>();
        scores = FindObjectOfType<Scores>();
        UpdateScoreBoard();
        //amountPlayers = playerHandler.getPlayerAmount();
    }

    private void UpdateScoreBoard()
    {
        if (scores != null)
        {
            for (int i = 1; i <= player1TextMeshs.Count - 1; i++)
            {
                int[] myScores = scores.GetScores();
                if (myScores[i] == 0)
                {
                    player1TextMeshs[i].SetText(""); // Unplayed courses
                }
                else
                {
                    player1TextMeshs[i].SetText(myScores[i].ToString()); // Played courses
                }
            }

            // Telling to players that those courses don't exist yet (warning for hard-coding)
            for (int j = 6; j <= 18; j++)
            {
                player1TextMeshs[j].SetText("x");
            }

            player1TextMeshs[19].SetText(scores.GetTotalShotsCount().ToString()); // Updating total shots in scoreboard
        }
    }

    // Access from ButtonScript.cs, needed to recall the course's index
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
