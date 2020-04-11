using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class sideCheck : MonoBehaviour
{
    public GameObject qua;


    // Update is called once per frame
    void Update()
    {

    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.tag == "Ground" || collision.collider.tag == "Enemy")
        {
            qua.GetComponent<backgroundScrollSpace>().isBlock = true;
        }
    }
    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.collider.tag == "Ground" || collision.collider.tag == "Enemy")
        {
            qua.GetComponent<backgroundScrollSpace>().isBlock = false;
        }
    }
}