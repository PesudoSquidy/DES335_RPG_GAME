﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bomb : MonoBehaviour
{

    [SerializeField]
    private int damage;

    [SerializeField]
    private float timeBeforeExplosion;

    [SerializeField]
    private float timeTillDestroyGameobject;

    [SerializeField]
    private Animator anim;

    private Augment equipmentAugment;

    void Start() 
    {
        if (anim == null)
            anim = gameObject.GetComponent<Animator>();

        equipmentAugment = EquipmentManager.instance.MainEquipment().augment;
    }

    void Update()
    {
        if (timeBeforeExplosion > 0)
            timeBeforeExplosion -= Time.deltaTime;
        else if (timeBeforeExplosion <= 0 && anim != null)
            anim.SetTrigger("boom");
    }

    void OnTriggerStay2D(Collider2D col)
    {
        if (col.gameObject.CompareTag("Enemy"))
        {
            if (timeBeforeExplosion <= 0)
            {
                EnemyHealth enemyHP_Script = col.GetComponent<EnemyHealth>();

                enemyHP_Script.TakeDamage(damage);

                if (equipmentAugment != null)
                {
                    if (equipmentAugment.augmentStatus == Augment.AugmentStatus.Burn)
                    {
                        if (enemyHP_Script != null && enemyHP_Script.currStatusCondition != EnemyHealth.StatusCondition.Burn)
                            enemyHP_Script.TakeDamage(0, EnemyHealth.StatusCondition.Burn);
                    }
                    else if (equipmentAugment.augmentStatus == Augment.AugmentStatus.Freeze)
                    {
                        if (enemyHP_Script != null && enemyHP_Script.currStatusCondition != EnemyHealth.StatusCondition.Freeze)
                            enemyHP_Script.TakeDamage(0, EnemyHealth.StatusCondition.Freeze);
                    }
                }
            }
        }
    }

    void Dead()
    {
        Destroy(gameObject);
    }
}
