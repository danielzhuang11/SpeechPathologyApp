using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class tropy : MonoBehaviour
{
    public float require;
    public GameObject trphie;
    void Update()
    {
        if(globalScore.score >= require)
        {
            trphie.SetActive(true);
        }
        else
        {
            trphie.SetActive(false);
        }
    }
}
