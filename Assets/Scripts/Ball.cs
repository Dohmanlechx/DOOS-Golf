using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball : MonoBehaviour
{
    // Cached references
    public Rigidbody2D rb;

    [SerializeField] List<Transform> startPositions;
    public int playersPositionChoice = 2;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        StartCoroutine(PositionTheBall());
    }

    private IEnumerator PositionTheBall()
    {
        gameObject.transform.position = startPositions[playersPositionChoice].transform.position;
        yield return new WaitForSeconds(1f);
    }

    public void DestroyBall()
    {
        Destroy(gameObject, 0.1f);
    }
}
