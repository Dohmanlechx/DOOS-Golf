using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonScript : MonoBehaviour
{
    public Ball theBall;

    public Button m_btnLeft, m_btnRight;

    private void Start()
    {
        Button btnLeft = m_btnLeft.GetComponent<Button>();
        Button btnRight = m_btnRight.GetComponent<Button>();
    }

    private void MoveBallToLeft()
    {
        theBall.playersPositionChoice = theBall.playersPositionChoice - 1;
    }

}
