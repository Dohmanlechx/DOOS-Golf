using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameSystem : MonoBehaviour
{
    // Cached references
    public ParticleSystem particles;
    public Ball theBall;

    // Private variables
    private AudioSource audioSource;
    [SerializeField] List<AudioClip> sounds;

    // Public variables
    public static int shotNumber = 0;

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
        particles = FindObjectOfType<ParticleSystem>();
        theBall = FindObjectOfType<Ball>();
    }

    public void AddShot()
    {
        shotNumber++;
    }

    // Goal trigger, but if the ball is moving too fast, it won't trigger
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (theBall.rb.velocity.magnitude < 3.5f)
        {
            StartCoroutine(Goal());
        }
    }

    // Activating particles to cheer the player, waiting 3 sec, then loads next scene
    IEnumerator Goal()
    {
        Debug.Log("Goal! Shots: " + shotNumber);
        audioSource.PlayOneShot(sounds[0], 1f);
        theBall.DestroyBall();
        particles.Play();
        yield return new WaitForSeconds(3);
        shotNumber = 0;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
}
