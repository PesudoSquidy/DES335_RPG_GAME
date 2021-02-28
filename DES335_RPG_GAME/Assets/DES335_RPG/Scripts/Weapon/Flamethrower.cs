using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Flamethrower : MonoBehaviour
{
    void OnTriggerEnter2D(Collider2D col2D)
    {
        if (col2D.gameObject.CompareTag("Enemy") || col2D.gameObject.CompareTag("FlyingEnemy"))
        {
            EnemyHealth enemyHP_Script = col2D.GetComponent<EnemyHealth>();

            if (enemyHP_Script != null && enemyHP_Script.currStatusCondition != EnemyHealth.StatusCondition.Burn)
                enemyHP_Script.TakeDamage(0, EnemyHealth.StatusCondition.Burn);
        }
    }

    void Dead()
    {
        Destroy(gameObject);
    }
}
