﻿using UnityEngine;
using UnityEngine.SceneManagement;


public class SceneSwitcher : MonoBehaviour
{
    public void GotoDifficultyScene()
    {
        SceneManager.LoadScene("SelectDifficulty");
    }
    public void GotoGameSelectorScene()
    {
        spaceMove.frozen = false;
        SceneManager.LoadScene("SelectGame");
    }

    public void GotoGameScrollerScene()
    {
        SceneManager.LoadScene("gameScroller");
    }
    public void GotoSpaceScene()
    {
        SceneManager.LoadScene("spaceGame");
    }

    public void GotoFlashcardScene()
    {
        SceneManager.LoadScene("Flashcards");
    }


    public void GotoMenuScene()
    {
        SceneManager.LoadScene("MainMenu");
    }
    public void GotoRewardScene()
    {
        SceneManager.LoadScene("rewards");
    }
    public void GotoStatsScene()
    {
        SceneManager.LoadScene("stats");
    }
    public void doExitGame()
    {
        Application.Quit();
    }


}