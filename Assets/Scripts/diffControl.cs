﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class diffControl : MonoBehaviour
{
    public GameObject Easy;
    public GameObject Med;
    public GameObject Hard;
    public void easy()
    {
        PlayerPrefs.SetInt("diff", 0);
        Easy.SetActive(false);
        Med.SetActive(true);
        Hard.SetActive(true);
    }
    public void medium()
    {
        PlayerPrefs.SetInt("diff", 1);
        Easy.SetActive(true);
        Med.SetActive(false);
        Hard.SetActive(true);
    }
    public void hard()
    {
        PlayerPrefs.SetInt("diff", 2);
        Easy.SetActive(true);
        Med.SetActive(true);
        Hard.SetActive(false);
    }
}
