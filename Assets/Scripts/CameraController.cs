using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour {

    public GameObject ball;

    public Vector3 offset;

	void Start ()
    {
        offset = transform.position - ball.transform.position;
	}

	void LateUpdate ()
    {
        if (FindObjectOfType<Ball>().allowCameraMove)
        transform.position = ball.transform.position + offset;
	}
}
