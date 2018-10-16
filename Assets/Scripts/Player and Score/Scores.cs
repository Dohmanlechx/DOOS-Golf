using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Scores : MonoBehaviour
{
    // THIS SCRIPT NEVER DESTROYS IN GAME
    // THIS SCRIPT IS STORING PLAYER'S SCORES AND ALSO COUNTING TOTAL SHOTS OF THEM

    // Cached references
    public GameSystem gameSystem;

    // Private variables
    private static Scores _instance;
    private int[] player1Scores;
    private int totalShotsCount = 0;

    // Singleton
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
        // The first 1 contains name, 2-19 are for holes and 20 is for total
        player1Scores = new int[20];
    }

    private void Update()
    {
        // To make sure to always have connection with GameSystem
        if (GameObject.Find("Game System") != null)
        {
            gameSystem = FindObjectOfType<GameSystem>();
        }
    }

    // Needing this code to let GameSystem make instance of this gameobject
    public void NeverMind()
    {
        Debug.Log("Never mind");
    }

    // Getters
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

    // Setter and storing in array
    public void SetScore(int courseIndex, int shotCount)
    {
        player1Scores[courseIndex] = shotCount;
    }
}
