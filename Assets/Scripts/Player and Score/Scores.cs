using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scores : MonoBehaviour
{
    private int[] player1Scores = new int[19];
    private static bool created = false;

    private void Awake()
    {
        if (!created)
        {
            DontDestroyOnLoad(gameObject);
            created = true;
        }
    }

    private void Start()
    {

    }

    public int[] GetScores()
    {
        return player1Scores;
    }

    public void SetScore(int course, int score)
    {
        player1Scores[course] = score;
    }
}
