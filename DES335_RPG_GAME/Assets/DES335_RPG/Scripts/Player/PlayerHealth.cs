using System.Collections;
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

    public Coroutine StatusCheckCoroutine;

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

    public void AfflictStatusCondition(Status newStatus, float duration)
    {
        if(StatusCheckCoroutine == null)
            StatusCheckCoroutine = StartCoroutine(ChangeStatusCondition(newStatus, duration));
        else
        {
            StopCoroutine(StatusCheckCoroutine);
            StatusCheckCoroutine = StartCoroutine(ChangeStatusCondition(newStatus, duration));
        }

    }

    private IEnumerator ChangeStatusCondition(Status newStatus, float duration)
    {
        //Debug.Log("Change player's status");
        statusCondition = newStatus;
        yield return new WaitForSeconds(duration);
        statusCondition = Status.None;
    }
}
