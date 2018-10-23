using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    // Cached references
    public GameObject theBall;
    public Club theClub;

    // Private variables
    [SerializeField] float MIN_X;
    [SerializeField] float MAX_X;
    [SerializeField] float MIN_Y;
    [SerializeField] float MAX_Y;
    private Vector2 startPosition = new Vector2(0, -0.8f);
    private Vector3 touchStart;
    private Vector3 offset;

    // --- START ---
    private void Start()
    {
        offset = transform.position - theBall.transform.position;
    }

    // --- UPDATE ---
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

    // This method is needed for builds
    public void RestoreCameraToStartPosition()
    {
        transform.position = startPosition;
    }
}
