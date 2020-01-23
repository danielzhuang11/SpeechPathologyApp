using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class globalScore
{
    public static float score;
    public static float coins;

    public static void start()
    {
        score = PlayerPrefs.GetFloat("Score");


    }
}
