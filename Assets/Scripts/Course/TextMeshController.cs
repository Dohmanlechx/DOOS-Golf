using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TextMeshController : MonoBehaviour
{
    // This script holds control over Textmeshs for shotCount and whoseTurn

    // Public variables
    public TextMeshProUGUI shotCountText;
    public TextMeshProUGUI whoseTurnText;

    // --- START ---
    void Start()
    {
        whoseTurnText.SetText("P" + PlayerPrefs.GetInt("whoseTurn").ToString());
    }

    // --- METHODS --
    public void UpdateText(int shotCount)
    {
        shotCountText.SetText(shotCount.ToString());
    }

    public void MakeRed()
    {
        shotCountText.color = Color.red;
    }
}
