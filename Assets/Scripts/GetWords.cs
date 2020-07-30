using System.Collections;
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
    public ConfidenceLevel conLvl = ConfidenceLevel.High;
    private int conInt = 2;
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

            AudioRecorder.updateChosen(chosen);

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
            if(PlayerPrefs.HasKey("diff"))
                conInt = PlayerPrefs.GetInt("diff");
            if (conInt == 2)
                conLvl = ConfidenceLevel.High;
            else if(conInt == 1)
                conLvl = ConfidenceLevel.Medium;
            else if(conInt == 0)
            {
                conLvl = ConfidenceLevel.Low;
            }
            recognizer = new KeywordRecognizer(t, conLvl);
            Debug.Log(conLvl);
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

        thi.transform.position = new Vector3(thi.transform.position.x, thi.transform.position.y, -50000);


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


            results.text = "Nice Job! You said <b>" + correct + "</b>" + " correctly! " + "\n" + "Your audio is being played back";
            globalScore.score += 1;
            globalScore.coins += 1;
#if !(UNITY_EDITOR || UNITY_STANDALONE)
            sample.OnClickSpeaks(word);
#endif
            WordBase.termData.groupScore[cGrop] += 1;
            PlayerPrefs.SetInt(cGrop, WordBase.termData.groupScore[cGrop]);
            PlayerPrefs.SetFloat("Score", globalScore.score);
            thi.transform.position = new Vector3(thi.transform.position.x, thi.transform.position.y, -50000);

            word = "";
            spaceMove.frozen = false;
            updateOn = false;



            // SceneManager.LoadScene("Flashcards");
        }
    }



}