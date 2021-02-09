using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using RPGCharacterAnimsFREE;

public class PlayerHealth : MonoBehaviour
{

    public int health;
    private RPGCharacterController RPGCC;
    private bool death = false;

    void Awake()
    {
        RPGCC = gameObject.GetComponent<RPGCharacterController>();
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Enemy")
        {
            if (health > 0)
                --health;
            else if (health <= 0)
            {
                if(!death)
                {
                    death = true;
                    Death();
                }
            }
        }
    }

    void Death()
    {
        if (RPGCC.CanStartAction("Death"))
            RPGCC.StartAction("Death");
        else if (RPGCC.CanEndAction("Death"))
            RPGCC.EndAction("Death");
    }
}
