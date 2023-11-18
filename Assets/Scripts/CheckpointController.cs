using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CheckpointController : MonoBehaviour
{
    private GameMaster gm;

    // private SunQuotes sunQuotes;

    public Vector2 startPosition = Vector2.zero;

    public Stack<Vector2> reachedCheckPoints;

    public int fallsInARow = 1;

    // public Text punishmentForNextFallText;
    // private string writtenPunishment;

    private float timeToSpawn = 0.3f;
    private float timeSinceSpawn;

    void Start() {
        gm = GameObject.FindGameObjectWithTag("GM").GetComponent<GameMaster>();
        reachedCheckPoints = new Stack<Vector2>();
    }

    void Update() {
        UpdateTimeSinceSpawn();
    }

    private void UpdateTimeSinceSpawn() {
        if (timeToSpawn > 0) {
            timeSinceSpawn -= Time.deltaTime;
        }
    }

    public void Restart() {
        reachedCheckPoints = new Stack<Vector2>();
    }

    public Vector2 GetRespawnPoint() {
        if (reachedCheckPoints.Count == 0 ) {
            return startPosition;
        }
        else if (reachedCheckPoints.Count == 1 ) {
            reachedCheckPoints.Pop();
            return startPosition;
        }
        else {
            reachedCheckPoints.Pop();
            return reachedCheckPoints.Pop();
        }
    }

    public void AddCheckPoint(Vector2 checkPointPosition) {
        if (CheckIfAddingRepeatedCheckpoint(checkPointPosition)) {
            return;
        }
        reachedCheckPoints.Push(checkPointPosition);
        // sunQuotes.MadeJump();
    }

    private bool CheckIfAddingRepeatedCheckpoint(Vector2 checkPointPosition) {
        if (reachedCheckPoints.Count != 0) {
            if (reachedCheckPoints.Peek() == checkPointPosition) {
                return true;
            }
        }
        return false;
    }


    // void Start()
    // {
    //     gm = GameObject.FindGameObjectWithTag("GM").GetComponent<GameMaster>();
    //     // sunQuotes = GameObject.FindGameObjectWithTag("Sun").GetComponent<SunQuotes>();
    //     reachedCheckPoints = new Stack<Vector2>();
    //     UpdatePunishmentText();
    // }

    // void Update() {
    //     UpdateTimeSinceSpawn();
    // }

    // private void UpdateTimeSinceSpawn() {
    //     if (timeToSpawn > 0) {
    //         timeSinceSpawn -= Time.deltaTime;
    //     }
    // }

    // public void Restart() {
    //     reachedCheckPoints = new Stack<Vector2>();
    //     UpdatePunishmentText();
    // }

    // public Vector2 GetRespawnPoint() {
    //     if (StaticClass.GetDifficulty() == 4) {
    //         return twoRespawnPoint();
    //     } 
        
    //     else if (StaticClass.GetDifficulty() == 3) {
    //         return fourRespawnPoint();
    //     } 

    //     else if (StaticClass.GetDifficulty() == 2) {
    //         return IntendedRespawnPoint();
    //     } 
        
    //     else if (StaticClass.GetDifficulty() == 1) {
    //         return EasyRespawnPoint();
    //     }

    //     else {
    //         return CasualRespawnPoint();
    //     }
    // }

    // private Vector2 twoRespawnPoint() {
    //     fallsInARow++;

    //     if (reachedCheckPoints.Count == 0 ) {
    //         fallsInARow = 0;
    //         UpdatePunishmentText();
    //         return startPosition;
    //     }
    //     if (reachedCheckPoints.Count == 1 ) {
    //         fallsInARow = 0;
    //         UpdatePunishmentText();
    //         return startPosition;
    //     }

    //     double punishment = 2 * fallsInARow;

    //     PopStackUntilRespawnPoint(punishment);
    //     timeSinceSpawn = timeToSpawn;
    //     if (reachedCheckPoints.Count == 1 || reachedCheckPoints.Count == 0) {
    //         fallsInARow = 0;
    //         reachedCheckPoints = new Stack<Vector2>();
    //         UpdatePunishmentText();
    //         return startPosition;
    //     }
    //     UpdatePunishmentText();
    //     return reachedCheckPoints.Pop();
    // }

    // private Vector2 fourRespawnPoint() {
    //     fallsInARow++;

    //     if (reachedCheckPoints.Count == 0 ) {
    //         fallsInARow = 0;
    //         UpdatePunishmentText();
    //         return startPosition;
    //     }
    //     if (reachedCheckPoints.Count == 1 ) {
    //         fallsInARow = 0;
    //         UpdatePunishmentText();
    //         return reachedCheckPoints.Pop();
    //     }

    //     double punishment;
    //     if (fallsInARow == 1) {
    //         punishment = 2;
    //     } else {
    //         punishment = 4 * (fallsInARow - 1);
    //     }

    //     PopStackUntilRespawnPoint(punishment);
    //     timeSinceSpawn = timeToSpawn;
    //     if (reachedCheckPoints.Count == 1) {
    //         fallsInARow = 0;
    //     }
    //     UpdatePunishmentText();
    //     return reachedCheckPoints.Pop();
    // }

    // private Vector2 IntendedRespawnPoint() {
    //     fallsInARow++;

    //     if (reachedCheckPoints.Count == 0 ) {
    //         fallsInARow = 0;
    //         UpdatePunishmentText();
    //         return startPosition;
    //     }
    //     if (reachedCheckPoints.Count == 1 ) {
    //         fallsInARow = 0;
    //         UpdatePunishmentText();
    //         return reachedCheckPoints.Pop();
    //     }

    //     double punishment = Math.Pow(2, fallsInARow);
    //     PopStackUntilRespawnPoint(punishment);
    //     timeSinceSpawn = timeToSpawn;
    //     if (reachedCheckPoints.Count == 1) {
    //         fallsInARow = 0;
    //     }
    //     UpdatePunishmentText();
    //     return reachedCheckPoints.Pop();
    // }

    // private Vector2 EasyRespawnPoint() {
    //     if (reachedCheckPoints.Count == 0 ) {
    //         return startPosition;
    //     }
    //     else if (reachedCheckPoints.Count == 1 ) {
    //         return reachedCheckPoints.Pop();
    //     }
    //     else {
    //         reachedCheckPoints.Pop();
    //         return reachedCheckPoints.Pop();
    //     }
    // }

    // private Vector2 CasualRespawnPoint() {
    //     if (reachedCheckPoints.Count == 0 ) {
    //         return startPosition;
    //     } else if (reachedCheckPoints.Count == 1) {
    //         return reachedCheckPoints.Pop();
    //     }
    //     return reachedCheckPoints.Peek();
    // }

    // private void PopStackUntilRespawnPoint(double punishment) {
    //     for (int i = 0; i < punishment; i++) {
    //         if (reachedCheckPoints.Count > 1) {
    //             reachedCheckPoints.Pop();
    //         }
    //     }
    // }

    // public void AddCheckPoint(Vector2 checkPointPosition) {
    //     if (CheckIfAddingRepeatedCheckpoint(checkPointPosition)) {
    //         return;
    //     }
    //     DecrementFallsInARow();
    //     reachedCheckPoints.Push(checkPointPosition);
    //     // sunQuotes.MadeJump();
    // }

    // private bool CheckIfAddingRepeatedCheckpoint(Vector2 checkPointPosition) {
    //     if (reachedCheckPoints.Count != 0) {
    //         if (reachedCheckPoints.Peek() == checkPointPosition) {
    //             return true;
    //         }
    //     }
    //     return false;
    // }

    // private void DecrementFallsInARow() {
    //     if (timeSinceSpawn < 0) {
    //         if (fallsInARow > 0) {
    //             fallsInARow--;
    //             UpdatePunishmentText();
    //         }
    //     }
    // }

    // public void UpdatePunishmentText() {
    //     if (StaticClass.GetDifficulty() == 4) {
    //         UpdatePunishmentTextTwo();
    //     } else if (StaticClass.GetDifficulty() == 3) {
    //         UpdatePunishmentTextFour();
    //     } else if (StaticClass.GetDifficulty() == 2) {
    //         UpdatePunishmentTextIntended();
    //     }
    // }

    // private void UpdatePunishmentTextTwo() {
    //     writtenPunishment = "x" + 2 * (fallsInARow + 1);
    //     punishmentForNextFallText.text = writtenPunishment;
    // }

    // private void UpdatePunishmentTextFour() {
    //     double punishment;
    //     if (fallsInARow == 0) {
    //         punishment = 2;
    //     } else {
    //         punishment = 4 * (fallsInARow);
    //     }
    //     writtenPunishment = "x" + punishment;
    //     punishmentForNextFallText.text = writtenPunishment;
    // }

    // private void UpdatePunishmentTextIntended() {
    //     writtenPunishment = "x" + Math.Pow(2, fallsInARow + 1);
    //     punishmentForNextFallText.text = writtenPunishment;
    // }

    // public int GetFallsInARow() {
    //     return fallsInARow;
    // }
}
