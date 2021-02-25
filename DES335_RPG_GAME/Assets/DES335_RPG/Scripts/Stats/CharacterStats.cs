using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterStats : MonoBehaviour
{
    public int maxHealth = 100;
    public int currHealth { get; private set; }

    public Stats damage;
    public Stats armor;

    void Awake()
    {
        currHealth = maxHealth;
    }

    void Update()
    {
        if(Input.GetKeyDown(KeyCode.T))
        {
            TakeDamage(10);
        }
    }

    public void TakeDamage(int damage)
    {
        damage -= armor.GetValue();
        damage = Mathf.Clamp(damage, 0, int.MaxValue);

        currHealth -= damage;

        Debug.Log(transform.name + "takes " + damage + "damage.");

        if(currHealth <= 0)
        {
            Die();
        }
    }

    public virtual void Die()
    {
        //Die in some way
        //This method is meant to be overwritten

        Debug.Log(transform.name + "died.");
    }
}
