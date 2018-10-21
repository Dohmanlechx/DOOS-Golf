using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TextMeshController : MonoBehaviour {

    // This script holds control over Textmeshs for shotCount and whoseTurn

    public TextMeshProUGUI shotCountText;
    public TextMeshProUGUI whoseTurnText;

    void Start () {
        whoseTurnText.SetText("P" + Scores.GetWhoseTurn().ToString());
    }

    public void UpdateText(int shotCount)
    {
        shotCountText.SetText(shotCount.ToString());
    }

    public void MakeRed()
    {
        shotCountText.color = Color.red;
    }
}
