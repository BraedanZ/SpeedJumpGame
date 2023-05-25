using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameMaster : MonoBehaviour
{
    public static GameMaster instance;

    public Vector2 startPosition;
    int fallsInARow = 0;
    public Text punishmentForNextFallText;
    private string writtenPunishment;

    private Stack<Vector2> reachedCheckPoints;

    public Text timeCounter;

    private float startTime, elapsedTime;
    TimeSpan timePlaying;
    private string timePlayingStr;

    public GameObject winPanel;

    public GameObject gameOverlay;

    public GameObject pausePanel;

    private bool isPaused;

    public bool gamePlaying { get; private set; }

    public float timeToSpawn;

    private float timeSinceSpawn;

    private int deathCount;
    private string writtenDeathCount;
    public Text deathCountText;

    private int jumpCount;
    private string writtenJumpCount;
    public Text jumpCountText;

    private bool showingTimer;
    private bool showingDeaths;
    private bool showingJumps;
    private bool showingPunishment;
    private bool muted;


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
        reachedCheckPoints.Push(startPosition);
        gamePlaying = true;
        isPaused = false;
        showingTimer = true;
        showingDeaths = true;
        showingJumps = true;
        showingPunishment = true;
        muted = false;
        startTime = Time.time;
        writtenJumpCount = "Jumps: 0";
        writtenDeathCount = "Deaths: 0";
        // StaticClass.SetDifficulty(2);
        if (StaticClass.GetDifficulty() != 2) {
            gameOverlay.transform.Find("Punishment").GetComponent<Text>().enabled = false;
        }
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

    public Vector2 GetRespawnPoint() {
        if (StaticClass.GetDifficulty() == 2) {
            return IntendedRespawnPoint();
        } 
        
        else if (StaticClass.GetDifficulty() == 1) {
            return EasyRespawnPoint();
        }

        else {
            return reachedCheckPoints.Peek();
        }
    }

    private Vector2 IntendedRespawnPoint() {
        fallsInARow++;

        if (reachedCheckPoints.Count == 0 ) {
            fallsInARow = 0;
            UpdatePunishmentText();
            return startPosition;
        }
        if (reachedCheckPoints.Count == 1 ) {
            fallsInARow = 0;
            UpdatePunishmentText();
            return reachedCheckPoints.Pop();
        }

        double punishment = Math.Pow(2, fallsInARow - 1);
        PopStackUntilRespawnPoint(punishment);
        timeSinceSpawn = timeToSpawn;
        if (reachedCheckPoints.Count == 1) {
            fallsInARow = 0;
        }
        UpdatePunishmentText();
        return reachedCheckPoints.Pop();
    }

    private Vector2 EasyRespawnPoint() {
        if (reachedCheckPoints.Count == 0 ) {
            return startPosition;
        }
        else if (reachedCheckPoints.Count == 1 ) {
            return reachedCheckPoints.Pop();
        }
        else {
            reachedCheckPoints.Pop();
            return reachedCheckPoints.Pop();
        }
    }

    private void PopStackUntilRespawnPoint(double punishment) {
        for (int i = 0; i < punishment; i++) {
            if (reachedCheckPoints.Count > 1) {
                reachedCheckPoints.Pop();
            }
        }
    }

    public void AddCheckPoint(Vector2 checkPointPosition) {
        if (CheckIfAddingRepeatedCheckpoint(checkPointPosition)) {
            return;
        }
        DecrementFallsInARow();
        reachedCheckPoints.Push(checkPointPosition);
    }

    private bool CheckIfAddingRepeatedCheckpoint(Vector2 checkPointPosition) {
        if (reachedCheckPoints.Count != 0) {
            if (reachedCheckPoints.Peek() == checkPointPosition) {
                return true;
            }
        }
        return false;
    }

    private void DecrementFallsInARow() {
        if (timeSinceSpawn < 0) {
            if (fallsInARow > 0) {
                fallsInARow--;
                UpdatePunishmentText();
            }
        }
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

    private void UpdatePunishmentText() {
        writtenPunishment = "Punishment for next fall: " + Math.Pow(2, fallsInARow);
        punishmentForNextFallText.text = writtenPunishment;
    }

    public void StopTimer() {
        gamePlaying = false;
    }

    public void PauseGame() {
        Time.timeScale = 0;
        pausePanel.SetActive(true);
        isPaused = true;
    }

    public void UnpauseGame() {
        Time.timeScale = 1;
        pausePanel.SetActive(false);
        isPaused = false;
    }

    public bool IsPaused() {
        return isPaused;
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

    public void DifficultySettings(int difficulty) {
        // if (difficulty == 2) {
        //     SetupIntendedDifficulty();
        // } else if (difficulty == 1) {
        //     SetupEasyDifficulty();
        // } else {
        //     SetupCasualDifficulty();
        // }
        StaticClass.SetDifficulty(difficulty);
    }

    // private void SetupIntendedDifficulty() {
    //     StaticClass.SetDifficulty(2);
    // }

    // private void SetupEasyDifficulty() {
    //     StaticClass.SetDifficulty(1);
    // }

    // private void SetupCasualDifficulty() {
    //     StaticClass.SetDifficulty(0);
    // }

    public void ShowTimer() {
        if (showingTimer == true) {
            timeCounter.gameObject.SetActive(false);
            showingTimer = false;
        } else {
            timeCounter.gameObject.SetActive(true);
            showingTimer = true;
        }
    }

    public void ShowDeaths() {
        if (showingDeaths == true) {
            deathCountText.gameObject.SetActive(false);
            showingDeaths = false;
        } else {
            deathCountText.gameObject.SetActive(true);
            showingDeaths = true;
        }
    }

    public void ShowJumps() {
        if (showingJumps == true) {
            jumpCountText.gameObject.SetActive(false);
            showingJumps = false;
        } else {
            jumpCountText.gameObject.SetActive(true);
            showingJumps = true;
        }
    }

    public void ShowPunishment() {
        if (showingPunishment == true) {
            punishmentForNextFallText.gameObject.SetActive(false);
            showingPunishment = false;
        } else {
            punishmentForNextFallText.gameObject.SetActive(true);
            showingPunishment = true;
        }
    }

    public void Mute() {
        if (muted == true) {
            AudioListener.pause = false;
            muted = false;
        } else {
            AudioListener.pause = true;
            muted = true;
        }
    }
}
