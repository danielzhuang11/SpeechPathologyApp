using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class backgroundScrollSpace : MonoBehaviour
{
    Material material;
    Vector2 offset;

    public float xVelocity, yVelocity;
    private void Awake()
    {
        material = GetComponent<Renderer>().material;
    }
    void Start()
    {
        offset = new Vector2(xVelocity, yVelocity);
    }

    // Update is called once per frame
    void Update()
    {
        material.mainTextureOffset += offset * Time.deltaTime;
    }
}
