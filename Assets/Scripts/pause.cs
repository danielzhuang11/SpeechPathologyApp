using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class pause : MonoBehaviour
{
    public GameObject pMenu;
    public static bool isPaused = false;
    public void pauseGame()
    {
        
        if (isPaused)
        {
            Debug.Log("PogChamp");
            Time.timeScale = 1;
            isPaused = false;
            pMenu.SetActive(false);
        }
        else
        {
            Debug.Log("Pepega");
            isPaused = true;
            pMenu.SetActive(true);          
            Time.timeScale = 0;
        }
    }
}
