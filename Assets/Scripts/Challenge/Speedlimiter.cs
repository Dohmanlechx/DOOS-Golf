using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Speedlimiter : MonoBehaviour
{
    // Public variables
    public float maxSpeed = 18f;

    // --- FIXEDUPDATE ---
    void FixedUpdate()
    {
        if (GetComponent<Rigidbody2D>().velocity.magnitude > maxSpeed)
        {
            GetComponent<Rigidbody2D>().velocity = GetComponent<Rigidbody2D>().velocity.normalized * maxSpeed;
        }
    }
}
