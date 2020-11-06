using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class deleteOpen : MonoBehaviour
{
    public GameObject delMenu;
    private List<string> groList = new List<string>();
    
    public void openDelete()
    {
        delMenu.SetActive(true);
    }
    public void closeDelete()
    {
        delMenu.SetActive(false);
    }
    public void clearAll()
    {
        groList = new List<string>(WordBase.termData.groupScore.Keys);
        PlayerPrefs.SetFloat("Score", 0);
        globalScore.score = 0;
        globalScore.coins = 0;
       
        for(int x = 0; x < WordBase.termData.groupScore.Count; x++)
        {
            
            WordBase.termData.groupScore[groList[x]] = 0;
            PlayerPrefs.SetInt(groList[x], 0);
           
        }
    }
}
