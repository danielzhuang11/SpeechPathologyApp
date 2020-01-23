using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;

public class statDisp : MonoBehaviour
{
    public Text[] stDisp;
    void FixedUpdate()
    {
        for(int x =0; x<WordBase.termData.groupScore.Count; x++)
        {
            stDisp[x].text = WordBase.termData.groupScore.ElementAt(x).Key + ": " + WordBase.termData.groupScore.ElementAt(x).Value;
        }
    }
}
