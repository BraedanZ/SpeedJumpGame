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
    private string timePlayingStr;

    public GameObject winPanel;

    public GameObject gameOverlay;

    public bool gamePlaying { get; private set; }

    public float timeToSpawn;

    private float timeSinceSpawn;

    private int deathCount;
    private string writtenDeathCount;
    public Text deathCountText;

    private int jumpCount;
    private string writtenJumpCount;
    public Text jumpCountText;


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
        writtenJumpCount = "Jumps: 0";
        writtenDeathCount = "Deaths: 0";
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
        print("on Respawn " + fallsInARow);
        if (reachedCheckPoints.Count == 0 ) {
            return startPosiiton;
        }
        if (reachedCheckPoints.Count == 1 ) {
            return reachedCheckPoints.Pop();
        }
        double punishment = Math.Pow(2, fallsInARow - 1);
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
            // fallsInARow = 0;
            if (fallsInARow > 0) {
                fallsInARow--;
            }
            print("on add checkpoint " + fallsInARow);
        }
            
        reachedCheckPoints.Push(checkPointPosition);
        // fallsInARow--;
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

            timePlayingStr = timePlaying.ToString("mm':'ss'.'ff");
            timeCounter.text = timePlayingStr;
        }
    }

    public void StopTimer() {
        gamePlaying = false;
    }

    public void ShowGameOverScreen() {
        GameMaster.instance.StopTimer();
        winPanel.transform.Find("FinalTime").GetComponent<Text>().text = timePlayingStr;
        winPanel.transform.Find("FinalJumps").GetComponent<Text>().text = writtenJumpCount;
        winPanel.transform.Find("FinalDeaths").GetComponent<Text>().text = writtenDeathCount;
        winPanel.SetActive(true);
        gameOverlay.SetActive(false);


    }

    public void IncramentDeath() {
        deathCount++;
        writtenDeathCount = "Deaths: " + deathCount;
        deathCountText.text = writtenDeathCount;
    }

    public int GetDeathCount() {
        return deathCount;
    }

    public void IncramentJumps() {
        jumpCount++;
        writtenJumpCount = "Jumps: " + jumpCount;
        jumpCountText.text = writtenJumpCount;
    }

    public int GetJumpCount() {
        return jumpCount;
    }
}
