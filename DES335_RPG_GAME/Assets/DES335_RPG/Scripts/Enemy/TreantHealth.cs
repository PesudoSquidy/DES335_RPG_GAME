using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TreantHealth : EnemyHealth
{
    public override void Die()
    {
        Debug.Log("Treant dead");
        anim.SetTrigger("isDead");
    }
}
