using UnityEngine;
using UnityEngine.SceneManagement;
using System.Net;
using System.IO;

public class SceneSwitcher : MonoBehaviour
{
    public string GetHtmlFromUri(string resource)
    {  
        string html = string.Empty;
        HttpWebRequest req = (HttpWebRequest)WebRequest.Create(resource);
        try
        {
            using (HttpWebResponse resp = (HttpWebResponse)req.GetResponse())
            {
                bool isSuccess = (int)resp.StatusCode < 299 && (int)resp.StatusCode >= 200;
                if (isSuccess)
                {
                    using (StreamReader reader = new StreamReader(resp.GetResponseStream()))
                    {
                        
                        char[] cs = new char[80];
                        reader.Read(cs, 0, cs.Length);
                        foreach (char ch in cs)
                        {
                            html += ch;
                        }
                    }
                }
            }
        }
        catch
        {
            return "";
        }
        return html;
    }
    public void GotoDifficultyScene()
    {
        Scene scene = SceneManager.GetActiveScene();
        Time.timeScale = 1;
        string HtmlText = GetHtmlFromUri("http://google.com");
        if (HtmlText == "")
        {
            //No connection
            Debug.Log("no Connection");
            if (scene.name.Equals("MainMenu"))
            {
               GameObject intCh = GameObject.FindGameObjectWithTag("intCC");
                GameObject childInt = intCh.transform.GetChild(0).gameObject;
                childInt.SetActive(true);
            }
        }
        else if (!HtmlText.Contains("schema.org/WebPage"))
        {
            //Redirecting since the beginning of googles html contains that 
            //phrase and it was not found
        }
        else
        {
            //success
            if (scene.name.Equals("MainMenu"))
            {
                GameObject intCh = GameObject.FindGameObjectWithTag("intCC");
                GameObject childInt = intCh.transform.GetChild(0).gameObject;
                childInt.SetActive(false);

                if (globalScore.lo < 200 && DropdownFill.ready == false)
                {
                    GameObject loBar = GameObject.FindGameObjectWithTag("loBar");
                    GameObject childBar = loBar.transform.GetChild(0).gameObject;
                    childBar.SetActive(true);
                }
                else
                {
                    SceneManager.LoadScene("SelectDifficulty");
                }
            }
            else
            {
                SceneManager.LoadScene("SelectDifficulty");
            }
            
        }
    }
    public void GotoGameSelectorScene()
    {
        Time.timeScale = 1;
        spaceMove.frozen = false;
        pause.isPaused = false;
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