using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bgSpace : MonoBehaviour
{
    Material material;
    Vector2 offset;
    

    public float xVelocity;
  
    private void Awake()
    {
        material = GetComponent<Renderer>().material;
    }
    void Start()
    {
        offset = new Vector2(xVelocity, 0);
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        material.mainTextureOffset += offset * Time.deltaTime;
    }
}
