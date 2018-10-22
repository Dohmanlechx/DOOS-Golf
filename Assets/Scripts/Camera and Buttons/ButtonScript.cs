using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ButtonScript : MonoBehaviour
{
    // Cached references
    public Ball theBall;
    public Club theClub;
    public GameSystem gameSystem;
    //public ScoreBoard scoreBoard;

    // Public variables
    public Button m_btnLeft, m_btnRight, m_btnScoreboard;

    private void Start()
    {
        gameSystem = FindObjectOfType<GameSystem>();
        //scoreBoard = FindObjectOfType<ScoreBoard>();

        // Attaching the buttons
        Button btnLeft = m_btnLeft.GetComponent<Button>();
        Button btnRight = m_btnRight.GetComponent<Button>();
        Button btnScoreboard = m_btnScoreboard.GetComponent<Button>();
        // Removing the in-built listeners
        btnLeft.onClick.RemoveAllListeners();
        btnRight.onClick.RemoveAllListeners();
        btnScoreboard.onClick.RemoveAllListeners();
        // Adding own listeners
        btnLeft.onClick.AddListener(MoveBallToLeft);
        btnRight.onClick.AddListener(MoveBallToRight);
        btnScoreboard.onClick.AddListener(LoadScoreboardScene);
    }

    private void Update()
    {
        // If club is pressed, destroy the buttons
        if (theClub.isPressed)
            Destroy(gameObject);
    }

    private void MoveBallToLeft()
    {
        if (theBall.playersPositionChoice >= 1 && theBall.playersPositionChoice <= 4)
            theBall.playersPositionChoice = theBall.playersPositionChoice - 1;
    }

    private void MoveBallToRight()
    {
        if (theBall.playersPositionChoice >= 0 && theBall.playersPositionChoice <= 3)
            theBall.playersPositionChoice = theBall.playersPositionChoice + 1;
    }

    private void LoadScoreboardScene()
    {
        PlayerPrefs.SetInt("lastPlayedCourse", SceneManager.GetActiveScene().buildIndex);
        ScoreBoard.SetLastCourseIndex(PlayerPrefs.GetInt("lastPlayedCourse")); // Back to last course
        SceneManager.LoadScene("Scoreboard");
    }
}
