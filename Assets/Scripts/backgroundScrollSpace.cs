using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class backgroundScrollSpace : MonoBehaviour
{
    Material material;
    Vector2 offset;
    public FixedJoystick joystick;

    private float xVelocity;
    public float velLim = 5;
    public bool isBlock = false;
    private float hMove;
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
        hMove = Input.GetAxisRaw("Horizontal");
        if (!isBlock)
        {
            if (hMove > 0.1f || hMove < -0.1f)
            {
                xVelocity = hMove / velLim;
            }
            else
            {
                xVelocity = joystick.Horizontal / velLim;
            }
            offset = new Vector2(xVelocity, 0);
            material.mainTextureOffset += offset * Time.deltaTime;
        }
    }
}