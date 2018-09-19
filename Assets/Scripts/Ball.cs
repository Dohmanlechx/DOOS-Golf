using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball : MonoBehaviour {

    // Cached references
    public Rigidbody2D rb;
    public Rigidbody2D hookRb;
    public GameObject hook;

    // Public variables
    [SerializeField] public float releaseTime = 0.5f;
    [SerializeField] public float maxDragDistance = 2f;
    public bool allowCameraMove = false;

    // Private variables
    private bool isPressed = false;
    private bool isBallMoving = false;
    private bool alreadyExecuted = false;

    // Update ()
    private void Update ()
    {
        if (isPressed)
        {
            Vector2 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);

            if (Vector3.Distance(mousePos, hookRb.position) > maxDragDistance)
                rb.position = hookRb.position + (mousePos - hookRb.position).normalized * maxDragDistance;
            else
                rb.position = mousePos;
        }

        if (!alreadyExecuted && rb.velocity.magnitude <= 0.05f) // alreadyExecuted prevents it from running every frame
        {
            isBallMoving = false;
            UpdateHookPosition();
        }
    }

    // Updating hook's position into ball's position, needed
    // for shooting the ball again
    private void UpdateHookPosition()
    {
        Debug.Log("UpdateHookPosition() running");
        isBallMoving = false;
        hook.gameObject.transform.position = transform.position;
        alreadyExecuted = true;
    }

    // Executes as soon as mouse click is down
    private void OnMouseDown()
    {
        if (rb.velocity.magnitude <= 0.05f) // Checks if ball is not moving
        {
            GetComponent<SpringJoint2D>().enabled = true;
            allowCameraMove = false;
            isPressed = true;
            rb.isKinematic = true;
        }
    }

    // Executes as soon as mouse click is released
    private void OnMouseUp()
    {
        if (rb.velocity.magnitude <= 0.05f)
        {
            allowCameraMove = true;
            isPressed = false;
            rb.isKinematic = false;

            StartCoroutine(Release());
        }
    }

    // This coroutine shoots the ball, using component "SpringJoint2D"
    IEnumerator Release()
    {
        isBallMoving = true;

        yield return new WaitForSeconds(releaseTime);
        GetComponent<SpringJoint2D>().enabled = false;
        yield return new WaitForSeconds(2f);

        alreadyExecuted = false;
    }

    public void DestroyBall()
    {
        Destroy(gameObject, 0.1f);
    }
}
