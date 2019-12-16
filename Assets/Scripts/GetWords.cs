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

    public string correct;
    public string difficulty;
    public string group;
    public ConfidenceLevel confidence = ConfidenceLevel.Medium;
    public float speed = 1;

    public TextMeshProUGUI results;



    protected PhraseRecognizer recognizer;
    protected string word = "";

    public bool updateOn = true;

    void getRandFromCSV(string diff, string group)
    {
        GameObject.Find("GameController").GetComponent<Loader>().Load();


        foreach (KeyValuePair<string, string[]> kvp in termData.terms)
        {
            
            Debug.Log("Key = {0}, Value = {1}" + kvp.Key+ kvp.Value);
        }
      //  switch 
      //  termData.terms
    }
    //setup

    

    private void Start()
    {
        
        getRandFromCSV(difficulty, group);

        gOver.gameObject.SetActive(false);
        image.sprite = newSprite; 
        string[] t = new[] { correct };

        StartCoroutine(updateOff());
        if (word != null)
        {
            recognizer = new KeywordRecognizer(t, confidence);
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
