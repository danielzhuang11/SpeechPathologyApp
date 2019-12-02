using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneSwitcher : MonoBehaviour
{
    public void GotoDifficultyScene()
    {
        SceneManager.LoadScene("SelectDifficulty");
    }

    public void GotoMenuScene()
    {
        SceneManager.LoadScene("Menu");
    }
}