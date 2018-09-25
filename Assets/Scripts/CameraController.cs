using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public GameObject ball;
    public Vector3 offset;
    public float MIN_X;
    public float MAX_X;
    public float MIN_Y;
    public float MAX_Y;

    void Start()
    {
        offset = transform.position - ball.transform.position;
    }

    void LateUpdate()
    {
        if (ball != null)
        {
            transform.position = ball.transform.position + offset;
            transform.position = new Vector3
                (Mathf.Clamp(transform.position.x, MIN_X, MAX_X),
                Mathf.Clamp(transform.position.y, MIN_Y, MAX_Y),
                Mathf.Clamp(transform.position.z, -10f, -10f));
        }
    }
}
