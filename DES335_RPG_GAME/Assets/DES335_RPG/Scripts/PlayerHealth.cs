using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


using RPGCharacterAnimsFREE;

public class PlayerHealth : MonoBehaviour
{

    [SerializeField] private int health;
    private RPGCharacterController RPGCC;
    private bool death = false;

    // Handles the player Health UI
    [SerializeField] private GameObject[] healthUI;

    // The sprite for the Health UI
    //[SerializeField] private Sprite Full_Heart_Sprite;  // 2
    //[SerializeField] private Sprite Half_Heart_Sprite;  // 1
    //[SerializeField] private Sprite Empty_Heart_Sprite; // 0

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

                // Calculate where to update the health UI
                //int hp_UI = health % 2;

                //if(hp_UI > 0)
                //{
                //    hp_UI = health / 2;
                //    healthUI[[hp_UI].GetComponent<RawImage>().texture = Half_Heart_Sprite;
                //}
                //else
                //{
                //    hp_UI = health / 2;
                //    //healthUI[hp_UI].SetActive(false);
                //    healthUI[hp_UI].GetComponent<RawImage>().texture = Empty_Heart_Sprite;
                //}
            }

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

    void Death()
    {
        if (RPGCC.CanStartAction("Death"))
            RPGCC.StartAction("Death");
        else if (RPGCC.CanEndAction("Death"))
            RPGCC.EndAction("Death");
    }
}
