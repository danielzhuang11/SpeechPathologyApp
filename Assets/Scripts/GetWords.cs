﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using TMPro;
using UnityEngine.SceneManagement;
using UnityEngine.Networking;
using System.Timers;
using UnityEngine.Audio;
#if UNITY_EDITOR || UNITY_STANDALONE
    using UnityEngine.Windows.Speech;
#endif
public class GetWords : MonoBehaviour
{
    public Image image;
    public string cGrop;
    public string correct;
    private string group;
    public float speed = 1;
    public TextMeshProUGUI results;
    public GameObject thi;
    public static string word = "";
    public Sprite card;
    AudioSource audioSource;
    public bool updateOn = true;
    private bool sentence = false;
    public Button record;
    public Button speech;
    public SampleSpeechToText sample;
    public GameObject player;
    private Rigidbody2D rigid;
#if UNITY_EDITOR || UNITY_STANDALONE

    protected PhraseRecognizer recognizer;
#endif


    private static System.Timers.Timer aTimer;

    public void newWord()
    {
#if UNITY_EDITOR || UNITY_STANDALONE
        audioSource = gameObject.AddComponent<AudioSource>();
        audioSource.clip = Microphone.Start("", true, 10, 44100);
#endif


        updateOn = true;
        string chosen = WordBase.getRandFromCSV(group);

        if (WordBase.termData.terms[chosen][1] != null)
            StartCoroutine(WordBase.setImage(WordBase.termData.terms[chosen][1], image));
        else { image.GetComponent<Image>().sprite = card; }
        results.text = chosen;


        cGrop = WordBase.termData.terms[chosen][0];
        correct = chosen;
        string[] t = new[] { correct };
        if (group.Contains("Level 3"))
        {
            sentence = true;
            record.gameObject.SetActive(true);
            speech.gameObject.SetActive(false);

        }
        else
        {
            sentence = false; record.gameObject.SetActive(false);
#if !(UNITY_EDITOR || UNITY_STANDALONE)
            speech.gameObject.SetActive(true);
#endif
        }
#if UNITY_EDITOR || UNITY_STANDALONE
        if (word != null && !sentence)
          {
            recognizer = new KeywordRecognizer(t, ConfidenceLevel.High);
            recognizer.OnPhraseRecognized += Recognizer_OnPhraseRecognized;
            recognizer.Start();
        }
#endif
    }
#if UNITY_EDITOR || UNITY_STANDALONE
        private void Recognizer_OnPhraseRecognized(PhraseRecognizedEventArgs args)
        {
            word = args.text;

        }
#endif
    private void Start()
    {

        thi.SetActive(false);

        results.text = "Press the New Word Button";
        //difficulty = DropdownFill.difficulty;
        group = DropdownFill.group;
        if (group.Contains("Level 3"))
        {
            sentence = true;
            record.gameObject.SetActive(true);
            speech.gameObject.SetActive(false);

        }
        else { sentence = false; }

#if UNITY_EDITOR || UNITY_STANDALONE
        speech.gameObject.SetActive(false);
#endif

    }
    private void Update()
    {

        if (word.ToLower().Equals(correct.ToLower()) && updateOn == true)
        {
#if UNITY_EDITOR || UNITY_STANDALONE
            audioSource.Play();
            Microphone.End(null);
            recognizer.Stop();

#endif


            results.text = "Nice Job! You said <b>" + correct + "</b>" + " correctly!";
            globalScore.score += 1;
            globalScore.coins += 1;
            rigid = player.GetComponent<Rigidbody2D>();
            rigid.constraints = RigidbodyConstraints2D.None;
            rigid.constraints = RigidbodyConstraints2D.FreezeRotation;
#if !(UNITY_EDITOR || UNITY_STANDALONE)
                      sample.OnClickSpeaks(word);
#endif
            WordBase.termData.groupScore[cGrop] += 1;
            PlayerPrefs.SetInt(cGrop, WordBase.termData.groupScore[cGrop]);
            PlayerPrefs.SetFloat("Score", globalScore.score);
            thi.SetActive(false);

            word = "";
            spaceMove.frozen = false;
            updateOn = false;



            // SceneManager.LoadScene("Flashcards");
        }
    }



}
