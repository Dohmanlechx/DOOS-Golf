using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball : MonoBehaviour
    {
    // Cached references
    public Rigidbody2D rb;
    public Club theClub;
    public AudioSource audioSource;

    // Private variables
    [SerializeField] List<Transform> startPositions;
    [SerializeField] List<AudioClip> sounds;

    // Public variables
    public int playersPositionChoice;

    private void Start()
        {
        audioSource = GetComponent<AudioSource>();
        // 2 is the middle position, set as default
        playersPositionChoice = 2;
        rb = GetComponent<Rigidbody2D>();
        StartCoroutine(PositionTheBall());
        }

    private void OnCollisionEnter2D(Collision2D collision)
        {
        int randomSound = Random.Range(0, 3);
        audioSource.PlayOneShot(sounds[randomSound], 1f);
        }

    // This coroutine is running as long player hasn't touched the club
    private IEnumerator PositionTheBall()
        {
        while (!theClub.isPressed)
            {
            transform.position = startPositions[playersPositionChoice].transform.position;
            yield return new WaitForSeconds(0f);
            }
        }

    public void DestroyBall()
        {
        Destroy(gameObject, 0.1f);
        }
    }
