using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    [SerializeField]
    private int health;

    private Animator anim;
    private LootDrop lootDrop;

    void Start()
    {
        if(anim == null)
            anim = gameObject.GetComponent<Animator>();

        if (lootDrop == null)
            lootDrop = gameObject.GetComponent<LootDrop>();
    }

    public void TakeDamage(int damage)
    {
        health -= damage;

        if (health <= 0)
            Die();
    }

    void Die()
    {
        anim.SetTrigger("isDead");
    }

    void Dead()
    {
        lootDrop.DropLoot();
        Destroy(gameObject);
    }
}
