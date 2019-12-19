using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class tropy : MonoBehaviour
{
    public float require;
    public GameObject trphie;
    void FixedUpdate()
    {
        Debug.Log("updating");
        Debug.Log(globalScore.score);
        if(globalScore.score >= require)
        {
            Debug.Log("score");
            trphie.SetActive(true);
        }
       
    }
}
