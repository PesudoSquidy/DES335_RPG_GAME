using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bomb : MonoBehaviour
{

    [SerializeField]
    private int damage;

    [SerializeField]
    private float timeBeforeExplosion;

    [SerializeField]
    private Animator anim;

    private bool canDealDamage;

    void Start()
    {
        canDealDamage = false;

        if (anim == null)
            anim = gameObject.GetComponent<Animator>();

        if (anim != null)
            anim.enabled = false;
    }

    void Update()
    {
        if(timeBeforeExplosion > 0)
            timeBeforeExplosion -= Time.deltaTime;
        else if (timeBeforeExplosion <= 0 && anim != null)
        {
            Debug.Log("Play animation");
            canDealDamage = true;
            anim.enabled = true;
        }
    }

    void OnTriggerStay2D(Collider2D collider2D)
    {
        if (collider2D.gameObject.tag == "Enemy")
        {
            if (canDealDamage)
            {
                canDealDamage = false;
                EnemyHealth enemyHP_Script = collider2D.GetComponent<EnemyHealth>();

                if (enemyHP_Script != null)
                    enemyHP_Script.TakeDamage(damage);
            }
        }
    }

    void Dead()
    {
        //Destroy(gameObject);
        gameObject.SetActive(false);
    }
}
