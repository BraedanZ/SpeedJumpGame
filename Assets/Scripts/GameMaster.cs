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

    public CheckpointController checkpointController;

    public Text timeCounter;

    private float startTime;
    public float elapsedTime;
    public float loadedTime;
    TimeSpan timePlaying;
    private string timePlayingStr;

    public GameObject winPanel;

    public GameObject gameOverlay;

    public GameObject pausePanel;

    public bool gamePlaying { get; private set; }

    public int deathCount;
    private string writtenDeathCount;
    public Text deathCountText;

    public int jumpCount;
    private string writtenJumpCount;
    public Text jumpCountText;

    private bool waterZone = false;
    public float timeInWater;
    public float waterTimeDifferential;

    // Vector3 loadedPlayerPosition;
    // Vector2 loadedPlayerVelocity;

    public GameObject pauseButton;

    public GameObject nextLevelButton;

    public Scene scene;

    public float mapLength;

    void Awake() {
        instance = this;
    }

    void Start() {
        SetStartVariables();
        // LoadPlayer();
        UpdateJumpCount();
        UpdateDeathCount();
    }

    private void SetStartVariables() {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
        checkpointController = GameObject.FindGameObjectWithTag("CheckpointController").GetComponent<CheckpointController>();
        scene = SceneManager.GetActiveScene();
        gamePlaying = true;
        startTime = Time.time;
        // StaticClass.SetDifficulty(4);
        if (StaticClass.GetDifficulty() == 1) {
            gameOverlay.transform.Find("Punishment").GetComponent<Text>().enabled = false;
        } else if (StaticClass.GetDifficulty() == 0) {
            gameOverlay.transform.Find("Punishment").GetComponent<Text>().enabled = false;
            gameOverlay.transform.Find("TimeCounterText").GetComponent<Text>().enabled = false;
            gameOverlay.transform.Find("DeathCount").GetComponent<Text>().enabled = false;
            gameOverlay.transform.Find("JumpCount").GetComponent<Text>().enabled = false;
        }
    }

    public void Restart() {
        checkpointController.Restart();
        // ResetStartVariables();
        // WipeSave();
        UpdateJumpCount();
        UpdateDeathCount();
        player.Restart();
    }

    // private void ResetStartVariables() {
    //     gamePlaying = true;
    //     startTime = Time.time;
    //     loadedTime = 0f;
    // }

    void Update() {
        UpdateTimer();
    }

    // void OnApplicationQuit() {
    //     SavePlayer();
    // }

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

    public void StopTimer() {
        gamePlaying = false;
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
        nextLevelButton.SetActive(true);
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

    public void doExitGame() {
        Application.Quit();
    }

    // public void SavePlayer() {
    //     SaveSystem.SavePlayer(this);
    // }

    // public void LoadPlayer() {
    //     PlayerData data = SaveSystem.LoadPlayer(scene.name);

    //     checkpointController.fallsInARow = data.fallsInARow;
    //     checkpointController.UpdatePunishmentText();
    //     loadedTime = data.loadedTime;
    //     deathCount = data.deathCount;
    //     jumpCount = data.jumpCount;

    //     loadedPlayerPosition.x = data.position[0];
    //     loadedPlayerPosition.y = data.position[1];
    //     loadedPlayerPosition.z = data.position[2];

    //     loadedPlayerVelocity.x = data.velocity[0];
    //     loadedPlayerVelocity.y = data.velocity[1];

    //     for (int i = 0; i <= data.checkpoints.GetUpperBound(0); i++) {
    //         Vector2 checkpoint = new Vector2(data.checkpoints[i, 0], data.checkpoints[i, 1]);
    //         checkpointController.reachedCheckPoints.Push(checkpoint);
    //     }
    // }

    // public void WipeSave() {
    //     checkpointController.fallsInARow = 0;
    //     elapsedTime = 0f;
    //     deathCount = 0;
    //     jumpCount = 0;
    //     loadedPlayerPosition = new Vector3(0f, 0f, 0f);
    //     loadedPlayerVelocity = new Vector2(0f, 0f);

    //     SavePlayer();
    // }

    // public Vector3 GetLoadedPosition() {
    //     return loadedPlayerPosition;
    // }

    // public Vector2 GetLoadedVelocity() {
    //     return loadedPlayerVelocity;
    // }

    public void PlayAgain() {
        SceneManager.LoadScene("SampleLevel 1");
    }

    public void PlayNextLevel() {
        StaticClass.IncrementLevel();
        SceneManager.LoadScene("Assets/Scenes/Worlds/" + StaticClass.GetWorld() + "/" + StaticClass.GetWorld() + "-" + StaticClass.GetLevel() + ".unity");
    }

    public void MainMenu() {
        SceneManager.LoadScene("MainMenu");
    }
}
