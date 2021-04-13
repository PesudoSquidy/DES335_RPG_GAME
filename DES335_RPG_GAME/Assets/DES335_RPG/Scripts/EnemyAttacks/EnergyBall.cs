using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnergyBall : MonoBehaviour
{
    public Vector3 targetGoal;
    public int damage;
    [SerializeField] private float distanceThreshold;

    // Update is called once per frame
    void Update()
    {
        if(Vector2.Distance(targetGoal, transform.position) <= distanceThreshold)
        {
            GetComponent<Rigidbody2D>().velocity = Vector2.zero;
            GetComponent<Animator>().SetTrigger("Explosion");
        }
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        if(col.CompareTag("Player"))
        {
            col.GetComponent<PlayerHealth>().TakeDamage(damage);
        }
    }

    public void Destroy()
    {
        Destroy(gameObject);
    }
}
