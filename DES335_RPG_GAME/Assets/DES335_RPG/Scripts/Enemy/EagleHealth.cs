using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EagleHealth : EnemyHealth
{
    public override void Die()
    {
        anim.SetTrigger("isDead");
    }
}
