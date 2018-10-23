using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball : MonoBehaviour
{
    // Cached references
    public Rigidbody2D rb;
    public Club theClub;
    public AudioSource audioSource;
    public SpriteRenderer spriteRenderer;
    public Sprite ballDefault, ballBlue, ballYellow, ballRed;

    // Public variables
    public int playersPositionChoice;

    // Private variables
    [SerializeField] List<Transform> startPositions;
    [SerializeField] List<AudioClip> sounds;
    private int whoseTurnColor;

    // --- START ---
    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        audioSource = GetComponent<AudioSource>();
        // 2 is the middle position, set as default
        playersPositionChoice = 2;
        whoseTurnColor = PlayerPrefs.GetInt("whoseTurn");

        // Switching the color on ball depending player
        switch (whoseTurnColor)
        {
            case 2:
                gameObject.GetComponent<SpriteRenderer>().sprite = ballRed;
                break;
            case 3:
                gameObject.GetComponent<SpriteRenderer>().sprite = ballYellow;
                break;
            case 4:
                gameObject.GetComponent<SpriteRenderer>().sprite = ballBlue;
                break;
            default:
                gameObject.GetComponent<SpriteRenderer>().sprite = ballDefault;
                break;
        }

        StartCoroutine(PositionTheBall());
    }

    // --- METHODS ---
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
