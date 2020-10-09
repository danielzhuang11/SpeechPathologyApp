using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class globalScore
{
    public static float score;
    public static float coins;
    public static int lo=0;

    public static void start()
    {
        score = PlayerPrefs.GetFloat("Score");


    }
    
}
