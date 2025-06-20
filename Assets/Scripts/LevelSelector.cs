using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LevelSelector : MonoBehaviour
{
    // public static int selectedLevel = 1;
    // public int level;

    // public int world;
    // public Text levelText;

    // void Start() {
    //     levelText.text = level.ToString();
    // }

    public void SetWorld(int world)
    {
        StaticClass.SetWorld(world); 
    }

    public void SetLevel(int level)
    {
        StaticClass.SetLevel(level); 
    }

    public void OpenScene()
    {
        // selectedLevel = level;
        // SceneManager.LoadScene("SampleLevel 1");
        SceneManager.LoadScene("Assets/Scenes/Worlds/" + StaticClass.GetWorld() + "/" + StaticClass.GetWorld() + "-" + StaticClass.GetLevel() + ".unity");

    }
}
