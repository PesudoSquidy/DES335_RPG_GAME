using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;


public abstract class Enemy_AI : MonoBehaviour
{

    public Transform target;
    public float speed = 200f;
    public float nextWaypointDistance = 3;

    protected Path path;
    protected int curretWaypoint = 0;
    //bool reachedEndOfPath = false;

    protected Seeker seeker;
    protected Rigidbody2D rb;

    protected Animator anim;
    public bool canMove;

    protected EnemyHealth enemyHealth;

    // Start is called before the first frame update
    void Start()
    {
        if (anim == null)
            anim = GetComponent<Animator>();

        if (seeker == null)
            seeker = GetComponent<Seeker>();
        
        if(rb == null)
            rb = GetComponent<Rigidbody2D>();

        InvokeRepeating("UpdatePath", 0f, 0.5f);

        if(target == null)
            target = GameObject.FindGameObjectWithTag("Player").transform;

        canMove = false;

        if (enemyHealth == null)
            enemyHealth = GetComponent<EnemyHealth>();
    }

    void UpdatePath()
    {
        if(seeker.IsDone())
            seeker.StartPath(rb.position, target.position, OnPathComplete);
    }

    void OnPathComplete(Path p)
    {
        if(!p.error)
        {
            path = p;
            curretWaypoint = 0;
        }
    }

    // Update is called once per frame
    public virtual void FixedUpdate()
    {
        if (path == null)
            return;
        
        if(curretWaypoint >= path.vectorPath.Count)
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
            Vector2 direction = ((Vector2)path.vectorPath[curretWaypoint] - rb.position).normalized;
            Vector2 force = direction * speed * Time.deltaTime;

            rb.AddForce(force);

            float distance = Vector2.Distance(rb.position, path.vectorPath[curretWaypoint]);

            if (distance < nextWaypointDistance)
            {
                ++curretWaypoint;
            }

            Animation(force);
        }
    }
    public virtual void Animation(Vector2 force)
    {

    }

    public virtual void StartMoving()
    {
        canMove = true;
    }
}
