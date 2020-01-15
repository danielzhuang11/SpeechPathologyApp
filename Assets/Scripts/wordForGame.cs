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
public class wordForGame : MonoBehaviour
{
    public Image image;
    public Image gOver;
    public int time;

    public string correct;
    private string difficulty;
    private string group;
    public ConfidenceLevel confidence = ConfidenceLevel.Medium;
    public float speed = 1;
    public TextMeshProUGUI results;
    public GameObject you;
    protected PhraseRecognizer recognizer;
    protected string word = "";
    public GameObject player;
    public GameObject other;

    public bool updateOn = true;
    private static System.Timers.Timer aTimer;

    public void newWord()
    {
        updateOn = true;
        string chosen = WordBase.getRandFromCSV(group, difficulty);

        //StartCoroutine(WordBase.setImage(WordBase.termData.terms[chosen][0], image));
        results.text = word;
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

            recognizer.Stop();
            updateOn = false;
            player.SetActive(true);
            other.SetActive(true);
            Destroy(you);


            // SceneManager.LoadScene("Flashcards");
        }
    }
    private void outOfTime()
    {
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

