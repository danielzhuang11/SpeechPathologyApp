using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DropdownFill : MonoBehaviour
{
    public static string difficulty;
    public static string group ;
    // Start is called before the first frame update
    public Dropdown dropdownd;
    public Dropdown dropdowng;
    List<string> groups = new List<string>() { "R Initial", "R Final", "S Initial", "S Final", "L Initial", "L Final", "R Blends Initial", "L blends Initial", "Any" };
    List<string> difficulties = new List<string>() { "Easy", "Medium", "Hard", "Any" };
    void Start()
    {
        PopulateListG();
        PopulateListD();
        difficulty = "Easy";
        group = "R Initial";

    }
    public void groupindexChanged(int index)
    {
        group = groups[index];
    }

    public void dindexChanged(int index)
    {
        difficulty = difficulties[index];
    }
    void PopulateListG()
    {
        dropdowng.AddOptions(groups);
    }
    void PopulateListD()
    {
        dropdownd.AddOptions(difficulties);
    }
   
}
