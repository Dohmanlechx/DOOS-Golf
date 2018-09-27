using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public GameObject ball;
    public Club club;

    private Vector3 touchStart;
    public Vector3 offset;
    public float MIN_X;
    public float MAX_X;
    public float MIN_Y;
    public float MAX_Y;

    private void Start()
    {
        offset = transform.position - ball.transform.position;
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            touchStart = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        }
        if (Input.GetMouseButton(0))
        {
            Vector3 direction = touchStart - Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Camera.main.transform.position += direction;
        }

        if (ball != null)
        {
            if (club.ongoingShoot)
                transform.position = ball.transform.position + offset;

            transform.position = new Vector3
                (Mathf.Clamp(transform.position.x, MIN_X, MAX_X),
                Mathf.Clamp(transform.position.y, MIN_Y, MAX_Y),
                Mathf.Clamp(transform.position.z, -10f, -10f));
        }
    }
}
