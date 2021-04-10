using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Flamethrower : MonoBehaviour
{

    private Animator anim;

    private PlayerAttack playerAttack;

    private Augment equipmentAugment;


    void Start()
    {
        if (anim == null)
            anim = gameObject.GetComponent<Animator>();

        if (playerAttack == null)
            playerAttack = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerAttack>();

        if (playerAttack.eq_Name == "Flamethrower")
            equipmentAugment = EquipmentManager.instance.MainEquipment().augment;
        else if (playerAttack.eq_Name_2 == "Flamethrower")
            equipmentAugment = EquipmentManager.instance.SideEquipment().augment;
    }

    void Update()
    {
        if (playerAttack.eq_Name == "Flamethrower")
            equipmentAugment = EquipmentManager.instance.MainEquipment().augment;
        else if(playerAttack.eq_Name_2 == "Flamethrower")
            equipmentAugment = EquipmentManager.instance.SideEquipment().augment;

        if (equipmentAugment != null)
        {
            if (equipmentAugment.augmentStatus != Augment.AugmentStatus.Freeze)
                anim.SetBool("isCyro", false);
            else if (equipmentAugment.augmentStatus == Augment.AugmentStatus.Freeze)
                anim.SetBool("isCyro", true);
        }
    }

    void OnTriggerStay2D(Collider2D col2D)
    {
        if (col2D.gameObject.CompareTag("Enemy") || col2D.gameObject.CompareTag("FlyingEnemy"))
        {
            equipmentAugment = EquipmentManager.instance.MainEquipment().augment;
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
