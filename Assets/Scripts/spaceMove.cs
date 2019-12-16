using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class spaceMove : MonoBehaviour
{
   

    public float moveSpeed = 20f;
    public FixedJoystick joystick;
    float horizontalMove = 0f;
    public float healthMax = 5f;
    public Transform player;
    public Image healthBar;
    private float health;
    public GameObject GameOver;
    public GameObject playerz;
    private int scoreSpa = 0;
    public Text score;

    void Start()
    {
        health = healthMax;
    }
    void FixedUpdate()
    {
        
        if (health <= 0)
        {
            GameOver.SetActive(true);
            playerz.SetActive(false);

        }
        if (joystick.Horizontal >= 0.2f)
        {
            horizontalMove = moveSpeed;
            GetComponent<Rigidbody2D>().velocity = new Vector2(horizontalMove, GetComponent<Rigidbody2D>().velocity.y);
            
        }
        else if (joystick.Horizontal <= -0.2f)
        {
            horizontalMove = -moveSpeed;
            GetComponent<Rigidbody2D>().velocity = new Vector2(horizontalMove, GetComponent<Rigidbody2D>().velocity.y);
            
            

        }
        else
        {
            horizontalMove = 0f;
            GetComponent<Rigidbody2D>().velocity = new Vector2(horizontalMove, GetComponent<Rigidbody2D>().velocity.y);
           
        }
        

    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.tag == "Enemy")
        {
            health -= 1;
            healthBar.fillAmount = health / healthMax;
        }
        if (collision.collider.tag == "Coin")
        {
            scoreSpa += 1;
            score.text = "Score: " + scoreSpa;
        }
    }
    
}