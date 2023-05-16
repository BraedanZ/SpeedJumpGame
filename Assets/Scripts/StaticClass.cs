using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class StaticClass
{
    public static int difficulty;

    static void SetDifficulty(int difficulty) {
        StaticClass.difficulty = difficulty;
    }

    static int GetDifficulty() {
        return StaticClass.difficulty;
    }
}
