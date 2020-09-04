using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpaceEnemyMove : MonoBehaviour
{
    public GameObject shot;
    private float count;
    private float move = 50f;
    public GameObject target;
    // Start is called before the first frame update
    void Start()
    {
        count = 0;
        GetComponent<Rigidbody2D>().gravityScale = 0.1f;
        GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezeRotation;
    }

    // Update is called once per frame
    private void FixedUpdate()
    {
        count += Time.deltaTime;
        if (count > (2.5f))
        {
            Instantiate(shot, transform.position, Quaternion.identity);
            count = 0;
        }
       /* Vector3 targ = target.transform.position;
            targ.z = 0f;

            Vector3 objectPos = transform.position;
            targ.x = targ.x - objectPos.x;
            targ.y = targ.y - objectPos.y;

            float angle = Mathf.Atan2(targ.y, targ.x) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.Euler(new Vector3(0, 0, angle)); */
            move -= 1;
            if (move > 0)
            {
                GetComponent<Rigidbody2D>().velocity = new Vector2(5, GetComponent<Rigidbody2D>().velocity.y);
            }
            else if (move < 0)
            {
                GetComponent<Rigidbody2D>().velocity = new Vector2(-5, GetComponent<Rigidbody2D>().velocity.y);

            }
            if (move < -50)
            {
                move = 50;
            }
        
    }
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Wall"))
        {
            Destroy(gameObject);

        }
    }
}
