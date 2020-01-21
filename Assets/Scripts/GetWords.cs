﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Windows.Speech;
using System;
using TMPro;
using UnityEngine.SceneManagement;
using UnityEngine.Networking;
using System.Timers;
using UnityEngine.Audio;
public class GetWords : MonoBehaviour
{
    public Image image;
    public Image gOver;
    public int time;

    public string correct;
    private string difficulty;
    private string group;
    public ConfidenceLevel confidence = ConfidenceLevel.High;
    public float speed = 1;
    public TextMeshProUGUI results;
    public GameObject thi;
    protected PhraseRecognizer recognizer;
    protected string word = "";
   public  Sprite card;
    AudioSource audioSource;
    public bool updateOn = true;
    private static System.Timers.Timer aTimer;
    public void newWord()
    {
        audioSource = gameObject.AddComponent<AudioSource>();
        audioSource.clip = Microphone.Start("", true, 10, 44100);

        updateOn = true;
        string chosen = WordBase.getRandFromCSV(group);

        if(WordBase.termData.terms[chosen][1] != null)
            StartCoroutine(WordBase.setImage(WordBase.termData.terms[chosen][1], image));
        else { image.GetComponent<Image>().sprite = card; }
        results.text = chosen;

        correct = chosen;
        string[] t = new[] { correct };

        if (word != null)
        {
            recognizer = new KeywordRecognizer(t, confidence);
            recognizer.OnPhraseRecognized += Recognizer_OnPhraseRecognized;
            recognizer.Start();
        }
    }

    private void Start()
    {
        results.text = "Press the New Word Button";
        //difficulty = DropdownFill.difficulty;
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
            audioSource.Play();
            Microphone.End(null);


            results.text = "Nice Job! You said <b>" + correct + "</b>" + " correctly!";
            globalScore.score += 1;

            globalScore.coins += 1;
            PlayerPrefs.SetFloat("Score", globalScore.score);

            recognizer.Stop();
            updateOn = false;
            word = "";
            thi.SetActive(false);
            Time.timeScale = 1;
         

            // SceneManager.LoadScene("Flashcards");
        }
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
