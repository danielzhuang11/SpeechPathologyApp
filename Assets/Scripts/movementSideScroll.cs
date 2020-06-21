﻿using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class movementSideScroll : MonoBehaviour
{
    public Animator anim;

    public float moveSpeed = 20f;
    public FixedJoystick joystick;
    float horizontalMove = 0f;
    public bool isGrounded = true;
    public float jumpForce = 5f;
    public float healthMax = 5f;
    public Transform player;
    public Image healthBar;
    private float health;
    public GameObject GameOver;
    public GameObject playerz;
    public Text coinTxt;
    public GameObject mi;
    public Transform gamPos;
    public GameObject ui;
    private float hMove;
    private float vMove;
    private Rigidbody2D rigid;

    void Start()
    {
        globalScore.coins = 0;
        health = healthMax;
        Time.timeScale = 1;
        rigid = playerz.GetComponent<Rigidbody2D>();
    }
    void FixedUpdate()
    {
        hMove = Input.GetAxisRaw("Horizontal");
        vMove = Input.GetAxisRaw("Vertical");
        if (player.position.y < -30)
        {
            health = 0;
            healthBar.fillAmount = 0;

        }
        if (health <= 0)
        {
            GameOver.SetActive(true);
            playerz.SetActive(false);
            globalScore.coins = 0;
        }
        if (joystick.Horizontal >= 0.2f || hMove >= 0.1f)
        {
            coinTxt.text = "Coin: " + globalScore.coins;
            horizontalMove = moveSpeed;
            anim.SetFloat("Speed", Mathf.Abs(horizontalMove));
            GetComponent<Rigidbody2D>().velocity = new Vector2(horizontalMove, GetComponent<Rigidbody2D>().velocity.y);
            transform.localScale = new Vector2(Mathf.Abs(transform.localScale.x), transform.localScale.y);
        }
        else if (joystick.Horizontal <= -0.2f || hMove <= -0.1f)
        {
            coinTxt.text = "Coin: " + globalScore.coins;
            horizontalMove = -moveSpeed;
            GetComponent<Rigidbody2D>().velocity = new Vector2(horizontalMove, GetComponent<Rigidbody2D>().velocity.y);
            transform.localScale = new Vector2(-Mathf.Abs(transform.localScale.x), transform.localScale.y);
            anim.SetFloat("Speed", Mathf.Abs(horizontalMove));
        }
        else
        {
            coinTxt.text = "Coin: " + globalScore.coins;
            horizontalMove = 0f;
            GetComponent<Rigidbody2D>().velocity = new Vector2(horizontalMove, GetComponent<Rigidbody2D>().velocity.y);
            anim.SetFloat("Speed", Mathf.Abs(horizontalMove));
        }
        if ((joystick.Vertical >= 0.7f || vMove >= 0.1f) && isGrounded == true)
        {
            Jump();
        }

    }
    void Jump()
    {
        gameObject.GetComponent<Rigidbody2D>().AddForce(new Vector2(0f, jumpForce), ForceMode2D.Impulse);
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.tag == "Health" && health < healthMax)
        {
            health += 1;
            healthBar.fillAmount = health / healthMax;
        }
        if (collision.collider.tag == "Coin")
        {
            ui.transform.position = new Vector3(ui.transform.position.x, ui.transform.position.y, 0);
            ui.SetActive(true);
        }
        if (collision.collider.tag == "Enemy")
        {
            health -= 1;
            healthBar.fillAmount = health / healthMax;
        }
    }
}