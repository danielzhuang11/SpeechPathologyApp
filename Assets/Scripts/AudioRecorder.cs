using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

//Use the PointerDown and PointerUP interfaces to detect a mouse down and up on a ui element
public class AudioRecorder : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    public  GameObject thi;
    AudioClip recording;
    //Keep this one as a global variable (outside the functions) too and use GetComponent during start to save resources
    AudioSource audioSource;
    private float startRecordingTime;

    //Get the audiosource here to save resources
    private void Start()
    {
        audioSource = gameObject.AddComponent<AudioSource>();
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        //End the recording when the mouse comes back up, then play it
        Microphone.End("");

        //Trim the audioclip by the length of the recording
        AudioClip recordingNew = AudioClip.Create(recording.name, (int)((Time.time - startRecordingTime) * recording.frequency), recording.channels, recording.frequency, false);
        float[] data = new float[(int)((Time.time - startRecordingTime) * recording.frequency)];
        recording.GetData(data, 0);
        recordingNew.SetData(data, 0);
        this.recording = recordingNew;

        //Play recording
        audioSource.clip = recording;
        audioSource.Play();

        thi.SetActive(false);


    }

    public void OnPointerDown(PointerEventData eventData)
    {
        //Get the max frequency of a microphone, if it's less than 44100 record at the max frequency, else record at 44100
        int minFreq;
        int maxFreq;
        int freq = 44100;
        Microphone.GetDeviceCaps("", out minFreq, out maxFreq);
        if (maxFreq < 44100)
            freq = maxFreq;

        //Start the recording, the length of 300 gives it a cap of 5 minutes
        recording = Microphone.Start("", false, 300, 44100);
        startRecordingTime = Time.time;
    }

}