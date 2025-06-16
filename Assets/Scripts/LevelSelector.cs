using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LevelSelector : MonoBehaviour
{
    public static int selectedLevel = 1;
    public int level;
    public Text levelText;

    void Start() {
        levelText.text = level.ToString();
    }

    public void OpenScene()
    {
        selectedLevel = level;
        // SceneManager.LoadScene("SampleLevel 1");
        SceneManager.LoadScene("Assets/Scenes/Worlds/1/1-" + level + ".unity");

    }
}
