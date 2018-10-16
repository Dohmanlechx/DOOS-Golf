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
    public TextMeshProUGUI shotCountText;

    // Private variables
    private AudioSource audioSource;
    [SerializeField] List<AudioClip> sounds;
    private static int shotCount;
    //private static int totalShotsCount = 0;
    private bool goalAt7thSwing;
    private int courseIndex;

    public int GetShotCount() { return shotCount; }
    //public int GetTotalShotsCount() { return totalShotsCount; }
    public int GetCourseIndex() { return courseIndex; }

    private void Start()
    {
        Scores.Instance.NeverMind();
        shotCount = 0;
        scores = FindObjectOfType<Scores>();
        audioSource = GetComponent<AudioSource>();
        particles = FindObjectOfType<ParticleSystem>();
        theBall = FindObjectOfType<Ball>();
        theClub = FindObjectOfType<Club>();
        shotCountText = FindObjectOfType<TextMeshProUGUI>();
        courseIndex = SceneManager.GetActiveScene().buildIndex;
        goalAt7thSwing = false;
    }
    
    public void AddShot()
    {
        shotCount++;
        scores.AddOneShotToTotal();
        Debug.Log("shotCount:" + shotCount);
        Debug.Log("totalShotsCount: " + scores.GetTotalShotsCount());
        shotCountText.SetText(shotCount.ToString());

        if (shotCount >= 7)
            StartCoroutine(TooManyShots(shotCount));
    }
    
    // Goal trigger, but if the ball is moving too fast, it won't trigger
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (theBall.rb.velocity.magnitude < 3.5f)
        {
            scores.SetScore(courseIndex, shotCount);
            StartCoroutine(Goal());
        }
    }

    /*
    public void AddShot()
    {
        scores.AddShotToCount(1);
        scores.AddShotToTotal(1);
        Debug.Log("shotCount: " + scores.GetShotCount());
        shotCountText.SetText(scores.GetShotCount().ToString());

        if (scores.GetShotCount() >= 7)
        {
            StartCoroutine(TooManyShots(scores.GetShotCount());
        }
    }
    */

    // Executes when the player had swung his 7th swing. If no goal, it counts as 8 shots
    public IEnumerator TooManyShots(int shotCount)
    {
        shotCountText.color = Color.red;
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
        LoadNextScene(shotCount);
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
        LoadNextScene(shotCount);
    }

    public void LoadNextScene(int finalShotCount)
    {
        //int tempShot = scores.GetShotCount();
        //finalShotCount = tempShot;
        scores.SetScore(courseIndex, finalShotCount);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
}