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

    void Start()
    {
        PopulateListG();
        group = groups[0];

    }
    public void groupindexChanged(int index)
    {
        group = groups[index];
    }
    void PopulateListG()
    {
        var texture = new Texture2D(1, 1); // creating texture with 1 pixel
        texture.SetPixel(50, 50, Color.red); // setting to this pixel some color
        texture.Apply(); //applying texture. necessarily
        var item = new Dropdown.OptionData(Sprite.Create(texture, new Rect(0, 0, texture.width, texture.height), new Vector2(0, 0)));

        dropdowng.AddOptions(groups);
        ready = true;

    }

   
}
