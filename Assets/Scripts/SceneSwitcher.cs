using UnityEngine;
using UnityEngine.SceneManagement;


public class SceneSwitcher : MonoBehaviour
{
    public void GotoDifficultyScene()
    {
        Time.timeScale = 1;

        SceneManager.LoadScene("SelectDifficulty");
    }
    public void GotoGameSelectorScene()
    {
        Time.timeScale = 1;

        spaceMove.frozen = false;
        SceneManager.LoadScene("SelectGame");
    }

    public void GotoGameScrollerScene()
    {
        Time.timeScale = 1;

        SceneManager.LoadScene("gameScroller");
    }
    public void GotoSpaceScene()
    {
        Time.timeScale = 1;

        SceneManager.LoadScene("spaceGame");
    }

    public void GotoFlashcardScene()
    {
        Time.timeScale = 1;

        SceneManager.LoadScene("Flashcards");
    }


    public void GotoMenuScene()
    {
        Time.timeScale = 1;

        SceneManager.LoadScene("MainMenu");
    }
    public void GotoRewardScene()
    {
        Time.timeScale = 1;

        SceneManager.LoadScene("rewards");
    }
    public void GotoStatsScene()
    {
        Time.timeScale = 1;

        SceneManager.LoadScene("stats");
    }
    public void doExitGame()
    {
        Time.timeScale = 1;

        Application.Quit();
    }


}