using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PauseMenu : MonoBehaviour
{
    private GameMaster gm;

    public bool isPaused;
    
    private bool showingTimer = true;
    private bool showingDeaths= true;
    private bool showingJumps= true;
    private bool showingPunishment= true;
    private bool muted;

    public Text timeCounter;
    public Text deathCountText;
    public Text jumpCountText;
    public Text punishmentForNextFallText;

    void Start() {
        gm = GameObject.FindGameObjectWithTag("GM").GetComponent<GameMaster>();
    }
    
    void OnApplicationFocus(bool hasFocus) {
        if (!hasFocus) {
            PauseGame();
        }
    }

    public void PauseGame() {
        Time.timeScale = 0f;
        // gm.SavePlayer();
        this.gameObject.transform.GetChild(0).gameObject.SetActive(true);
        isPaused = true;
    }

    public void UnpauseGame() {
        Time.timeScale = 1f;
        this.gameObject.transform.GetChild(0).gameObject.SetActive(false);
        isPaused = false;
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

    public void LoadMainMenu() {
        
        SceneManager.LoadScene("MainMenu");
    }

    public void doExitGame() {
        Application.Quit();
    }

    public void Restart() {
        gm.Restart();
    }
}
