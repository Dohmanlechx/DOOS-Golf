using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball : MonoBehaviour {

    // Cached references
    public Rigidbody2D rb;

    // Public variables
    [SerializeField] public float releaseTime = 0.5f;
    [SerializeField] public float maxDragDistance = 2f;
    public bool allowCameraMove = false;

    // Private variables
    private bool isPressed = false;
    private bool isBallMoving = false;
    private bool alreadyExecuted = false;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    public void DestroyBall()
    {
        Destroy(gameObject, 0.1f);
    }
}
