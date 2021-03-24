﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour
{

    public int health;
    private bool death = false;

    public enum Status { None, Stun };

    public Status statusCondition = Status.None;

    public GameObject stunAnim;

    void Update()
    {
        if (statusCondition == Status.Stun)
            stunAnim.SetActive(true);
        else
            stunAnim.SetActive(false);
    }
    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Enemy" || collision.gameObject.tag == "FlyingEnemy")
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
        //Debug.Log("Player dies");
    }

    public IEnumerator ChangeStatusCondition(Status newStatus, float duration)
    {
        //Debug.Log("Change player's status");
        statusCondition = newStatus;
        yield return new WaitForSeconds(duration);
        statusCondition = Status.None;
    }
}
