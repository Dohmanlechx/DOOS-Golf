using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TextMeshController : MonoBehaviour {

    public TextMeshProUGUI shotCountText;
    public TextMeshProUGUI whoseTurnText;

    // Use this for initialization
    void Start () {
        whoseTurnText.SetText("P" + Scores.GetWhoseTurn().ToString());
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    public void UpdateText(int shotCount)
    {
        shotCountText.SetText(shotCount.ToString());
    }
}
