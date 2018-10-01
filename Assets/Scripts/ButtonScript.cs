using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonScript : MonoBehaviour
{
    public Ball theBall;
    public Club theClub;

    public Button m_btnLeft, m_btnRight;

    private void Start()
    {
        Button btnLeft = m_btnLeft.GetComponent<Button>();
        Button btnRight = m_btnRight.GetComponent<Button>();
        btnLeft.onClick.RemoveAllListeners();
        btnRight.onClick.RemoveAllListeners();

        btnLeft.onClick.AddListener(MoveBallToLeft);
        btnRight.onClick.AddListener(MoveBallToRight);
    }

    private void Update()
    {
        if (theClub.isPressed)
            Destroy(gameObject);
    }

    private void MoveBallToLeft()
    {
        if (theBall.playersPositionChoice >= 1 && theBall.playersPositionChoice <= 4)
            theBall.playersPositionChoice = theBall.playersPositionChoice - 1;
    }

    private void MoveBallToRight()
    {
        if (theBall.playersPositionChoice >= 0 && theBall.playersPositionChoice <= 3)
            theBall.playersPositionChoice = theBall.playersPositionChoice + 1;
    }

}
