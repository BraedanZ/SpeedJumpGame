using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameMaster : MonoBehaviour
{
    public static GameMaster instance;

    public Vector2 startPosiiton;
    int fallsInARow = 0;
    private Stack<Vector2> reachedCheckPoints;

    public Text timeCounter;

    private float startTime, elapsedTime;
    TimeSpan timePlaying;

    public GameObject winPanel;

    public GameObject gameOverlay;

    public bool gamePlaying { get; private set; }

    public float timeToSpawn;

    private float timeSinceSpawn;

    void Awake() {
        instance = this;
        // if (instance == null) {
        //     instance = this;
        //     DontDestroyOnLoad(instance);
        // } else {
        //     Destroy(gameObject);
        // }
    }

    void Start() {
        reachedCheckPoints = new Stack<Vector2>();
        reachedCheckPoints.Push(startPosiiton);
        gamePlaying = true;
        startTime = Time.time;
    }

    void Update() {
        UpdateTimer();
        if (timeToSpawn > 0) {
            timeSinceSpawn -= Time.deltaTime;
        }
    }

    public void LoadDemoScene() {
        SceneManager.LoadScene("DemoMap");
    }

    public Vector2 GetTopCheckPoint() {
        fallsInARow++;
        if (reachedCheckPoints.Count == 0 ) {
            return startPosiiton;
        }
        if (reachedCheckPoints.Count == 1 ) {
            return reachedCheckPoints.Pop();
        }
        double punishment = Math.Pow(2, fallsInARow - 1);
        print("from Get top check point: " + fallsInARow);
        print("Punishment: " + punishment);
        for (int i = 0; i < punishment; i++) {
            if (reachedCheckPoints.Count > 1) {
                reachedCheckPoints.Pop();
            }
        }
        timeSinceSpawn = timeToSpawn;
        return reachedCheckPoints.Pop();
    }

    public void AddCheckPoint(Vector2 checkPointPosition) {
        if (reachedCheckPoints.Count != 0) {
            if (reachedCheckPoints.Peek() == checkPointPosition) {
                return;
            }
        }
        if (timeSinceSpawn < 0) {
            fallsInARow = 0;
            print("from add check point: " + fallsInARow);
        }
            
        reachedCheckPoints.Push(checkPointPosition);
    }

    public void Restart() {
        Start();
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        // LoadDemoScene();
    }

    private void UpdateTimer() {
        if (gamePlaying) {
            elapsedTime = Time.time - startTime;
            timePlaying = TimeSpan.FromSeconds(elapsedTime);

            string timePlayingStr = timePlaying.ToString("mm':'ss'.'ff");
            timeCounter.text = timePlayingStr;
        }
    }

    public void StopTimer() {
        gamePlaying = false;
    }

    public void ShowGameOverScreen() {
        GameMaster.instance.StopTimer();
        string timePlayingStr = timePlaying.ToString("mm':'ss'.'ff");
        winPanel.transform.Find("FinalTime").GetComponent<Text>().text = timePlayingStr;
        winPanel.SetActive(true);
        gameOverlay.SetActive(false);
    }
}
