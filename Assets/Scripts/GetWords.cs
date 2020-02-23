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
using UnityEngine.Audio;
public class GetWords : MonoBehaviour
{
    public Image image;
    public Image gOver;
    public int time;
    public string cGrop;
    public string correct;
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
    private bool sentence = false;
    public Button record;
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

       
        cGrop = WordBase.termData.terms[chosen][0];
        correct = chosen;
        string[] t = new[] { correct };
        if (group.Contains("Level 3"))
        {
            sentence = true;
            record.gameObject.SetActive(true);
        }
        else { sentence = false; record.gameObject.SetActive(false); }
        if (word != null && !sentence)
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
        if (group.Contains("Level 3"))
        {
            sentence = true;
            record.gameObject.SetActive(true);
        }
        else { sentence = false; }
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
            //Speech.instance.Say(word);
            Microphone.End(null);

            results.text = "Nice Job! You said <b>" + correct + "</b>" + " correctly!";
            globalScore.score += 1;

            globalScore.coins += 1;
            Debug.Log(cGrop);
            WordBase.termData.groupScore[cGrop] += 1;
            PlayerPrefs.SetInt(cGrop, WordBase.termData.groupScore[cGrop]);
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
