using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DropdownFill : MonoBehaviour
{
    public static string difficulty;
    public static string group ;
    public static bool ready=false;
    // Start is called before the first frame update
    //public Dropdown dropdownd;
    public static List<string> groups;
    public Dropdown dropdowng;
   //new List<string>() { "R Initial", "R Final", "S Initial", "S Final", "L Initial", "L Final", "R Blends Initial", "L blends Initial", "Any" };
    //List<string> difficulties = new List<string>() { "Level 1", "Level 2", "Level 3", "Any" };
    void Start()
    {
        PopulateListG();
      //  PopulateListD();
        group = groups[0];

    }
    public void groupindexChanged(int index)
    {
        group = groups[index];
    }

   /* public void dindexChanged(int index)
    {
        difficulty = difficulties[index];
    }*/
    void PopulateListG()
    {
        var texture = new Texture2D(1, 1); // creating texture with 1 pixel
        texture.SetPixel(50, 50, Color.red); // setting to this pixel some color
        texture.Apply(); //applying texture. necessarily
        var item = new Dropdown.OptionData(Sprite.Create(texture, new Rect(0, 0, texture.width, texture.height), new Vector2(0, 0)));

      //  dropdowng.options.Add(item);
        dropdowng.AddOptions(groups);
        ready = true;

    }
   /* void PopulateListD()
    {
        dropdownd.AddOptions(difficulties);
    }*/
   
}
