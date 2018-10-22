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
    //public List<TextMeshPro> listOfAllPlayers = new List<TextMeshPro>();
    public bool isContinue;

    // Private variables
    private static int lastCourseIndex;
    private int amountPlayers;

    private void Start()
    {
        gameSystem = FindObjectOfType<GameSystem>();
        scores = FindObjectOfType<Scores>();
        amountPlayers = PlayerPrefs.GetInt("amountPlayers");

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

        UpdateScoreBoard();
    }

    private void UpdateScoreBoard()
    {
        if (scores != null)
        {
            for (int i = 1; i <= amountPlayers; i++)
            {
                // Temporary empty List where it gets overwritten by an existing one, see switch/case statement
                List<TextMeshPro> thisPlayerTextMeshs = new List<TextMeshPro>();

                switch (i)
                {
                    case 1:
                        thisPlayerTextMeshs = player1TextMeshs;
                        break;
                    case 2:
                        thisPlayerTextMeshs = player2TextMeshs;
                        break;
                    case 3:
                        thisPlayerTextMeshs = player3TextMeshs;
                        break;
                    case 4:
                        thisPlayerTextMeshs = player4TextMeshs;
                        break;
                }
                // Updating total shots in scoreboard
                thisPlayerTextMeshs[thisPlayerTextMeshs.Count - 1].SetText(scores.GetTotalShotsCount(i).ToString());

                // Updatering scores
                for (int j = 1; j <= thisPlayerTextMeshs.Count - 2; j++)
                {
                    int[] myScores = scores.GetScores(i);
                    //Debug.Log("ScoreBoard.cs: myScores[j]: " + myScores[j]);
                    if (myScores[j] == 0)
                    {
                        thisPlayerTextMeshs[j].SetText(""); // Unplayed courses
                    }
                    else
                    {
                        thisPlayerTextMeshs[j].SetText(myScores[j].ToString()); // Played courses
                    }
                }
                // Telling to players that those courses don't exist yet (warning for hard-coding)
                for (int k = 6; k <= 18; k++)
                {
                    thisPlayerTextMeshs[k].SetText("x");
                }
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
