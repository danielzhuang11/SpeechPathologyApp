using UnityEngine;
using UnityEngine.UI;
using TextSpeech;

public class SampleSpeechToText : MonoBehaviour
{
    // public InputField inputLocale;
    public GetWords words;
     public float pitch;
     public float rate;

    /* public Text txtLocale;
     public Text txtPitch;
     public Text txtRate;*/
    void Start()
    {
        Setting("en-US");
        SpeechToText.instance.onResultCallback = OnResultSpeech;
    }


    public void StartRecording()
    {
#if UNITY_EDITOR
#else
        SpeechToText.instance.StartRecording("Speak any");
#endif
    }
    public void OnClickSpeaks(string word)
    {
        TextToSpeech.instance.StartSpeak(word);
    }
    public void OnClickStopSpeak()
    {
        TextToSpeech.instance.StopSpeak();
    }
    public void StopRecording()
    {
#if UNITY_EDITOR
        OnResultSpeech("Not support in editor.");
#else
        SpeechToText.instance.StopRecording();
#endif
#if UNITY_IOS
#endif
    }
    void OnResultSpeech(string _data)
    {
        GetWords.word = _data;
#if UNITY_IOS
#endif
    }
    public void Setting(string code)
    {
        SpeechToText.instance.Setting(code);
        TextToSpeech.instance.Setting(code, pitch, rate);

    }

}
