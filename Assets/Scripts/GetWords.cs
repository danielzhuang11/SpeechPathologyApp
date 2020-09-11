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
    private int conInt = 2;
    public static float y;
#if UNITY_EDITOR || UNITY_STANDALONE
    public ConfidenceLevel conLvl = ConfidenceLevel.High;

    private String spokenText = "";
    protected PhraseRecognizer recognizer;
    protected DictationRecognizer recognizer2;

#endif

    [SerializeField] private TextMeshProUGUI uiText;
    [SerializeField] private float mainTimer;

    [SerializeField] private GameObject newWordBtn;
    [SerializeField] private GameObject diff;

    private float timer;
    private bool canCount = true;
    private bool doOnce = false;

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
        if (recognizer2 != null)
        {
            recognizer2.Dispose();
        }
#endif
        SceneManager.LoadScene("SelectGame");
    }
    public void Resetbtn()
    {
        timer = mainTimer;
        canCount = true;
        doOnce = false;
    }
    public void stop()
    {
        canCount = false;
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
        if (group.Contains("Sentences"))
        {
            sentence = true;
            record.gameObject.SetActive(true);
            speech.gameObject.SetActive(false);

            AudioRecorder.updateChosen(chosen);

        }
        else
        {
            sentence = false; record.gameObject.SetActive(false);
            //activate mobile record button
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
            if(conLvl == ConfidenceLevel.Low)
            {
                recognizer2 = new DictationRecognizer();

                recognizer2.DictationResult += (text, confidence) =>
                {
                    Debug.LogFormat("Dictation result: {0}", text);
                    spokenText = text;
                    //m_Recognitions.text += text + "\n";
                };
               

                recognizer2.Start();

            }
            else
            {
                recognizer = new KeywordRecognizer(t, conLvl);
                Debug.Log(conLvl);
                recognizer.OnPhraseRecognized += Recognizer_OnPhraseRecognized;
                recognizer.Start();
            }
           
        }

#endif
        updateOn = true;

        Resetbtn();
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
        timer = mainTimer;

        updateOn = false;
        y = thi.transform.position.y;
        thi.transform.position = new Vector3(thi.transform.position.x, -5000, -5000);


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

#if !(UNITY_EDITOR || UNITY_STANDALONE)
        //nocheck mobile
        if(!ToggleSwitch._isOn && updateOn && word.Length>0)
        {
            newWordBtn.SetActive(true);

            results.text = "Nice Job! Your audio is being played back";
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
        if (timer >= 0.0f && canCount  && updateOn)
        {
#if UNITY_EDITOR || UNITY_STANDALONE
            if (conLvl != ConfidenceLevel.Low)
#endif
            {
                timer -= Time.deltaTime;
                uiText.text = timer.ToString("F");
            }
        }
        else if (timer <= 0.0f && !doOnce && updateOn == true)
        {
            canCount = false;
            doOnce = true;
            uiText.text = "0.00";
            timer = 0.0f;
            results.text = "You did not answer in time";
            newWordBtn.SetActive(true);

            if (sentence == true)
            {
                record.gameObject.SetActive(false);

            }
            updateOn = false;

        }
#if UNITY_EDITOR || UNITY_STANDALONE
        //no check - pc
        if (spokenText.Length > 0 && updateOn == true)
        {
            newWordBtn.SetActive(true);

            audioSource.Play();
            Microphone.End(null);
            recognizer2.Stop();
            results.text = "Nice Job! Your audio is being played back";
            globalScore.score += 1;
            globalScore.coins += 1;
            WordBase.termData.groupScore[cGrop] += 1;
            PlayerPrefs.SetInt(cGrop, WordBase.termData.groupScore[cGrop]);
            PlayerPrefs.SetFloat("Score", globalScore.score);
            thi.transform.position = new Vector3(thi.transform.position.x, -5000, -5000);
            word = "";
            spaceMove.frozen = false;
            spokenText = "";
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
            results.text = "Nice Job! You said <b>" + correct + "</b>" + " correctly! " + "\n" + "Your audio is being played back";
            globalScore.score += 1;
            globalScore.coins += 1;
#if !(UNITY_EDITOR || UNITY_STANDALONE)
            sample.OnClickSpeaks(word);
#endif
            newWordBtn.SetActive(true);
            stop();

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