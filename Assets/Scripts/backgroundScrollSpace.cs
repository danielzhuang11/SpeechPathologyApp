﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class backgroundScrollSpace : MonoBehaviour
{
    Material material;
    Vector2 offset;
    public FixedJoystick joystick;

    private float xVelocity;
    public float velLim = 5;
    private void Awake()
    {
        material = GetComponent<Renderer>().material;
    }
    void Start()
    {
        offset = new Vector2(0, 0);
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        xVelocity = joystick.Horizontal / velLim;
        offset = new Vector2(xVelocity, 0);
        material.mainTextureOffset += offset * Time.deltaTime;
    }
}
