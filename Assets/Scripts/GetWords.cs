using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Windows.Speech;
using System;
using TMPro;
using UnityEngine.SceneManagement;
using UnityEngine.Networking;
using System.Timers;
public class GetWords : MonoBehaviour
{
    public Image image;
    public Image gOver;
    public int time;

    public string correct;
    private string difficulty;
    private string group;
    public ConfidenceLevel confidence = ConfidenceLevel.Low;
    public float speed = 1;
    public TextMeshProUGUI results;
    public GameObject thi;
    public GameObject mi;
    public GameObject player;
    protected PhraseRecognizer recognizer;
    protected string word = "";

    public bool updateOn = true;
    private static System.Timers.Timer aTimer;

    public void newWord()
    {

        results.text = "Speak Now";
        updateOn = true;
        string chosen = WordBase.getRandFromCSV(group,difficulty);

        StartCoroutine(WordBase.setImage(WordBase.termData.terms[chosen][0], image));

        correct = chosen;
        string[] t = new[] { correct };

        if (word != null)
        {
            gOver.gameObject.SetActive(false);
            recognizer = new KeywordRecognizer(t, confidence);
            recognizer.OnPhraseRecognized += Recognizer_OnPhraseRecognized;
            recognizer.Start();
        }
    }

    private void Start()
    {
        gOver.gameObject.SetActive(false);
        difficulty = DropdownFill.difficulty;
        group = DropdownFill.group;
    }

    private void Recognizer_OnPhraseRecognized(PhraseRecognizedEventArgs args)
    {
        word = args.text;
    }
   
    private void Update()
    {
        
        if (word == correct && updateOn == true)
        {
           
            results.text = "You said: <b>" + correct + "</b>" + " correctly!";
            globalScore.score += 1;

            globalScore.coins += 1;
            PlayerPrefs.SetFloat("Score", globalScore.score);
           

            recognizer.Stop();
            updateOn = false;
            word = "";
            thi.SetActive(false);
            mi.SetActive(true);
            player.SetActive(true);

            // SceneManager.LoadScene("Flashcards");
        }
    }


   /* private void outOfTime()
    {
        updateOn = false;

        gOver.gameObject.SetActive(true);
        recognizer.Stop();

        //   SceneManager.LoadScene("Flashcards");
    }*/
    private void OnApplicationQuit()
    {
        if (recognizer != null && recognizer.IsRunning)
        {
            recognizer.OnPhraseRecognized -= Recognizer_OnPhraseRecognized;
            recognizer.Stop();
        }
    }
}
