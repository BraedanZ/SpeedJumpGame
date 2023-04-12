using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameMaster : MonoBehaviour
{
    private static GameMaster instance;

    public Vector2 lastCheckPointPosition;
    int fallsInARow = 0;
    private Stack<Vector2> reachedCheckPoints;

    void Awake() {
        
        if (instance == null) {
            instance = this;
            DontDestroyOnLoad(instance);
        } else {
            Destroy(gameObject);
        }
    }

    void Start() {
        reachedCheckPoints = new Stack<Vector2>();
        reachedCheckPoints.Push(lastCheckPointPosition);
    }

    public Vector2 GetTopCheckPoint() {
        fallsInARow++;
        if (reachedCheckPoints.Count == 0 ) {
            return lastCheckPointPosition;
        }
        if (reachedCheckPoints.Count == 1 ) {
            return reachedCheckPoints.Pop();
        }
        reachedCheckPoints.Pop();
        return reachedCheckPoints.Pop();
    }

    public void AddCheckPoint(Vector2 checkPointPosition) {
        if (reachedCheckPoints.Count != 0) {
                if (reachedCheckPoints.Peek() == checkPointPosition) {
                    return;
            }
        }
        
        reachedCheckPoints.Push(checkPointPosition);
        fallsInARow = 0;
    }
}
