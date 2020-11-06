using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using TMPro;
public class spaceMove : MonoBehaviour
{
   

    public float moveSpeed = 20f;
    public FixedJoystick joystick;
    float horizontalMove = 0f;
    public float healthMax = 5f;
    public Transform player;
    public Image healthBar;
    public Image img;
    private float health;
    public GameObject GameOver;
    public GameObject playerz;
    public Text score;
    public GameObject ui;
    private float hMove;
    public TextMeshProUGUI results;
    public Rigidbody2D plaa;


    public static bool frozen = false;
    void Start()
    {
        globalScore.coins = 0;

        health = healthMax;
        Time.timeScale = 1;
    }
    void FixedUpdate()
    {
        hMove = Input.GetAxisRaw("Horizontal");
        
        if (health <= 0)
        {
            GameOver.SetActive(true);
            playerz.SetActive(false);
            // globalScore.score += scoreSpa;
            globalScore.coins = 0;


        }
        if (ui.transform.position.z<-2000)
        {
            plaa.constraints = RigidbodyConstraints2D.None;
            plaa.constraints = RigidbodyConstraints2D.FreezePositionY | RigidbodyConstraints2D.FreezeRotation;
            if (joystick.Horizontal >= 0.2f || hMove >= 0.1f)
            {
                score.text = "Score: " + globalScore.coins;
                horizontalMove = moveSpeed;
                GetComponent<Rigidbody2D>().velocity = new Vector2(horizontalMove, GetComponent<Rigidbody2D>().velocity.y);

            }
            else if (joystick.Horizontal <= -0.2f || hMove <= -.1f)
            {

                score.text = "Score: " + globalScore.coins;
                horizontalMove = -moveSpeed;
                GetComponent<Rigidbody2D>().velocity = new Vector2(horizontalMove, GetComponent<Rigidbody2D>().velocity.y);

            }
            else
            {
                score.text = "Score: " + globalScore.coins;
                horizontalMove = 0f;
                GetComponent<Rigidbody2D>().velocity = new Vector2(horizontalMove, GetComponent<Rigidbody2D>().velocity.y);

            }
        }
        else
        {
            plaa.constraints = RigidbodyConstraints2D.FreezePositionX | RigidbodyConstraints2D.FreezePositionY | RigidbodyConstraints2D.FreezeRotation;
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
            if (health < healthMax)
            {
                health += 1;
                healthBar.fillAmount = health / healthMax;
            }
            ui.transform.position = new Vector3(ui.transform.position.x, GetWords.y, 0);
            score.text = "Score: " + globalScore.coins;
            ui.SetActive(true);
            img.sprite = null;

            results.text = "Press the New Word Button";
            frozen = true;
        }
        
    }
    
}