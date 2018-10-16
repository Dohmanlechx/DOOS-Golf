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
    private int[] player1Scores = new int[20];
    private static int shotCount;
    private bool foundGameSystem = false;
    //private static bool created = false;

    public static Scores Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = Instantiate(Resources.Load<GameObject>("Scores")).GetComponent<Scores>();
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
        Debug.Log("Start runs");
        //gameSystem = FindObjectOfType<GameSystem>();
    }

    private void Update()
    {
        if (GameObject.Find("Game System") != null)
        {
            gameSystem = FindObjectOfType<GameSystem>();
        }
    }

    public int GetShotCount() { return shotCount; }

    public void AddShot()
    {
        shotCount++;
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

    public int[] GetScores()
    {
        return player1Scores;
    }

    public void SetScore(int course, int score)
    {
        player1Scores[course] = score;
    }
}
