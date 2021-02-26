using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    [SerializeField]
    private int health;

    private Animator anim;

    void Start()
    {
        anim = gameObject.GetComponent<Animator>();
    }

    public void TakeDamage(int damage)
    {
        health -= damage;

        if (health <= 0)
            Die();
    }

    void Die()
    {
        Debug.Log(gameObject.name + "Die");
        anim.SetTrigger("isDead");
    }

    void Dead()
    {
        Debug.Log(gameObject.name + "Dead");
        Destroy(gameObject);
    }
}
