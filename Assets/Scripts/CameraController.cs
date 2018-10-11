using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
    {
    // Cached references
    public GameObject theBall;
    public Club theClub;

    // Public variables
    public Vector3 offset;
    public float MIN_X;
    public float MAX_X;
    public float MIN_Y;
    public float MAX_Y;

    // Private variables
    private Vector3 touchStart;

    private void Start()
        {
        offset = transform.position - theBall.transform.position;
        }

    private void Update()
        {
        // Allowing the player to move the camera around
        if (Input.GetMouseButtonDown(0))
            {
            touchStart = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            }
        if (Input.GetMouseButton(0))
            {
            Vector3 direction = touchStart - Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Camera.main.transform.position += direction;
            }

        if (theBall != null)
            {
            if (theClub.ongoingShoot) // Forcing the camera to follow the ball if a shoot is ongoing (and during the rolling)
                transform.position = theBall.transform.position + offset;

            transform.position = new Vector3
                (Mathf.Clamp(transform.position.x, MIN_X, MAX_X),
                Mathf.Clamp(transform.position.y, MIN_Y, MAX_Y),
                Mathf.Clamp(transform.position.z, -10f, -10f));
            }
        }
    }
