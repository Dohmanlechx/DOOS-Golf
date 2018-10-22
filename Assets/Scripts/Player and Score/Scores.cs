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

        if (!PlayerPrefs.HasKey("whoseTurn"))
        {
            PlayerPrefs.SetInt("whoseTurn", whoseTurn);
        }
        else
        {
            whoseTurn = PlayerPrefs.GetInt("whoseTurn");
        }

        if (PlayerPrefs.HasKey("player1Scores"))
        {
            player1Scores = GetStoredScores(1);

            if (PlayerPrefs.HasKey("player2Scores"))
            {
                player2Scores = GetStoredScores(2);

                if (PlayerPrefs.HasKey("player3Scores"))
                {
                    player3Scores = GetStoredScores(3);

                    if (PlayerPrefs.HasKey("player4Scores"))
                    {
                        player4Scores = GetStoredScores(4);
                    }
                }
            }
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
        PlayerPrefs.SetInt("whoseTurn", whoseTurn);
    }

    private void Start()
    {

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

        SetScoresIntoPrefs(player1Scores, 1);
        SetScoresIntoPrefs(player2Scores, 2);
        SetScoresIntoPrefs(player3Scores, 3);
        SetScoresIntoPrefs(player4Scores, 4);
    }

    public void TestMetod(int amountPlayers)
    {
        switch (amountPlayers)
        {
            case 1:
                SetScoresIntoPrefs(player1Scores, 1);
                break;
            case 2:
                SetScoresIntoPrefs(player1Scores, 1);
                SetScoresIntoPrefs(player2Scores, 2);
                break;
            case 3:
                SetScoresIntoPrefs(player1Scores, 1);
                SetScoresIntoPrefs(player2Scores, 2);
                SetScoresIntoPrefs(player3Scores, 3);
                break;
            case 4:
                SetScoresIntoPrefs(player1Scores, 1);
                SetScoresIntoPrefs(player2Scores, 2);
                SetScoresIntoPrefs(player3Scores, 3);
                SetScoresIntoPrefs(player4Scores, 4);
                break;
            default:
                SetScoresIntoPrefs(player1Scores, 1);
                break;
        }
    }

    // Use this to SET Integer array
    public static void SetScoresIntoPrefs(int[] scores, int playerIndex)
    {
        switch (playerIndex)
        {
            case 1:
                PlayerPrefs.SetString("player1Scores", GetSerializedString(scores));
                break;
            case 2:
                PlayerPrefs.SetString("player2Scores", GetSerializedString(scores));
                break;
            case 3:
                PlayerPrefs.SetString("player3Scores", GetSerializedString(scores));
                break;
            case 4:
                PlayerPrefs.SetString("player4Scores", GetSerializedString(scores));
                break;
        }
    }

    // Use this to GET Integer array
    public static int[] GetStoredScores(int playerIndex)
    {
        string thisPlayerScores = "";

        switch (playerIndex)
        {
            case 1:
                thisPlayerScores = "player1Scores";
                break;
            case 2:
                thisPlayerScores = "player2Scores";
                break;
            case 3:
                thisPlayerScores = "player3Scores";
                break;
            case 4:
                thisPlayerScores = "player4Scores";
                break;
        }

        string[] data = PlayerPrefs.GetString(thisPlayerScores, "0").Split('|');
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
