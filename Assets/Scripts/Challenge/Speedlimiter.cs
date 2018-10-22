using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Speedlimiter : MonoBehaviour {

    public float maxSpeed = 18f;
	// Update is called once per frame
	void FixedUpdate () {
		if(GetComponent<Rigidbody2D>().velocity.magnitude > maxSpeed)
        {
            GetComponent<Rigidbody2D>().velocity = GetComponent<Rigidbody2D>().velocity.normalized * maxSpeed;
        }
	}
}
