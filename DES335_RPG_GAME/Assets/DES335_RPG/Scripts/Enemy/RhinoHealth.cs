using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RhinoHealth : EnemyHealth
{
    public override void Die()
    {
        anim.SetTrigger("isDead");
    }
}
