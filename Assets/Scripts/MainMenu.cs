using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public void LoadDemoScene() {
        SceneManager.LoadScene("DemoMap");
    }

    public void LoadSecretMap() {
        SceneManager.LoadScene("SecretMap");
    }

    public void LoadMap1() {
        SceneManager.LoadScene("Map1");
    }

    public void LoadLevelSelect() {
        SceneManager.LoadScene("Level Select");
    }

    public void DifficultySettings(int difficulty) {
        StaticClass.SetDifficulty(difficulty);
    }

    public void doExitGame() {
        Application.Quit();
        Debug.Log("QUIT");
    }
}
