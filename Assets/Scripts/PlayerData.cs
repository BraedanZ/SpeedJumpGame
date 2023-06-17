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


        Stack<Vector2> copyOfReachedCheckPoints = new Stack<Vector2>(new Stack<Vector2>(gm.reachedCheckPoints));

        checkpoints = new float[copyOfReachedCheckPoints.Count, 2];
        for (int i = copyOfReachedCheckPoints.Count - 1; i >= 0; i--) {
            Vector2 checkpoint = copyOfReachedCheckPoints.Pop();
            checkpoints[i, 0] = checkpoint.x;
            checkpoints[i, 1] = checkpoint.y;
        }

        position = new float[3];
        Vector3 playerPosition = gm.player.GetPosition();
        position[0] = playerPosition.x;
        position[1] = playerPosition.y;
        position[2] = playerPosition.z;

        velocity = new float[2];
        Vector2 playerVelocity = gm.player.GetVelocity();
        velocity[0] = playerVelocity.x;
        velocity[1] = playerVelocity.y;
    }
}
