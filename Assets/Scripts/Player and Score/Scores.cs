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
    private static int[] player1Scores, player2Scores, player3Scores, player4Scores;
    private static int whoseTurn = 1;
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

        if (PlayerPrefs.HasKey("Scores"))
        {
            player1Scores = GetStoredScores();
            Debug.Log("AWAKE " + player1Scores[1] + " " + player1Scores[2]);
        }
    }

    // Getters
    public static int GetWhoseTurn() { return whoseTurn; }

    public int[] GetScores(int playerIndex)
    {
        int[] temp = new int[20];
        switch (playerIndex)
        {
            case 1:
                temp = player1Scores;
                break;
            case 2:
                temp = player2Scores;
                break;
            case 3:
                temp = player3Scores;
                break;
            case 4:
                temp = player4Scores;
                break;
        }
        return temp;
    }

    public int GetTotalShotsCount(int playerIndex)
    {
        totalShotsCount = 0;
        //Debug.Log("Scores.cs: GetScores(" + playerIndex + ").Length: " + GetScores(playerIndex).Length);
        for (int i = 1; i < GetScores(playerIndex).Length; i++)
        {
            //Debug.Log("Scores.cs: GetScores(" + playerIndex + "): " + GetScores(playerIndex)[i]);
            totalShotsCount += GetScores(playerIndex)[i];
        }
        return totalShotsCount;
    }

    // Setter and storing in array
    public void SetScore(int courseIndex, int playerIndex, int shotCount)
    {
        GetScores(playerIndex)[courseIndex] = shotCount;
    }

    public void SetWhoseTurn(int playerIndex)
    {
        whoseTurn = playerIndex;
    }

    private void Start()
    {
        // The first 1 contains name, 2-19 are for holes and 20 is for total
        if (!PlayerPrefs.HasKey("Scores"))
        {
            player1Scores = new int[20];
        }

        player2Scores = new int[20];
        player3Scores = new int[20];
        player4Scores = new int[20];
    }

    private void Update()
    {
        // To make sure to always have connection with GameSystem
        if (GameObject.Find("Game System") != null)
        {
            gameSystem = FindObjectOfType<GameSystem>();
        }
    }

    public static void NewGame()
    {
        player1Scores = new int[20];
        player2Scores = new int[20];
        player3Scores = new int[20];
        player4Scores = new int[20];

        SetScoresIntoPrefs(player1Scores);
    }

    public void TestMetod()
    {
        SetScoresIntoPrefs(player1Scores);
    }

    // Use this to SET Integer array
    public static void SetScoresIntoPrefs(int[] scores)
    {
        PlayerPrefs.SetString("Scores", GetSerializedString(scores));
    }

    // Use this to GET Integer array
    public static int[] GetStoredScores()
    {
        string[] data = PlayerPrefs.GetString("Scores", "0").Split('|');
        int[] val = new int[data.Length];
        int score;
        for (int i = 0; i < val.Length; i++)
        {
            val[i] = int.TryParse(data[i], out score) ? score : 0;
        }
        Debug.Log("val: " + val);
        return val;
    }

    private static string GetSerializedString(int[] data)
    {
        if (data.Length == 0) return string.Empty;

        string result = data[0].ToString();
        for (int i = 1; i < data.Length; i++)
        {
            result += ("|" + data[i]);
        }
        Debug.Log("result: " + result);
        return result;
    }

    // Needing this code to let GameSystem make instance of this gameobject
    public void NeverMind()
    {
        // Nothing :)
    }
}
