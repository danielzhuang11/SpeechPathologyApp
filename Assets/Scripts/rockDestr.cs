using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class rockDestr : MonoBehaviour
{
    public Transform player;
    public GameObject rock;
    
    // Update is called once per frame
    void Update()
    {
        if (player.position.y < -30)
        {
            Destroy(rock);
        }
     }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.tag == "Player")
        {
            Destroy(rock);
        }
        
    }
}
