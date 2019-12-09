using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Setup : MonoBehaviour
{
    public static Setup Instance;

    public static int pts = 0;
    public static string[] email;
    public TextMeshProUGUI ptsD;

    //todo: pull from saved file

    // Start is called before the first frame update
    void Start()
    {
        ptsD.text = "Points: " + pts;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
