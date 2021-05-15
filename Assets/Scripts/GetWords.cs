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
    public static string correct;
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
    public int conInt = 2;
    public static float y;
#if UNITY_EDITOR || UNITY_STANDALONE
    public ConfidenceLevel conLvl = ConfidenceLevel.High;

    private String spokenText = "";
    protected PhraseRecognizer recognizer;

#endif

    [SerializeField] private TextMeshProUGUI uiText;

    [SerializeField] private GameObject newWordBtn;
    [SerializeField] private GameObject diff;

    public void GotoGameSelectorScene()
    {
        Time.timeScale = 1;
        spaceMove.frozen = false;
        pause.isPaused = false;
#if UNITY_EDITOR || UNITY_STANDALONE

        if (recognizer != null)
        {
            PhraseRecognitionSystem.Shutdown();
        }
        
#endif
        SceneManager.LoadScene("SelectGame");
    }
 
    
    public void newWord()
    {
        
        newWordBtn.SetActive(false);
        //start recording for pc
#if UNITY_EDITOR || UNITY_STANDALONE
        if(audioSource != null)
            audioSource.Stop();

        audioSource = gameObject.AddComponent<AudioSource>();

        audioSource.clip = Microphone.Start("", true, 10, 44100);
#endif

        string chosen = WordBase.getRandFromCSV(group);

        if (WordBase.termData.terms[chosen][1] != null)
            StartCoroutine(WordBase.setImage(WordBase.termData.terms[chosen][1], image));
        else { image.GetComponent<Image>().sprite = card; }
        results.text = chosen;


        cGrop = WordBase.termData.terms[chosen][0];
        correct = chosen;
        string[] t = new[] { correct };

#if UNITY_EDITOR || UNITY_STANDALONE
        
        if (word != null && !sentence)
        {
            if (PlayerPrefs.HasKey("diff"))
                conInt = PlayerPrefs.GetInt("diff");
            if (conInt == 2)
                conLvl = ConfidenceLevel.Medium;
            else if (conInt == 1)
                conLvl = ConfidenceLevel.Low;
            if (conInt == 0)
            {
               

            }
            else
            {
                recognizer = new KeywordRecognizer(t, conLvl);
                Debug.Log(conLvl);
                recognizer.OnPhraseRecognized += Recognizer_OnPhraseRecognized;
                recognizer.Start();
            }

        }
        if (group.Contains("Sentences") || conInt == 0)
        {
            sentence = true;
            record.gameObject.SetActive(true);
            speech.gameObject.SetActive(false);

            AudioRecorder.updateChosen(chosen);

        }
        else
        {
            sentence = false;
            record.gameObject.SetActive(false);
            //activate mobile record button

        }



#endif
        updateOn = true;

        if (sentence == true)
        {
            record.gameObject.SetActive(true);

        }
    }
#if UNITY_EDITOR || UNITY_STANDALONE
        private void Recognizer_OnPhraseRecognized(PhraseRecognizedEventArgs args)
        {
            word = args.text;

        }
#endif
    private void Start()
    {

        updateOn = false;
        y = thi.transform.position.y;
        thi.transform.position = new Vector3(thi.transform.position.x, -5000, -5000);


        results.text = "Press the New Word Button";
        //difficulty = DropdownFill.difficulty;
        group = DropdownFill.group;
        if (group.Contains("Sentences"))
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

#if (UNITY_ANDROID || UNITY_IOS)
        //nocheck mobile
        if (!ToggleSwitch._isOn && updateOn && word.Length>0)
        {
            newWordBtn.SetActive(true);

            results.text = "Nice Job!";
            globalScore.score += 1;
            globalScore.coins += 1;
            WordBase.termData.groupScore[cGrop] += 1;
            PlayerPrefs.SetInt(cGrop, WordBase.termData.groupScore[cGrop]);
            PlayerPrefs.SetFloat("Score", globalScore.score);
            thi.transform.position = new Vector3(thi.transform.position.x, -5000, -5000);
            word = "";
            spaceMove.frozen = false;
            updateOn = false;
        }

#endif
        
         

        //checking for both mobile and pc
        if (word.ToLower().Equals(correct.ToLower()) && updateOn == true && ToggleSwitch._isOn)
        {


#if UNITY_EDITOR || UNITY_STANDALONE

            audioSource.Play();
            Microphone.End(null);
            recognizer.Stop();

#endif
            results.text = "Nice Job! You said <b>" + correct;
            globalScore.score += 1;
            globalScore.coins += 1;
#if (UNITY_ANDROID || UNITY_IOS)
            sample.OnClickSpeaks(word);
#endif
            newWordBtn.SetActive(true);

            WordBase.termData.groupScore[cGrop] += 1;
            PlayerPrefs.SetInt(cGrop, WordBase.termData.groupScore[cGrop]);
            PlayerPrefs.SetFloat("Score", globalScore.score);
            thi.transform.position = new Vector3(thi.transform.position.x, -5000, -5000);

            word = "";
            spaceMove.frozen = false;
            updateOn = false;



            // SceneManager.LoadScene("Flashcards");
        }
    }



}