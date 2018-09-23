using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameSystem : MonoBehaviour {

    // Cached references
    public ParticleSystem particles;

    private void Start()
    {
        particles = FindObjectOfType<ParticleSystem>();
    }

    // Goal trigger, but if the ball is moving too fast, it won't trigger
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (FindObjectOfType<Ball>().rb.velocity.magnitude < 3f)
        {
            Debug.Log("Goal!");
            FindObjectOfType<Ball>().DestroyBall();
            particles.Play();
        }
    }
}
