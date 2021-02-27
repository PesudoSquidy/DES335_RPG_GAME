using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Arrow : MonoBehaviour
{

    [SerializeField]
    private float speed;

    private Rigidbody2D rb;

    [SerializeField]
    private float lifeTime;

    [SerializeField]
    private int damage;

    // Start is called before the first frame update
    void Start()
    {
        if (rb == null)
            rb = gameObject.GetComponent<Rigidbody2D>();

        rb.velocity = transform.up *= speed;

        if(lifeTime >= 0)
            Destroy(gameObject, lifeTime);
    }

    
    void OnCollisionEnter2D(Collision2D hitInfo)
    {
        //Debug.Log(hitInfo.gameObject.name);

        if(hitInfo.gameObject.CompareTag("Enemy") || hitInfo.gameObject.CompareTag("FlyingEnemy"))
        {
            Debug.Log(hitInfo.gameObject.name);

            if (hitInfo.gameObject.GetComponent<EnemyHealth>() != null)
                hitInfo.gameObject.GetComponent<EnemyHealth>().TakeDamage(damage);
        }

        Destroy(gameObject);
    }
}
