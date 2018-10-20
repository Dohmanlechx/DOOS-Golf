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

        switch (amountPlayers)
        {
            case 1:
                listOfAllPlayers.Add(player1TextMeshs);
                break;
            case 2:
                listOfAllPlayers.Add(player1TextMeshs);
                listOfAllPlayers.Add(player2TextMeshs);
                break;
            case 3:
                listOfAllPlayers.Add(player1TextMeshs);
                listOfAllPlayers.Add(player2TextMeshs);
                listOfAllPlayers.Add(player3TextMeshs);
                break;
            case 4:
                listOfAllPlayers.Add(player1TextMeshs);
                listOfAllPlayers.Add(player2TextMeshs);
                listOfAllPlayers.Add(player3TextMeshs);
                listOfAllPlayers.Add(player4TextMeshs);
                break;
        }


        foreach (List<TextMeshPro> playerTextMeshs in listOfAllPlayers)
        {
            UpdateScoreBoard(playerTextMeshs);
        }
    }

    private void UpdateScoreBoard(List<TextMeshPro> thisPlayerTextMeshs)
    {
        if (scores != null)
        {
            for (int i = 1; i <= thisPlayerTextMeshs.Count - 1; i++)
            {
                int[] myScores = scores.GetScores(i);
                Debug.Log("i: " + i + " myScores[i]: " + myScores[i]);
                if (myScores[i] == 0)
                {
                    thisPlayerTextMeshs[i].SetText(""); // Unplayed courses
                }
                else
                {
                    thisPlayerTextMeshs[i].SetText(myScores[i].ToString()); // Played courses
                }

                //TODO fixa detta
                thisPlayerTextMeshs[19].SetText(scores.GetTotalShotsCount(i).ToString()); // Updating total shots in scoreboard
            }

            // Telling to players that those courses don't exist yet (warning for hard-coding)
            for (int j = 6; j <= 18; j++)
            {
                thisPlayerTextMeshs[j].SetText("x");
            }
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
