using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Arrow : MonoBehaviour
{
    [SerializeField] private float speed;

    private Rigidbody2D rb;

    [SerializeField] private float lifeTime;

    [SerializeField] private int damage;

    [SerializeField] private int hitAmount;

    private PlayerAttack playerAttack;
    private Augment equipmentAugment;

    // Start is called before the first frame update
    void Start()
    {
        if (rb == null)
            rb = gameObject.GetComponent<Rigidbody2D>();

        rb.velocity = transform.up *= speed;

        if(lifeTime >= 0)
            Destroy(gameObject, lifeTime);

        if (playerAttack == null)
            playerAttack = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerAttack>();

        if (playerAttack.eq_Name == "Bow")
            equipmentAugment = EquipmentManager.instance.MainEquipment().augment;
        else if (playerAttack.eq_Name_2 == "Bow")
            equipmentAugment = EquipmentManager.instance.SideEquipment().augment;
    }

    
    void OnTriggerEnter2D(Collider2D col)
    {
        if (playerAttack.eq_Name == "Bow")
            equipmentAugment = EquipmentManager.instance.MainEquipment().augment;
        else if (playerAttack.eq_Name_2 == "Bow")
            equipmentAugment = EquipmentManager.instance.SideEquipment().augment;

        if (col.CompareTag("Enemy") || col.CompareTag("FlyingEnemy") || col.gameObject.CompareTag("Boss"))
        {
            EnemyHealth enemyHP_Script = col.GetComponent<EnemyHealth>();

            enemyHP_Script.TakeDamage(damage);

            if (equipmentAugment != null)
            {
                //Debug.Log(col.name);

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

            --hitAmount;
        }

        if(hitAmount <= 0)
            Destroy(gameObject);
    }
}
