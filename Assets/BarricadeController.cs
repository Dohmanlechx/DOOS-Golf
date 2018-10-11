using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BarricadeController : MonoBehaviour {

    public Ball theBall;
    public Club theClub;

    private static int collisionHits = 0;

    private void Start()
    {
        theBall = FindObjectOfType<Ball>();
        theClub = FindObjectOfType<Club>();
    }

    private void Update()
    {
        if (theBall.rb.velocity.magnitude <= 0.02f)
            collisionHits = 0;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        collisionHits++;
        Debug.Log(collisionHits);

        if (collisionHits >= 3)
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
    }
}
