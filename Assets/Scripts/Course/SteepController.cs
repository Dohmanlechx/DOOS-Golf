using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SteepController : MonoBehaviour
{
    // Cached references
    public Rigidbody2D rb;
    public Ball theBall;

    // Private variables
    private bool isGravityArea = false;
    // Each course has own values, i.e. the higher scale/mass is, it feels steepier and harder
    [SerializeField] float myGravityScale = 0.5f;
    [SerializeField] float myMass = 0.2f;

    // When the ball enters the area, gravity activates
    private void OnTriggerEnter2D(Collider2D collision)
    {
        isGravityArea = true;
        StartCoroutine(TriggerGravity());
    }

    // When the ball exits the area, gravity deactivates from TriggerGravity()
    private void OnTriggerExit2D(Collider2D other)
    {
        isGravityArea = false;
    }

    IEnumerator TriggerGravity()
    {
        if (isGravityArea)
        {
            theBall.rb.gravityScale = myGravityScale;
            theBall.rb.mass = myMass;
            theBall.rb.drag = 0f;
            theBall.rb.angularDrag = 0.05f;
        }
        yield return new WaitUntil(() => isGravityArea == false);
        RestoreDefaultGravity();
    }

    public void RestoreDefaultGravity()
    {
        theBall.rb.gravityScale = 0f;
        theBall.rb.mass = 0.2f;
        theBall.rb.drag = 0.85f;
        theBall.rb.angularDrag = 0.5f;
    }
}
