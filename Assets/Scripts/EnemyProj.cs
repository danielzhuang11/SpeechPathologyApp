using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyProj : MonoBehaviour
{
    public float speed;
    Vector3 mousePosition;
    Vector3 direction;
    public GameObject target;

    void Start()
    {
        GetComponent<Rigidbody2D>().gravityScale = 2;

      /*  Vector3 targ = target.transform.position;
        targ.z = 0f;

        Vector3 objectPos = transform.position;
        targ.x = targ.x - objectPos.x;
        targ.y = targ.y - objectPos.y;

        float angle = Mathf.Atan2(targ.y, targ.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(new Vector3(0, 0, angle));*/

        Destroy(gameObject, 2);

    }

    void Update()
    {
        // transform.position -= transform.position * 2 * Time.deltaTime;
         //GetComponent<Rigidbody2D>().AddForce(transform.up * 0.1f * 10);

    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Wall"))
        {
            Destroy(gameObject);

        }
    }

}
