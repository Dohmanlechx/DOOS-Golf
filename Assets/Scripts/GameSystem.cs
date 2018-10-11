using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class GameSystem : MonoBehaviour
    {
    // Cached references
    public ParticleSystem particles;
    public Ball theBall;
    public Club theClub;
    public TextMeshProUGUI shotCountText;

    // Private variables
    private AudioSource audioSource;
    [SerializeField] List<AudioClip> sounds;
    private static int shotCount;
    private bool goalAt7thSwing;

    private void Start()
        {
        shotCount = 0;
        goalAt7thSwing = false;
        audioSource = GetComponent<AudioSource>();
        particles = FindObjectOfType<ParticleSystem>();
        theBall = FindObjectOfType<Ball>();
        theClub = FindObjectOfType<Club>();
        shotCountText = FindObjectOfType<TextMeshProUGUI>();
        }

    public void AddShot()
        {
        shotCount++;
        Debug.Log(shotCount);
        shotCountText.SetText(shotCount.ToString());

        if (shotCount >= 7)
            StartCoroutine(TooManyShots());
        }

    // Goal trigger, but if the ball is moving too fast, it won't trigger
    private void OnTriggerEnter2D(Collider2D collision)
        {
        if (theBall.rb.velocity.magnitude < 4.0f)
            {
            StartCoroutine(Goal());
            }
        }

    // Executes when the player had swung his 7th swing. If no goal, it counts as 8 shots
    IEnumerator TooManyShots()
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
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
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
        shotCount = 0;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        }
    }
