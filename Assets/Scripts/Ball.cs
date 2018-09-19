using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball : MonoBehaviour {

    // Cached references
    public Rigidbody2D rb;
    public Rigidbody2D hookRb;
    public GameObject hook;

    // Variables
    [SerializeField] public float releaseTime = 0.5f;
    [SerializeField] public float maxDragDistance = 2f;

    private bool isPressed = false;
    private bool isBallMoving = false;
    public bool allowCameraMove = false;

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

    }

    private void UpdateHookPosition()
    {
        Debug.Log("UpdateHookPosition() running");
        if (rb.velocity.magnitude < 0.1f)
        {
            isBallMoving = false;
            hook.gameObject.transform.position = transform.position;
        }
    }

    private void OnMouseDown()
    {
        allowCameraMove = false;
        isPressed = true;
        rb.isKinematic = true;
    }

    private void OnMouseUp()
    {
        allowCameraMove = true;
        isPressed = false;
        rb.isKinematic = false;
        
        StartCoroutine(Release());
    }

    IEnumerator Release()
    {
        isBallMoving = true;

        yield return new WaitForSeconds(releaseTime);

        GetComponent<SpringJoint2D>().enabled = false;

        yield return new WaitForSeconds(2f);

    }
}
