using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball : MonoBehaviour
{
    // Cached references
    public Rigidbody2D rb;
    public Club theClub;

    [SerializeField] List<Transform> startPositions;
    public int playersPositionChoice;

    private void Start()
    {
        playersPositionChoice = 2;
        rb = GetComponent<Rigidbody2D>();
        StartCoroutine(PositionTheBall());
    }

    private IEnumerator PositionTheBall()
    {
        while (!theClub.isPressed)
        {
            transform.position = startPositions[playersPositionChoice].transform.position;
            yield return new WaitForSeconds(0f);
        }
    }

    public void DestroyBall()
    {
        Destroy(gameObject, 0.1f);
    }
}
