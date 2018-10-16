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
    private bool goalAt7thSwing;
    private int courseIndex;

    public int GetShotCount() { return shotCount; }
    public int GetCourseIndex() { return courseIndex; }

    private void Start()
    {
        Scores.Instance.NeverMind();
        scores = FindObjectOfType<Scores>();
        particles = FindObjectOfType<ParticleSystem>();
        theBall = FindObjectOfType<Ball>();
        theClub = FindObjectOfType<Club>();
        shotCountText = FindObjectOfType<TextMeshProUGUI>();

        audioSource = GetComponent<AudioSource>();
        shotCount = 0;
        goalAt7thSwing = false;
        courseIndex = SceneManager.GetActiveScene().buildIndex;
    }

    public void AddShot()
    {
        shotCount++;
        shotCountText.SetText(shotCount.ToString());

        if (shotCount >= 7)
            StartCoroutine(TooManyShots(shotCount));
    }

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

    // Goal trigger, but if the ball is moving too fast, it won't trigger
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (theBall.rb.velocity.magnitude < 3.5f)
        {
            scores.SetScore(courseIndex, shotCount);
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
        LoadNextScene(shotCount);
    }

    public void LoadNextScene(int finalShotCount)
    {
        scores.SetScore(courseIndex, finalShotCount);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
}