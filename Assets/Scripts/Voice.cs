using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Windows.Speech;
using System;

public class Voice : MonoBehaviour
{
    public string[] badwords;
    public string correct;

    public ConfidenceLevel confidence = ConfidenceLevel.Medium;
    public float speed = 1;

    public Text results;
    public Image target;

    protected PhraseRecognizer recognizer;
    protected string word = "";

    private void Start()
    {
        string[] t = new[] { correct };

        string[] z = new String[badwords.Length + t.Length];
        badwords.CopyTo(z, 0);
        t.CopyTo(z, badwords.Length);

        if (z != null)
        {
            recognizer = new KeywordRecognizer(z, confidence);
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
        var x = target.transform.position.x;
        var y = target.transform.position.y;


        if (word == correct)
        { 
        results.text = "You said: <b>" + correct + "</b>" + "correctly!";
            Setup.pts += 100;
        }
        

        target.transform.position = new Vector3(x, y, 0);
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