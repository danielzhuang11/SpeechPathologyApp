using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class astSped : MonoBehaviour
{
   

    // Update is called once per frame
    void Update()
    {
        if (globalScore.coins % 3 == 0) {
             GetComponent<Rigidbody2D>().gravityScale = 1 + globalScore.coins / 3; 

        }

    }
  
}
