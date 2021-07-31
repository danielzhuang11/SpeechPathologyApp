using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Instructions : MonoBehaviour
{
    public GameObject instructions;


    // Start is called before the first frame update

    public void OpenInstructions()
    {
        instructions.SetActive(true);

    }
    public void CloseInstructions()
    {
        instructions.SetActive(false);
    }
}
