using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Eagle_AI : Enemy_AI
{

    public override void Animation(Vector2 force)
    {
        // Moving toward to the right
        if (force.x >= 0.01f)
        {
            transform.localScale = new Vector3(-Mathf.Abs(transform.localScale.x), transform.localScale.y, transform.localScale.z);
        }

        // Moving towards left
        else if (force.x <= -0.01f)
        {
            transform.localScale = new Vector3(Mathf.Abs(transform.localScale.x), transform.localScale.y, transform.localScale.z);
        }
    }

}
