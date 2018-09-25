using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameSystem : MonoBehaviour
{
    // Cached references
    public ParticleSystem particles;

    public static int shotNumber = 0;

    private void Start()
    {
        particles = FindObjectOfType<ParticleSystem>();
    }

    public void AddShot()
    {
        shotNumber++;
    }

    // Goal trigger, but if the ball is moving too fast, it won't trigger
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (FindObjectOfType<Ball>().rb.velocity.magnitude < 3.5f)
        {
            Debug.Log("Goal! Shots: " + shotNumber);
            FindObjectOfType<Ball>().DestroyBall();
            particles.Play();
        }
    }
}
