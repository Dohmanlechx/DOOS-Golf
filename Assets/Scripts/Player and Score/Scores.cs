using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scores : MonoBehaviour
{
    private int[] player1Scores = new int[18];
    private static bool created = false;

    private void Awake()
    {
        DontDestroyOnLoad(gameObject);
        created = true;
        Debug.Log("Awake:" + gameObject);
    }

    private void Start()
    {
    }

    public void TestLog()
    {
        Debug.Log("test: " + player1Scores[1]);
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
