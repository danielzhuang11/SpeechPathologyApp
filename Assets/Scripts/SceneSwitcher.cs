using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneSwitcher : MonoBehaviour
{
    public void GotoDifficultyScene()
    {
        SceneManager.LoadScene("SelectDifficulty");
    }
    public void GotoGameSelectorScene()
    {
        SceneManager.LoadScene("SelectGame");
    }

    public void GotoGameScrollerScene()
    {
        SceneManager.LoadScene("gameScroller");
    }

    public void GotoFlashcardScene()
    {
        SceneManager.LoadScene("Flashcards");
    }


    public void GotoMenuScene()
    {
        SceneManager.LoadScene("Menu");
    }
}