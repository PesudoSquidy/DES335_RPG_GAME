using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sandworm_AI : MonoBehaviour
{
    private GameObject playerGO;

    private SpriteRenderer spriteRen;
    private Animator anim;
    private Rigidbody2D rb;

    [SerializeField] private float rangedAttackRadius;
    [SerializeField] private GameObject rangedAttackObject;
    [SerializeField] private Transform rangedAttackSpawnPos;
    [SerializeField] private float rangedAttackCooldown;
    private float rangedAttackTimer;
    [SerializeField] private float rangedAttackForce;
    [SerializeField] private int rangedAttackDamage;

    [SerializeField] private float attackRadius;
    
    [SerializeField] private float movementForce;
    [SerializeField] private float distThreshold;
    [SerializeField] private float moveTime;

    private float currMoveTime;

    [SerializeField] private GameObject spawnTunnelPos;
    [SerializeField] private GameObject tunnelPassage;
    [SerializeField] private float tunnelPassageAliveTime;
    private GameObject prevTunnelPassage;

    enum Sandworm_State { IDLE, SENSING, DIGGING, RANGE_ATTACK}
    Sandworm_State currState;

    // Start is called before the first frame update
    void Start()
    {
        playerGO = GameObject.FindGameObjectWithTag("Player");

        anim = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        spriteRen = GetComponent<SpriteRenderer>();

        // Initialise values
        currMoveTime = 0.0f;
        rangedAttackTimer = 0.0f;

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
                    // Reset all attack relevant data
                    rangedAttackTimer = 0.0f;

                    FaceDirection(playerGO.transform);
                    break;
                }
            case Sandworm_State.DIGGING:
                {
                    if (anim.GetBool("Desurface") == false)
                        Desurfacing();
                    else if (anim.enabled == false && spriteRen.enabled == false)
                    {
                        if (Move(playerGO.transform) == false)
                        {
                            // Re-render the sandworm
                            showSandworm();

                            // Play the resurfacing animation
                            Resurfacing();

                            // Change the state
                            currState = Sandworm_State.IDLE;

                            // Change animation
                            anim.SetBool("Desurface", false);
                        }
                    }
                    break;
                }
            case Sandworm_State.RANGE_ATTACK:
                {
                    FaceDirection(playerGO.transform);

                    if (rangedAttackTimer <= 0.0f)
                    {
                        if(RangeAttack())
                            Debug.Log("Shoot at player");
                    }
                    else
                        rangedAttackTimer -= Time.deltaTime;
                    break;
                }
        }

        // Testing purposes
        if (Input.GetKeyDown(KeyCode.P))
        {
            Resurfacing();
        }
        else if(Input.GetKeyDown(KeyCode.O))
        {
            Desurfacing();
        }
        else if (Input.GetKeyDown(KeyCode.L))
        {
            currState = Sandworm_State.RANGE_ATTACK;
        }
        else if(Input.GetKeyDown(KeyCode.I))
        {
            currState = Sandworm_State.DIGGING;
        }
        else if (Input.GetKeyDown(KeyCode.M))
        {
            Move(playerGO.transform);
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
        anim.SetBool("Desurface", true);
    }

    bool RangeAttack()
    {
        Collider2D[] colInfo = Physics2D.OverlapCircleAll(transform.position, rangedAttackRadius);

        for (int i = 0; i < colInfo.Length; ++i)
        {
            // One of the collided object is player
            if (colInfo[i].gameObject.CompareTag("Player"))
            {
                rangedAttackTimer = rangedAttackCooldown;

                GameObject rangedAtk = Instantiate(rangedAttackObject, rangedAttackSpawnPos.position, Quaternion.identity);
                Rigidbody2D rbAtk = rangedAtk.GetComponent<Rigidbody2D>();
                EnergyBall ebAtk = rangedAtk.GetComponent<EnergyBall>();

                Vector2 dir = playerGO.transform.position - rangedAtk.transform.position;
                rbAtk.AddForce(dir * rangedAttackForce, ForceMode2D.Impulse);
                ebAtk.targetGoal = playerGO.transform.position;
                ebAtk.damage = rangedAttackDamage;
                //colInfo[i].gameObject.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
                //Debug.Log("Sandworm vel: " + GetComponent<Rigidbody2D>().velocity);
                //Debug.Log("Player vel: " + colInfo[i].gameObject.GetComponent<Rigidbody2D>().velocity);

                return true;
            }
        }

        return false;
    }

    public void SurfaceHit()
    {
        Collider2D[] colInfo = Physics2D.OverlapCircleAll(transform.position, attackRadius);

        for(int i = 0; i < colInfo.Length; ++i)
        {
            // One of the collided object is player
            if(colInfo[i].gameObject.CompareTag("Player"))
            {
                colInfo[i].gameObject.GetComponent<PlayerHealth>().AfflictStatusCondition(PlayerHealth.Status.Stun, 2.0f);
                colInfo[i].gameObject.GetComponent<Rigidbody2D>().velocity = Vector2.zero;

                //Debug.Log("Sandworm vel: " + GetComponent<Rigidbody2D>().velocity);
                //Debug.Log("Player vel: " + colInfo[i].gameObject.GetComponent<Rigidbody2D>().velocity);
            }
        }
    }

    void hideSandworm()
    {
        spriteRen.enabled = false;
        anim.enabled = false;
    }

    void showSandworm()
    {
        spriteRen.enabled = true;
        anim.enabled = true;
    }

    // Move towards target
    bool Move(Transform target)
    {
        //yield return new WaitForSeconds(timer);

        // Physics Handling
        Physics2D.IgnoreLayerCollision(15, 10, true);
        Physics2D.IgnoreLayerCollision(15, 12, true);
        Physics2D.IgnoreLayerCollision(15, 14, true);
        
        // Direction to move
        Vector2 dir = target.position - transform.position;

        // Normalize the vector
        dir = dir.normalized;

        // Distance 
        float distance = Vector3.Distance(target.position, transform.position);

        //Debug.Log("Distance: " + distance);

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

            if (prevTunnelPassage != null && Vector2.Distance(spawnTunnelPos.transform.position, prevTunnelPassage.transform.position) > 1)
            {
                //Debug.Log("Length Calculation: " + Vector2.Distance(spawnTunnelPos.transform.position, prevTunnelPassage.transform.position));
                prevTunnelPassage = Instantiate(tunnelPassage, spawnTunnelPos.transform.position, Quaternion.identity);
                prevTunnelPassage.GetComponent<TunnelPassage>().fActiveTime = tunnelPassageAliveTime;
                prevTunnelPassage.GetComponent<TunnelPassage>().bActive = true;
            }
            else if (prevTunnelPassage == null)
            {
                //Debug.Log("prevTunnelPassage: " + prevTunnelPassage);
                prevTunnelPassage = Instantiate(tunnelPassage, spawnTunnelPos.transform.position, Quaternion.identity);
                prevTunnelPassage.GetComponent<TunnelPassage>().fActiveTime = tunnelPassageAliveTime;
                prevTunnelPassage.GetComponent<TunnelPassage>().bActive = true;
            }

            //Debug.Log("Distance: " + distance);
            //Debug.Log("currMoveTime: " + currMoveTime);
            return true;
        }
        else
        {
            // Physics Handling
            Physics2D.IgnoreLayerCollision(15, 10, false);
            Physics2D.IgnoreLayerCollision(15, 12, false);
            Physics2D.IgnoreLayerCollision(15, 14, false);

            // Reset the rb force
            rb.velocity = Vector3.zero;

            // Reset the moveTime
            currMoveTime = 0.0f;
            return false;
        }
    }
}
