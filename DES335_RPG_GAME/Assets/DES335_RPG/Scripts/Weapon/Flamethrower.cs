using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Flamethrower : MonoBehaviour
{

    private Animator anim;

    private Augment equipmentAugment;

    void Start()
    {
        if (anim == null)
            anim = gameObject.GetComponent<Animator>();

        equipmentAugment = EquipmentManager.instance.MainEquipment().augment;
    }

    void OnTriggerStay2D(Collider2D col2D)
    {
        if (col2D.gameObject.CompareTag("Enemy") || col2D.gameObject.CompareTag("FlyingEnemy"))
        {
            EnemyHealth enemyHP_Script = col2D.GetComponent<EnemyHealth>();

            if (equipmentAugment != null)
            {
                if (equipmentAugment.augmentStatus == Augment.AugmentStatus.Burn)
                {
                    if (enemyHP_Script != null && enemyHP_Script.currStatusCondition != EnemyHealth.StatusCondition.Burn)
                        enemyHP_Script.TakeDamage(0, EnemyHealth.StatusCondition.Burn);
                }
                else if(equipmentAugment.augmentStatus == Augment.AugmentStatus.Freeze)
                {
                    if (enemyHP_Script != null && enemyHP_Script.currStatusCondition != EnemyHealth.StatusCondition.Freeze)
                        enemyHP_Script.TakeDamage(0, EnemyHealth.StatusCondition.Freeze);
                }
            }
        }
    }

    void Dead()
    {
        //Destroy(transform.parent.gameObject);
        gameObject.SetActive(false);
    }

    void Alive()
    {
        gameObject.SetActive(true);
    }

    public void Animate()
    {

    }
}
