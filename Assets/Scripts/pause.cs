using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class pause : MonoBehaviour
{
    public GameObject pMenu;
    bool isPaused = false;
    public void pauseGame()
    {
        if (isPaused)
        {
            Time.timeScale = 1;
            isPaused = false;
            pMenu.SetActive(false);
        }
        else
        {
            pMenu.SetActive(true);
            Time.timeScale = 0;
            isPaused = true;
        }
    }
}
