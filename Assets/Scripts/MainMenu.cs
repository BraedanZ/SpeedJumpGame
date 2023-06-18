using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public void LoadDemoScene() {
        SceneManager.LoadScene("DemoMap");
    }

    public void DifficultySettings(int difficulty) {
        StaticClass.SetDifficulty(difficulty);
    }

    public void doExitGame() {
        Application.Quit();
        Debug.Log("QUIT");
    }
}
