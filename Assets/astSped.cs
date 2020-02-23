using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class astSped : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (spaceMove.scoreSpa % 3 == 0) {
            GetComponent<Rigidbody2D>().gravityScale = 1 + spaceMove.scoreSpa / 3;
                }
    }
}
