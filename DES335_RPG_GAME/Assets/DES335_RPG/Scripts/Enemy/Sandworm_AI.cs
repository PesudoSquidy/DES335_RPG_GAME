using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sandworm_AI : MonoBehaviour
{
    private GameObject playerGO;
    
    private Animator anim;
    private Rigidbody2D rb;

    [SerializeField] private float movementForce;

    [SerializeField] private float distThreshold;
    [SerializeField] private float moveTime;

    private float currMoveTime;

    enum Sandworm_State { IDLE, SENSING, DIGGING, ATTACK}

    Sandworm_State currState;

    // Start is called before the first frame update
    void Start()
    {
        playerGO = GameObject.FindGameObjectWithTag("Player");

        anim = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();

        // Initialise values
        currMoveTime = 0.0f;

        // Guard & Defaulted Values
        if (distThreshold <= 0)
            distThreshold = 1.0f;

        if (moveTime <= 0)
            moveTime = 3.0f;

        // Default state at the start
        currState = Sandworm_State.IDLE;
    }

    // Update is called once per frame
    void Update()
    {
        switch(currState)
        {
            case Sandworm_State.IDLE:
                {
                    FaceDirection(playerGO.transform);
                    break;
                }
            case Sandworm_State.DIGGING:
                {
                    Move(playerGO.transform);
                    break;
                }
        }

        
        

        // Testing purposess
        if (Input.GetKeyDown(KeyCode.P))
        {
            Resurfacing();
        }
        else if(Input.GetKeyDown(KeyCode.O))
        {
            Desurfacing();
        }
        else if(Input.GetKeyDown(KeyCode.I))
        {
            Desurfacing();
            currState = Sandworm_State.DIGGING;
        }
    }

    void FaceDirection(Transform target)
    {
        // Find direction to face
        Vector3 dir = target.position - transform.position;

        // Face Right
        if (dir.x >= 0)
        {
            //Debug.Log("Face Right");
            transform.localScale = new Vector3(8, 8, 1);
        }
        else
        {
            //Debug.Log("Face Left");
            transform.localScale = new Vector3(-8, 8, 1);
        }
    }

    void Resurfacing()
    {
        anim.SetTrigger("Resurface");
    }

    void Desurfacing()
    {
        //anim.SetTrigger("Desurface");
        anim.SetBool("Desurface", true);
    }

    // Move towards target
    bool Move(Transform target)
    {
        //yield return new WaitForSeconds(timer);

        // Physics Handling
        Physics2D.IgnoreLayerCollision(15, 12, true);
        Physics2D.IgnoreLayerCollision(15, 14, true);
        
        // Direction to move
        Vector2 dir = target.position - transform.position;

        // Normalize the vector
        dir = dir.normalized;

        // Distance 
        float distance = Vector3.Distance(target.position, transform.position);

        Debug.Log("Distance: " + distance);

        // Move towards the player
        if (movementForce > 0 && distance > distThreshold && currMoveTime < moveTime)
        {
            dir = target.position - transform.position;
            dir = dir.normalized;

            currMoveTime += Time.fixedDeltaTime;

            //rb.MovePosition(rb.position + dir * movementSpeed * Time.fixedDeltaTime);
            //rb.AddRelativeForce(rb.position + dir * movementForce * Time.fixedDeltaTime);

            rb.AddForce(dir * movementForce * Time.fixedDeltaTime);

            distance = Vector2.Distance(target.position, transform.position);

            //Debug.Log("Distance: " + distance);
            //Debug.Log("currMoveTime: " + currMoveTime);
            return true;
        }
        else
        {
            // Reset the rb force
            rb.velocity = Vector3.zero;

            // Reset the moveTime
            currMoveTime = 0.0f;

            // Change the state
            currState = Sandworm_State.IDLE;

            // Change animation
            anim.SetBool("Desurface", false);

            return false;
        }
    }
}
