using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemyMove : MonoBehaviour
{
    private float move = 200f;
    public float speed = 3f;

    // Update is called once per frame
    void FixedUpdate()
    {
        if (movementSideScroll.movin)
        {
            move -= 1;
            if (move > 0)
            {
                GetComponent<Rigidbody2D>().velocity = new Vector2(speed, GetComponent<Rigidbody2D>().velocity.y);

            }
            else if (move < 0)
            {
                GetComponent<Rigidbody2D>().velocity = new Vector2(-speed, GetComponent<Rigidbody2D>().velocity.y);

            }
            if (move < -200)
            {
                move = 200;
            }
        }
       
    }
}
