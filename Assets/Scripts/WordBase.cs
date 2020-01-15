using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;
public static class WordBase 
{
    // Start is called before the first frame update
    public static TermData.Terms termData;


   /* public static IEnumerator setImage(string URL, Image image)
    {
        // Check internet connection
        if (Application.internetReachability == NetworkReachability.NotReachable)
        {
            yield return null;
        }

        var www = new WWW(URL);
        Debug.Log("Download image on progress");
        yield return www;

        if (string.IsNullOrEmpty(www.text))
        {
            Debug.Log("Download failed");
        }
        else
        {
            Debug.Log("Download succes");
            Texture2D texture = new Texture2D(1, 1);
            www.LoadImageIntoTexture(texture);
            Sprite sprite = Sprite.Create(texture,
                new Rect(0, 0, texture.width, texture.height),
                Vector2.one / 2);


            image.GetComponent<Image>().sprite = sprite;
        }
    }*/
    public static string getRandFromCSV(string group, string difficulty)
    {
        System.Random rnd = new System.Random();
        string chosen = termData.terms.ElementAt(rnd.Next(0,termData.terms.Count)).Key;
        Debug.Log((termData.terms[chosen][2]));
        while ((termData.terms[chosen][2] != group && group != "Any") || (termData.terms[chosen][1] != difficulty && difficulty != "Any"))
        {
            chosen = termData.terms.ElementAt(rnd.Next(0, termData.terms.Count)).Key;
        }
        return chosen;
    }

}
