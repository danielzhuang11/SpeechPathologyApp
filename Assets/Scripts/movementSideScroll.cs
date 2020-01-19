using UnityEngine;
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

    void Start()
    {
        globalScore.coins = 0;
        health = healthMax;
    }
    void FixedUpdate()
    {
        if(player.position.y < -30)
        {
            health = 0;
            healthBar.fillAmount = 0;
          
        }
        if(health <= 0)
        {
            GameOver.SetActive(true);
            playerz.SetActive(false);
            globalScore.coins = 0;
        }
        if (joystick.Horizontal >= 0.2f)
        {
            coinTxt.text = "Coin: " + globalScore.coins;
            horizontalMove = moveSpeed;
             anim.SetFloat("Speed", Mathf.Abs(horizontalMove));
            GetComponent<Rigidbody2D>().velocity = new Vector2(horizontalMove, GetComponent<Rigidbody2D>().velocity.y);
            transform.localScale = new Vector2(1, transform.localScale.y);
            anim.SetBool("isJumping", false);
        }
        else if (joystick.Horizontal <= -0.2f)
        {
            coinTxt.text = "Coin: " + globalScore.coins;
            horizontalMove = -moveSpeed;
            GetComponent<Rigidbody2D>().velocity = new Vector2(horizontalMove, GetComponent<Rigidbody2D>().velocity.y);
            transform.localScale = new Vector2(-1, transform.localScale.y);
            anim.SetFloat("Speed", Mathf.Abs(horizontalMove));
            anim.SetBool("isJumping", false);

        }
        else
        {
            coinTxt.text = "Coin: " + globalScore.coins;
            horizontalMove = 0f;
            GetComponent<Rigidbody2D>().velocity = new Vector2(horizontalMove, GetComponent<Rigidbody2D>().velocity.y);
            anim.SetFloat("Speed", Mathf.Abs(horizontalMove));
            anim.SetBool("isJumping", false);
        }
        if(joystick.Vertical >= 0.7f && isGrounded == true)
        {
            Jump();
           anim.SetBool("isJumping", true);
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
            Time.timeScale = 0;
            ui.SetActive(true);
        }
        if (collision.collider.tag == "Enemy")
        {
            health -= 1;
            healthBar.fillAmount = health / healthMax;
        }
    }
}