using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class PlayerData {

    public int fallsInARow;
    public float loadedTime;
    public int deathCount;
    public int jumpCount;
    public float[,] checkpoints;

    public float[] position;
    public float[] velocity;

    public PlayerData (GameMaster gm) {

        fallsInARow = gm.fallsInARow;
        loadedTime = gm.elapsedTime;
        deathCount = gm.deathCount;
        jumpCount = gm.jumpCount;


        Stack<Vector2> test = new Stack<Vector2>(gm.reachedCheckPoints);

        checkpoints = new float[test.Count, 2];
        for (int i = 0; i < test.Count; i++) {
            Vector2 temp = test.Pop();
            checkpoints[i, 0] = temp.x;
            checkpoints[i, 1] = temp.y;
        }

        position = new float[3];
        Vector3 playerPosition = gm.player.GetPosition();
        position[0] = playerPosition.x;
        position[1] = playerPosition.y;
        position[2] = playerPosition.z;

        velocity = new float[3];
        Vector3 playerVelocity = gm.player.GetVelocity();
        velocity[0] = playerVelocity.x;
        velocity[1] = playerVelocity.y;
        velocity[2] = playerVelocity.z;
    }
}
