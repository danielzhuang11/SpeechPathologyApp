using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Windows.Speech;
using System;
using TMPro;
using UnityEngine.SceneManagement;
public class GetWords : MonoBehaviour
{
    public Image image;
    public Sprite newSprite;
    public Image gOver;
    public int time;
    public static TermData.Terms termData;
  //  public string[] badwords;
    public string correct;

    public ConfidenceLevel confidence = ConfidenceLevel.Medium;
    public float speed = 1;

    public TextMeshProUGUI results;


    protected PhraseRecognizer recognizer;
    protected string word = "";

    public bool updateOn = true;

    void getRandFromCSV(string diff, string group)
    {

    }
    //setup

    

    private void Start()
    {
        gOver.gameObject.SetActive(false);
        image.sprite = newSprite; 
        string[] t = new[] { correct };

      //  string[] z = new string[badwords.Length + t.Length];
//badwords.CopyTo(z, 0);
      //  t.CopyTo(z, badwords.Length);
        StartCoroutine(updateOff());
        if (word != null)
        {
            recognizer = new KeywordRecognizer(z, confidence);
            recognizer.OnPhraseRecognized += Recognizer_OnPhraseRecognized;
            recognizer.Start();
        }
    }

    private void Recognizer_OnPhraseRecognized(PhraseRecognizedEventArgs args)
    {
        word = args.text;
    }

    private void Update()
    {

        //for (int i = 0; i < badwords.length; i++)
        //{
        //    if (word == badwords[i])
        //    {

        //    }
        //}

        if (word == correct && updateOn == true)
        {
            results.text = "You said: <b>" + correct + "</b>" + " correctly!";
            Setup.pts += 100;
            StopCoroutine("updateOff");
            recognizer.Stop();
           // SceneManager.LoadScene("Flashcards");
            
        }

    }

    IEnumerator updateOff()
    {
        yield return new WaitForSeconds(time);
        updateOn = false;
        gOver.gameObject.SetActive(true);
        recognizer.Stop();
     //   SceneManager.LoadScene("Flashcards");
    }

    private void OnApplicationQuit()
    {
        if (recognizer != null && recognizer.IsRunning)
        {
            recognizer.OnPhraseRecognized -= Recognizer_OnPhraseRecognized;
            recognizer.Stop();
        }
    }
}
