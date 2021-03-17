using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rhino_AI : Enemy_AI
{

    //public float accelerationlimit = 0;
    //
    //[Range(0.0f, 1.0f)]
    //public float accelerationSmooth = 0;

    public CameraShake cameraShake;
    public float linearDrag = 0f;

    public override void FixedUpdate()
    {

        if (path == null)
            return;

        if (curretWaypoint >= path.vectorPath.Count)
        {
            //reachedEndOfPath = true;
            return;
        }
        //else
        //{
        //    reachedEndOfPath = false;
        //}

        if (canMove && enemyHealth.currStatusCondition != EnemyHealth.StatusCondition.Freeze)
        {
            // Decrease drag
            rb.drag = 0.0f;

            Vector2 direction = ((Vector2)path.vectorPath[curretWaypoint] - rb.position).normalized;
            Vector2 force = direction * speed * Time.deltaTime;

            //rb.AddForce(force, ForceMode2D.Force);
            rb.AddForce(force);

            float distance = Vector2.Distance(rb.position, path.vectorPath[curretWaypoint]);

            if (distance < nextWaypointDistance)
            {
                ++curretWaypoint;
            }

            Animation(force);
        }
        else if(enemyHealth.currStatusCondition == EnemyHealth.StatusCondition.Freeze)
        {
            // Increase drag
            rb.drag = linearDrag;
        }
        else if(canMove == false)
        {
            rb.velocity = Vector3.zero;
        }
    }


    public override void Animation(Vector2 force)
    {
        // Set animation 
        anim.SetBool("isMoving", canMove);

        // Moving toward to the right
        //if (force.x >= 0.01f)
        if(rb.velocity.x >= 0.01f)
        {
            transform.localScale = new Vector3(-Mathf.Abs(transform.localScale.x), transform.localScale.y, transform.localScale.z);
        }

        // Moving towards left
        //else if (force.x <= -0.01f)
        else if (rb.velocity.x <= -0.01f)
        {
            transform.localScale = new Vector3(Mathf.Abs(transform.localScale.x), transform.localScale.y, transform.localScale.z);
        }
    }


    void OnCollisionEnter2D(Collision2D col)
    {
        // Set animation
        if (col.gameObject.tag == "Wall")
        {
            if(cameraShake == null)
                cameraShake = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<CameraShake>();

            StartCoroutine(cameraShake.Shake(0.15f, 0.4f));
            anim.SetTrigger("isCollision");
        }
        else if(col.gameObject.tag == "Player")
        {
            // Transfer force
            // Debug.Log("Force XFer: " + rb.velocity * rb.mass);
            //Debug.Log("Rhino collides with player");

            StartCoroutine(col.gameObject.GetComponent<PlayerHealth>().ChangeStatusCondition(PlayerHealth.Status.Stun, 2.0f));
            col.gameObject.GetComponent<PlayerMovement>().SpecialPhysics(rb.velocity * rb.mass);
            
            rb.velocity = Vector3.zero;
        }
    }
}
