using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class StaticClass
{
    private static int difficulty = 4;

    public static void SetDifficulty(int difficulty) {
        StaticClass.difficulty = difficulty;
    }

    public static int GetDifficulty() {
        return StaticClass.difficulty;
    }
}
