using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;

public class move : MonoBehaviour
{
    float dirX;
    public float moveSpeed = 5f, jumpForce = 700f;
    Rigidbody2D rb;
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        dirX = CrossPlatformInput.GetAxis("Horizontal");
        if (CrossPlatformInputManager.GetButtonDown("Jump"))
            DoJump();
    }
    void FixedUpdate()
    {
        rb.velocity = new Vector2(dirX * moveSpeed, rb.velocity.y);
    }
    public void DoJump()
    {
        if (rb.velocity.y == 0)

            rb.AddForce(new Vector2(0, jumpForce), ForceMode2D.Force);
    }
}
