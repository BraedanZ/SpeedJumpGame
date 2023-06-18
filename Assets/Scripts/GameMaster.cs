using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameMaster : MonoBehaviour
{
    public static GameMaster instance;

    public Player player;

    public Vector2 spawnOffset;

    public Vector2 startPosition;
    public int fallsInARow = 0;
    public Text punishmentForNextFallText;
    private string writtenPunishment;

    public Stack<Vector2> reachedCheckPoints;

    public Text timeCounter;

    private float startTime;
    public float elapsedTime;
    public float loadedTime;
    TimeSpan timePlaying;
    private string timePlayingStr;

    public GameObject winPanel;

    public GameObject gameOverlay;

    public GameObject pausePanel;

    private bool isPaused;

    public bool gamePlaying { get; private set; }

    public float timeToSpawn;

    private float timeSinceSpawn;

    public int deathCount;
    private string writtenDeathCount;
    public Text deathCountText;

    public int jumpCount;
    private string writtenJumpCount;
    public Text jumpCountText;

    private bool showingTimer;
    private bool showingDeaths;
    private bool showingJumps;
    private bool showingPunishment;
    private bool muted;

    private bool waterZone = false;
    public float timeInWater;
    public float waterTimeDifferential;

    Vector3 loadedPlayerPosition;
    Vector2 loadedPlayerVelocity;

    public GameObject pauseButton;

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
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
        reachedCheckPoints = new Stack<Vector2>();
        // reachedCheckPoints.Push(startPosition);
        gamePlaying = true;
        isPaused = false;
        showingTimer = true;
        showingDeaths = true;
        showingJumps = true;
        showingPunishment = true;
        muted = false;
        startTime = Time.time;
        // writtenJumpCount = "Jumps: 0";
        // writtenDeathCount = "Deaths: 0";
        // StaticClass.SetDifficulty(4);
        if (StaticClass.GetDifficulty() == 1) {
            gameOverlay.transform.Find("Punishment").GetComponent<Text>().enabled = false;
        } else if (StaticClass.GetDifficulty() == 0) {
            gameOverlay.transform.Find("Punishment").GetComponent<Text>().enabled = false;
            gameOverlay.transform.Find("TimeCounterText").GetComponent<Text>().enabled = false;
            gameOverlay.transform.Find("DeathCount").GetComponent<Text>().enabled = false;
            gameOverlay.transform.Find("JumpCount").GetComponent<Text>().enabled = false;
        }
        LoadPlayer();
        UpdatePunishmentText();
        UpdateJumpCount();
        UpdateDeathCount();
    }

    public void Restart() {
        // Start();
        // SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        // LoadDemoScene();
        reachedCheckPoints = new Stack<Vector2>();
        gamePlaying = true;
        startTime = Time.time;
        loadedTime = 0f;

        WipeSave();
        UpdatePunishmentText();
        UpdateJumpCount();
        UpdateDeathCount();
        player.Restart();
    }

    void Update() {
        UpdateTimer();
        if (timeToSpawn > 0) {
            timeSinceSpawn -= Time.deltaTime;
        }
    }

    void OnApplicationFocus(bool hasFocus) {
        if (!hasFocus) {
            PauseGame();
        }
    }

    void OnApplicationQuit() {
        SavePlayer();
    }

    public Vector2 GetRespawnPoint() {
        if (StaticClass.GetDifficulty() == 4) {
            return twoRespawnPoint();
        } 
        
        else if (StaticClass.GetDifficulty() == 3) {
            return fourRespawnPoint();
        } 

        else if (StaticClass.GetDifficulty() == 2) {
            return IntendedRespawnPoint();
        } 
        
        else if (StaticClass.GetDifficulty() == 1) {
            return EasyRespawnPoint();
        }

        else {
            return CasualRespawnPoint();
        }
    }

    private Vector2 twoRespawnPoint() {
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

        double punishment = 2 * fallsInARow;

        PopStackUntilRespawnPoint(punishment);
        timeSinceSpawn = timeToSpawn;
        if (reachedCheckPoints.Count == 1) {
            fallsInARow = 0;
        }
        UpdatePunishmentText();
        return reachedCheckPoints.Pop();
    }

    private Vector2 fourRespawnPoint() {
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

        double punishment;
        if (fallsInARow == 1) {
            punishment = 2;
        } else {
            punishment = 4 * (fallsInARow - 1);
        }

        PopStackUntilRespawnPoint(punishment);
        timeSinceSpawn = timeToSpawn;
        if (reachedCheckPoints.Count == 1) {
            fallsInARow = 0;
        }
        UpdatePunishmentText();
        return reachedCheckPoints.Pop();
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

        double punishment = Math.Pow(2, fallsInARow);
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

    private Vector2 CasualRespawnPoint() {
        if (reachedCheckPoints.Count == 0 ) {
            return startPosition;
        } else if (reachedCheckPoints.Count == 1) {
            return reachedCheckPoints.Pop();
        }
        return reachedCheckPoints.Peek();
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

    private void UpdateTimer() {
        if (waterZone) {
            timeInWater += Time.deltaTime;
            waterTimeDifferential = timeInWater / 3;
        }
        if (gamePlaying) {
            elapsedTime = Time.time - startTime + waterTimeDifferential + loadedTime;
            timePlaying = TimeSpan.FromSeconds(elapsedTime);

            timePlayingStr = timePlaying.ToString("mm':'ss'.'ff");
            timeCounter.text = timePlayingStr;
        }
    }

    private void UpdatePunishmentText() {
        if (StaticClass.GetDifficulty() == 4) {
            UpdatePunishmentTextTwo();
        } else if (StaticClass.GetDifficulty() == 3) {
            UpdatePunishmentTextFour();
        } else if (StaticClass.GetDifficulty() == 2) {
            UpdatePunishmentTextIntended();
        }
    }

    private void UpdatePunishmentTextTwo() {
        writtenPunishment = "x" + 2 * (fallsInARow + 1);
        punishmentForNextFallText.text = writtenPunishment;
    }

    private void UpdatePunishmentTextFour() {
        double punishment;
        if (fallsInARow == 0) {
            punishment = 2;
        } else {
            punishment = 4 * (fallsInARow);
        }
        writtenPunishment = "x" + punishment;
        punishmentForNextFallText.text = writtenPunishment;
    }

    private void UpdatePunishmentTextIntended() {
        writtenPunishment = "x" + Math.Pow(2, fallsInARow + 1);
        punishmentForNextFallText.text = writtenPunishment;
    }

    public void StopTimer() {
        gamePlaying = false;
    }

    public void PauseGame() {
        Time.timeScale = 0;
        SavePlayer();
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

    public void ActivateWaterZone() {
        Time.timeScale = 0.75f;
        waterZone = true;
    }

    public void DeactivateWaterZone() {
        Time.timeScale = 1f;
        waterZone = false;
    }

    public bool IsWaterZone() {
        return waterZone;
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
        UpdateDeathCount();
    }

    private void UpdateDeathCount() {
        writtenDeathCount = "Deaths: " + deathCount;
        deathCountText.text = writtenDeathCount;
    }

    public int GetDeathCount() {
        return deathCount;
    }

    public void IncramentJumps() {
        jumpCount++;
        UpdateJumpCount();
    }

    private void UpdateJumpCount() {
        writtenJumpCount = "Jumps: " + jumpCount;
        jumpCountText.text = writtenJumpCount;
    }

    public int GetJumpCount() {
        return jumpCount;
    }

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

    public int GetFallsInARow() {
        return fallsInARow;
    }

    public void doExitGame() {
        Application.Quit();
    }

    public void SavePlayer() {
        SaveSystem.SavePlayer(this);
    }

    public void LoadPlayer() {
        PlayerData data = SaveSystem.LoadPlayer();

        fallsInARow = data.fallsInARow;
        loadedTime = data.loadedTime;
        deathCount = data.deathCount;
        jumpCount = data.jumpCount;

        loadedPlayerPosition.x = data.position[0];
        loadedPlayerPosition.y = data.position[1];
        loadedPlayerPosition.z = data.position[2];

        loadedPlayerVelocity.x = data.velocity[0];
        loadedPlayerVelocity.y = data.velocity[1];

        for (int i = 0; i <= data.checkpoints.GetUpperBound(0); i++) {
            Vector2 checkpoint = new Vector2(data.checkpoints[i, 0], data.checkpoints[i, 1]);
            reachedCheckPoints.Push(checkpoint);
        }
    }

    public void WipeSave() {
        fallsInARow = 0;
        elapsedTime = 0f;
        deathCount = 0;
        jumpCount = 0;
        loadedPlayerPosition = new Vector3(0f, 0f, 0f);
        loadedPlayerVelocity = new Vector2(0f, 0f);

        SavePlayer();
    }

    public Vector3 GetLoadedPosition() {
        return loadedPlayerPosition;
    }

    public Vector2 GetLoadedVelocity() {
        return loadedPlayerVelocity;
    }
}
