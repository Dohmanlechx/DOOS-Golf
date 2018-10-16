using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Scores : MonoBehaviour
{
    // Cached references
    public GameSystem gameSystem;

    // Public variables

    // Private variables
    private static Scores _instance;
    private int[] player1Scores;
    //private static int shotCount;
    private int totalShotsCount = 0;
    //private static bool created = false;

    public static Scores Instance
    {
        get
        {
            if (_instance == null)
            {
                GameObject obj = Instantiate(Resources.Load<GameObject>("Scores"));
                _instance = obj.GetComponent<Scores>();
            }
            return _instance;
        }
    }

    // bullshit code
    private void Awake()
    {
        if (_instance != null && _instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            _instance = this;
            DontDestroyOnLoad(gameObject);
        }
    }

    private void Start()
    {
        player1Scores = new int[20];
    }

    private void Update()
    {
        if (GameObject.Find("Game System") != null)
        {
            gameSystem = FindObjectOfType<GameSystem>();
        }
    }

    /*
    public int GetShotCount() { return shotCount; }

    public void AddShotToCount(int shot)
    {
        shotCount++;
    }

    public int GetTotalShotsCount() { return totalShotsCount; }

    public void AddShotToTotal(int shot)
    {
        totalShotsCount++;
    }

    public void AddShot()
    {
        shotCount++;
        totalShotsCount++;
        Debug.Log("shotCount: " + shotCount);
        gameSystem.shotCountText.SetText(shotCount.ToString());

        if (shotCount >= 7)
        {
            StartCoroutine(gameSystem.TooManyShots(shotCount));
        }
    }

    public void ResetShots()
    {
        shotCount = 0;
    }
       */
    public void NeverMind()
    {
        Debug.Log("Never mind");
    }

    public int[] GetScores()
    {
        return player1Scores;
    }

    public int GetTotalShotsCount()
    {
        totalShotsCount = 0;
        for (int i = 1; i < player1Scores.Length; i++)
        {
            totalShotsCount += player1Scores[i];
        }
        return totalShotsCount;
    }

    public void SetScore(int course, int shotCount)
    {
        player1Scores[course] = shotCount;
    }
}
