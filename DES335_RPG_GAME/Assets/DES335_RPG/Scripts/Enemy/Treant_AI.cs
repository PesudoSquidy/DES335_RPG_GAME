using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Treant_AI : Enemy_AI
{
    public override void Animation(Vector2 force)
    {
        //base.Animation(force);

        if(anim != null)
        {
            anim.SetFloat("horizontalSpeed", force.x);
            anim.SetFloat("verticalSpeed", force.y);
            anim.SetFloat("speed", force.sqrMagnitude);
        }
    }
}
