using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;
using UnityEngine.SceneManagement;

public static class StaticClass
{
    private static int difficulty = 0;

    private static int world = 0;

    private static int level = 0;

    public static void SetDifficulty(int difficulty)
    {
        UnityEngine.Debug.Log(difficulty);
        StaticClass.difficulty = difficulty;
    }

    public static int GetDifficulty()
    {
        return StaticClass.difficulty;
    }

    public static void SetWorld(int world)
    {
        StaticClass.world = world;
    }

    public static int GetWorld()
    {
        // Debug.Log(world);
        return StaticClass.world;
    }

    public static void SetLevel(int level)
    {
        StaticClass.level = level;
    }

    public static int GetLevel()
    {
        return StaticClass.level;
    }

    public static void IncrementLevel()
    {
        if (level != 8)
        {
            level++;
        }
        else if (level == 8 && world != 8)
        {
            world++;
            level = 1;
        }
        else if (level == 8 && world == 8)
        {
            world = 1;
            level = 1;
        }
    }
}
