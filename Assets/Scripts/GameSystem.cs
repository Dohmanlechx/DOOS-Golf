using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class GameSystem : MonoBehaviour
{
    // Cached references
    public Scores scores;
    public ParticleSystem particles;
    public Ball theBall;
    public Club theClub;
    public TextMeshController tmController;

    // Private variables
    private AudioSource audioSource;
    [SerializeField] List<AudioClip> sounds;
    private static int shotCount;
    private bool goalAt7thSwing;
    private static int courseIndex;

    // Getter
    public int GetShotCount() { return shotCount; }
    public int GetCourseIndex() { return courseIndex; }

    private void Start()
    {
        Scores.Instance.NeverMind();
        scores = FindObjectOfType<Scores>();
        particles = FindObjectOfType<ParticleSystem>();
        theBall = FindObjectOfType<Ball>();
        theClub = FindObjectOfType<Club>();
        tmController = FindObjectOfType<TextMeshController>();

        audioSource = GetComponent<AudioSource>();
        shotCount = 0;
        goalAt7thSwing = false;
        courseIndex = SceneManager.GetActiveScene().buildIndex;

        // So player can continue from this course between sessions
        PlayerPrefs.SetInt("LastPlayed", courseIndex);
        //scores.SaveAllScores();
    }

    public void AddShot()
    {
        shotCount++;

        tmController.UpdateText(shotCount);

        if (shotCount >= 7)
            StartCoroutine(TooManyShots(shotCount));
    }

    // Executes when the player had swung his 7th swing. If no goal, it counts as 8 shots
    public IEnumerator TooManyShots(int shotCount)
    {
        tmController.MakeRed();
        yield return new WaitUntil(() => theClub.ongoingShoot == false);
        if (goalAt7thSwing)
        {
            shotCount = 7;
        }
        else
        {
            shotCount = 8;
        }
        Debug.Log("Final result:" + shotCount);
        FinalCheck(shotCount);
    }

    // Goal trigger, but if the ball is moving too fast, it won't trigger
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (theBall.rb.velocity.magnitude < 3.75f)
        {
            //scores.SetScore(courseIndex, currentPlayer, shotCount);
            StartCoroutine(Goal());
        }
    }

    // Activating particles to cheer the player, waiting 3 sec, then loads next scene
    IEnumerator Goal()
    {
        Debug.Log("Goal! Shots: " + shotCount);
        goalAt7thSwing = true; // Just in case
        audioSource.PlayOneShot(sounds[0], 1f);
        theBall.DestroyBall();
        particles.Play();
        yield return new WaitForSeconds(3f);
        FinalCheck(shotCount);
    }

    public void FinalCheck(int finalShotCount)
    {
        if (ChoosePlayers.GetAmountPlayers() == 1)
        {
            LoadNextScene(finalShotCount, 1, true);
        }
        else if (ChoosePlayers.GetAmountPlayers() > 1 && Scores.GetWhoseTurn() == 1)
        {
            LoadNextScene(finalShotCount, 2, false);
        }
        else if (ChoosePlayers.GetAmountPlayers() > 2 && Scores.GetWhoseTurn() == 2)
        {
            LoadNextScene(finalShotCount, 3, false);
        }
        else if (ChoosePlayers.GetAmountPlayers() > 3 && Scores.GetWhoseTurn() == 3)
        {
            LoadNextScene(finalShotCount, 4, false);
        }
        else
        {
            LoadNextScene(finalShotCount, 1, true);
        }
    }

    private void LoadNextScene(int finalShotCount, int player, bool next)
    {
        scores.SetScore(courseIndex, Scores.GetWhoseTurn(), finalShotCount);
        scores.TestMetod();
        scores.SetWhoseTurn(player);

        if (next)
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        }
        else
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
    }
}