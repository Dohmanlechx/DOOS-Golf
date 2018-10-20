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
    public List<TextMeshPro> player2TextMeshs = new List<TextMeshPro>();
    public List<TextMeshPro> player3TextMeshs = new List<TextMeshPro>();
    public List<TextMeshPro> player4TextMeshs = new List<TextMeshPro>();
    public ArrayList listOfAllPlayers = new ArrayList();
    public bool isContinue;

    // Private variables
    private static int lastCourseIndex;
    private int amountPlayers;

    private void Start()
    {
        gameSystem = FindObjectOfType<GameSystem>();
        scores = FindObjectOfType<Scores>();
        amountPlayers = ChoosePlayers.GetAmountPlayers();

        listOfAllPlayers.Add(player1TextMeshs);
        listOfAllPlayers.Add(player2TextMeshs);
        listOfAllPlayers.Add(player3TextMeshs);
        listOfAllPlayers.Add(player4TextMeshs);

        foreach (List<TextMeshPro> playerTextMeshs in listOfAllPlayers)
        {
            UpdateScoreBoard(playerTextMeshs);
        }
    }

    private void UpdateScoreBoard(List<TextMeshPro> thisPlayer)
    {
        if (scores != null)
        {
            /*
            List<TextMeshPro> thisPlayer = new List<TextMeshPro>();
            for (int i = 1; i <= amountPlayers; i++)
            {
                switch (amountPlayers)
                {
                    case 1:
                        thisPlayer = player1TextMeshs;
                        break;
                    case 2:
                        thisPlayer = player2TextMeshs;
                        break;
                    case 3:
                        thisPlayer = player3TextMeshs;
                        break;
                    case 4:
                        thisPlayer = player4TextMeshs;
                        break;
                }
                */

                for (int j = 1; j <= thisPlayer.Count - 1; j++)
                {
                    int[] myScores = scores.GetScores();
                    if (myScores[j] == 0)
                    {
                        thisPlayer[j].SetText(""); // Unplayed courses
                    }
                    else
                    {
                        thisPlayer[j].SetText(myScores[j].ToString()); // Played courses
                    }
                }

                // Telling to players that those courses don't exist yet (warning for hard-coding)
                for (int k = 6; k <= 18; k++)
                {
                    thisPlayer[k].SetText("x");
                }
            }

            thisPlayer[19].SetText(scores.GetTotalShotsCount().ToString()); // Updating total shots in scoreboard
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
