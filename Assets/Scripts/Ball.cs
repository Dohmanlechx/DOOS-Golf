using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball : MonoBehaviour
{
    // Cached references
    public Rigidbody2D rb;
    public Club theClub;
    
    // Private variables
    [SerializeField] List<Transform> startPositions;

    // Public variables
    public int playersPositionChoice;

    private void Start()
    {
        // 2 is the middle position, set as default
        playersPositionChoice = 2;
        rb = GetComponent<Rigidbody2D>();
        StartCoroutine(PositionTheBall());
    }

    // This coroutine is running as long player hasn't touched the club
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
