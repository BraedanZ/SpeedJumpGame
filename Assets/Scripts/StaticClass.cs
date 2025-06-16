using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class StaticClass
{
    private static int difficulty = 4;

    private static int world = 1;

    private static int level = 1;

    public static void SetDifficulty(int difficulty)
    {
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
        return StaticClass.world;
    }
    
    public static void SetLevel(int level)
    {
        StaticClass.level = level;
    }

    public static int GetLevel() {
        return StaticClass.level;
    }
}
