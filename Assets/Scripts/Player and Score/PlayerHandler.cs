using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHandler : MonoBehaviour {

    public static ArrayList playerList = new ArrayList();

    int[] player1Holes = new int[18];
    int[] player2Holes = new int[18];
    int[] player3Holes = new int[18];
    int[] player4Holes = new int[18];

    public int getPlayerAmount()
    {
        int p = playerList.Count;
        return p;
    }
    
}
