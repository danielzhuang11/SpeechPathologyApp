using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class backgroundScrollSpace : MonoBehaviour
{
    Material material;
    Vector2 offset;
    public FixedJoystick joystick;

    private float xVelocity;
    public float velLim = 5;
    public bool isBlock = false;
    private float hMove;
    public GameObject gaOv;
    public GameObject flashContain;
    public Rigidbody2D play;
    private float oldPos;
    public Transform pos;
    private void Awake()
    {   
        material = GetComponent<Renderer>().material;
    }
    void Start()
    {
        oldPos = pos.position.x;
        offset = new Vector2(0, 0);
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (!gaOv.activeInHierarchy && flashContain.transform.position.z < -2000)
        {
            
            //hMove = Input.GetAxisRaw("Horizontal");
            hMove = (pos.position.x - oldPos) * 10;
            //Debug.Log("hMove: " + hMove + " input: " + Input.GetAxisRaw("Horizontal"));
            if (oldPos != pos.position.x)
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
        
        oldPos = pos.position.x;
    }
}