using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameMaster : MonoBehaviour
{
    private static GameMaster instance;

    public Vector2 startPosiiton;
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
        reachedCheckPoints.Push(startPosiiton);
    }

    public Vector2 GetTopCheckPoint() {
        fallsInARow++;
        if (reachedCheckPoints.Count == 0 ) {
            return startPosiiton;
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

    public void Restart() {
        Start();
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
