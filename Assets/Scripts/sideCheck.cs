using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class sideCheck : MonoBehaviour
{
    private GameObject qua;


    // Update is called once per frame
    void Update()
    {
        qua = GameObject.FindGameObjectWithTag("Qua");
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.tag == "Player")
        {
            qua.GetComponent<backgroundScrollSpace>().isBlock = true;
        }
    }
    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.collider.tag == "Player")
        {
            qua.GetComponent<backgroundScrollSpace>().isBlock = false;
        }
    }
}