using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BarricadeController : MonoBehaviour
{

    // Cached references
    public GameSystem gameSystem;
    public CameraController cameraController;
    public SteepController steepController;

    //public Scores scores;
    public Ball theBall;
    public Club theClub;

    // Private variables
    private static int collisionHits = 0;

    private void Start()
    {
        gameSystem = FindObjectOfType<GameSystem>();
        cameraController = FindObjectOfType<CameraController>();
        steepController = FindObjectOfType<SteepController>();

        theBall = FindObjectOfType<Ball>();
        theClub = FindObjectOfType<Club>();
    }

    private void Update()
    {
        if (theBall != null)
        {
            if (theBall.rb.velocity.magnitude <= 0.02f) // Resets the collisionHits counter while ball is still
                collisionHits = 0;
        }
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        collisionHits++;

        // If the ball rolls back and hits on the rear of course, this executes
        if (gameObject.CompareTag("Restart"))
        {
            steepController.RestoreDefaultGravity();
            ResetBallPosition();
        }

        // Exclusive for Course 4
        // After 5 colliderhits with those barricades, the course resets
        if (SceneManager.GetActiveScene().name == "Course 4" &&
            collisionHits >= 5 && gameSystem.GetShotCount() < 7)
        {
            steepController.RestoreDefaultGravity();
            ResetBallPosition();
        }
        // If player after his 7th swing still misses, game loads next course and sets 8 as total swings
        else if (SceneManager.GetActiveScene().name == "Course 4" &&
            collisionHits >= 5 && gameSystem.GetShotCount() >= 7)
        {
            StartCoroutine(WaitThenLoadNextScene());
        }
    }

    private void ResetBallPosition()
    {
        theBall.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
        theBall.gameObject.transform.position = new Vector2(0, -3);
        cameraController.RestoreCameraToStartPosition();
    }

    // Having this method just because I want it to wait a bit
    IEnumerator WaitThenLoadNextScene()
    {
        yield return new WaitForSeconds(2f);
        gameSystem.FinalCheck(8); // 8 swings
    }
}
