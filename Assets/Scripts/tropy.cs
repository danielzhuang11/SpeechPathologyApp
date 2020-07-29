using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class tropy : MonoBehaviour
{
    public float require;
    public GameObject trphie;
    public GameObject trSh;
    void Update()
    {
   //     Debug.Log("updating");

        if(globalScore.score >= require)
        {
           // Debug.Log("score");
            trphie.SetActive(true);
            trSh.SetActive(false);
        }
       
    }
}
