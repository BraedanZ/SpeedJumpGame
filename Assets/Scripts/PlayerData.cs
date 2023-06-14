using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class PlayerData {

    public int fallsInARow;
    public float elapsedTime;
    public int deathCount;
    public int jumpCount;
    public float[,] checkpoints;

    public PlayerData (GameMaster gm) {

        fallsInARow = gm.fallsInARow;
        elapsedTime = gm.elapsedTime;
        deathCount = gm.deathCount;
        jumpCount = gm.jumpCount;
         
        checkpoints = new float[gm.reachedCheckPoints.Count, 2];
        for (int i = 0; i < gm.reachedCheckPoints.Count; i++) {
            Vector2 temp = gm.reachedCheckPoints.Pop();
            checkpoints[i, 0] = temp.x;
            checkpoints[i, 1] = temp.y;
        }
    }
}
