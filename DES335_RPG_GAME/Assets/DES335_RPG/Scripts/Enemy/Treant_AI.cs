using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Treant_AI : Enemy_AI
{

    private PlayerSkill playerSkill;

    public override void Start()
    {
        base.Start();
        playerSkill = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerSkill>();
    }


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

    public override void FixedUpdate()
    {
        if (playerSkill.isDigging == false)
        {
            //Debug.Log("Chase Player");
            base.FixedUpdate();
        }
        else
        {
            Animation(Vector2.zero);
        }
    }
}
