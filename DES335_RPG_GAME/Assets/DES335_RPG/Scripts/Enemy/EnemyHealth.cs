using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class EnemyHealth : MonoBehaviour
{
    [SerializeField]
    private int health;

    [SerializeField]
    protected Animator anim;

    [SerializeField]
    private LootDrop lootDrop;

    private bool dieOnce;

    void Start()
    {
        if(anim == null)
            anim = gameObject.GetComponent<Animator>();

        if (lootDrop == null)
            lootDrop = gameObject.GetComponent<LootDrop>();

        dieOnce = false;
    }

    public void TakeDamage(int damage)
    {
        health -= damage;

        if (health <= 0 && dieOnce == false)
        {
            dieOnce = true;
            Die();
        }
    }

    public virtual void Die()
    {
        Dead();
    }

    public void Dead()
    {
        if(lootDrop !=null)
            lootDrop.DropLoot();

        Destroy(gameObject);
    }
}
