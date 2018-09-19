using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameSystem : MonoBehaviour {

    // Cached references
    public GameObject ball;
    public Rigidbody2D ballSpeed;
    public ParticleSystem particles;

    private void Start()
    {
        ball = FindObjectOfType<GameObject>();
        ballSpeed = FindObjectOfType<Rigidbody2D>();
        particles = FindObjectOfType<ParticleSystem>();
    }

    void Update () {
		
	}

    // Goal trigger, but if the ball is moving too fast, it won't trigger
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (ballSpeed.velocity.magnitude < 2.5f)
        {
            Debug.Log("Goal!");
            FindObjectOfType<Ball>().DestroyBall();
            particles.Play();
        }
    }
}
