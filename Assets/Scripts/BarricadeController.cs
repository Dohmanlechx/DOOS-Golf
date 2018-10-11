using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BarricadeController : MonoBehaviour
    {

    // Cached references
    public GameSystem gameSystem;
    public Ball theBall;
    public Club theClub;

    // Private variables
    private static int collisionHits = 0;

    private void Start()
        {
        gameSystem = FindObjectOfType<GameSystem>();
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

    // After 5 colliderhits with those barricades, the course resets
    private void OnCollisionEnter2D(Collision2D collision)
        {
        collisionHits++;
        Debug.Log("collisionHits: " + collisionHits);

        if (collisionHits >= 5 && gameSystem.GetShotCount() < 7)
            {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
            }
        else if (collisionHits >= 5 && gameSystem.GetShotCount() >= 7)
            {
            StartCoroutine(WaitThenLoadNextScene());
            }
        }

    // Having this method just because I want it to wait a bit
    IEnumerator WaitThenLoadNextScene()
        {
        yield return new WaitForSeconds(2f);
        gameSystem.LoadNextScene(3);
        }
    }
