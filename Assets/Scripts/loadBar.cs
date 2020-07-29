using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class loadBar : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (GameObject.FindGameObjectWithTag("loloBar"))
        {
            GameObject loadB = GameObject.FindGameObjectWithTag("loloBar");
            Image childB = loadB.transform.GetChild(0).GetComponent<Image>();
            if (globalScore.lo <= 200)
            {
                childB.fillAmount = globalScore.lo / 200;
            }
            if (globalScore.lo >= 200 || DropdownFill.ready==true)
            {
                loadB.SetActive(false);
                SceneManager.LoadScene("SelectDifficulty");
            }
        }
    }
}
