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

    private Augment equipmentAugment;

    // Start is called before the first frame update
    void Start()
    {
        if (rb == null)
            rb = gameObject.GetComponent<Rigidbody2D>();

        rb.velocity = transform.up *= speed;

        if(lifeTime >= 0)
            Destroy(gameObject, lifeTime);

        equipmentAugment = EquipmentManager.instance.MainEquipment().augment;
    }

    
    void OnTriggerEnter2D(Collider2D col)
    {
        if(col.CompareTag("Enemy") || col.CompareTag("FlyingEnemy"))
        {
            EnemyHealth enemyHP_Script = col.GetComponent<EnemyHealth>();

            enemyHP_Script.TakeDamage(damage);

            if (equipmentAugment != null)
            {
                Debug.Log(col.name);

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
