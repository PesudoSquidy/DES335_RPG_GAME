using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using RPGCharacterAnimsFREE;

public class PlayerHealth : MonoBehaviour
{

    public int health;
    public GameObject[] healthUI;
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
            {
                --health;
                healthUI[health].SetActive(false);

                if (health <= 0)
                {
                    if (!death)
                    {
                        death = true;
                        Death();
                    }
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
