using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Setup : MonoBehaviour
{
    public static Setup Instance;
    public GameObject Settings;
    public GameObject Instructions;

    public GameObject Controls;

    public static string[] email;
    public TextMeshProUGUI ptsD;


    void Start()
    {
        globalScore.start();
        this.GetComponent<Loader>().Load();

        GameObject.Find("Settings").SetActive(false);

    }

    void Update()
    {
    }

    public void CloseSettings()
    {
        GameObject.Find("Settings").SetActive(false);
    }


    public void OpenSettings()
    {
        Settings.SetActive(true);

    }
    public void OpenControls()
    {
        Controls.SetActive(true);
    }
    public void CloseControls()
    {
        GameObject.Find("Controls").SetActive(false);
    }
}
