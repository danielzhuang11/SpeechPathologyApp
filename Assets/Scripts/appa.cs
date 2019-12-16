using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class appa : MonoBehaviour
{
    public GameObject apper;
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.tag == "Player")
        {
            apper.SetActive(false);
        }
    }
}
