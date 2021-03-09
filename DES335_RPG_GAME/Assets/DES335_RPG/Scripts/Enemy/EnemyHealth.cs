using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class EnemyHealth : MonoBehaviour
{
    [SerializeField] private float health;
    [SerializeField] protected Animator anim;
    [SerializeField] private LootDrop lootDrop;

    private bool dieOnce;

    public enum StatusCondition { None, Burn, Freeze };

    public StatusCondition currStatusCondition;

    [SerializeField] private float burnDuration;
    [SerializeField] private float burnTick;
    [SerializeField] private float burnDamage;

    private float statusConditionTimer;

    [SerializeField] private float freezeDuration;

    // Health Status Condition Part - To be seperated
    [SerializeField] private GameObject burnStatusAnim;
    [SerializeField] private GameObject freezeStatusAnim;

    void Awake()
    {
        if(anim == null)
            anim = gameObject.GetComponent<Animator>();

        if (lootDrop == null)
            lootDrop = gameObject.GetComponent<LootDrop>();

        dieOnce = false;
        currStatusCondition = StatusCondition.None;
        statusConditionTimer = 0;

        if (burnStatusAnim != null)
            burnStatusAnim.SetActive(false);

        if(freezeStatusAnim != null)
            freezeStatusAnim.SetActive(false);
    }

    void Update()
    {
        if(currStatusCondition != StatusCondition.None && statusConditionTimer > 0)
        {
            statusConditionTimer -= Time.deltaTime;

            if (statusConditionTimer <= 0)
            {
                currStatusCondition = StatusCondition.None;
                statusConditionTimer = 0;
                CancelInvoke("TakeDamage");

                if(burnStatusAnim != null)
                    burnStatusAnim.SetActive(false);

                if (freezeStatusAnim != null)
                    freezeStatusAnim.SetActive(false);
            }
        }
    }

    public void TakeDamage(float damage = 0, StatusCondition newStatus = StatusCondition.None)
    {
        if(newStatus == StatusCondition.None)
            health -= damage;
        else if(newStatus == StatusCondition.Burn && currStatusCondition != StatusCondition.Burn)
        {
            currStatusCondition = StatusCondition.Burn;
            statusConditionTimer = burnDuration;

            if (burnStatusAnim != null)
                burnStatusAnim.SetActive(true);

            InvokeRepeating("StatusDamage", 0.0f, burnTick);
        }
        else if(newStatus == StatusCondition.Freeze && currStatusCondition != StatusCondition.Freeze)
        {
            currStatusCondition = StatusCondition.Freeze;
            statusConditionTimer = freezeDuration;

            if (freezeStatusAnim != null)
                freezeStatusAnim.SetActive(true);
        }

        if (health <= 0 && dieOnce == false)
        {
            dieOnce = true;
            Die();
        }
    }

    void StatusDamage()
    {
        if (currStatusCondition == StatusCondition.None || currStatusCondition == StatusCondition.Freeze)
            return;
        if (currStatusCondition == StatusCondition.Burn)
            health -= burnDamage;

        if (health <= 0 && dieOnce == false)
        {
            dieOnce = true;
            Die();
        }
    }

    public virtual void Die()
    {
        Dead();
    }

    public void Dead()
    {
        if(lootDrop !=null)
            lootDrop.DropLoot();

        Destroy(gameObject);
    }
}
