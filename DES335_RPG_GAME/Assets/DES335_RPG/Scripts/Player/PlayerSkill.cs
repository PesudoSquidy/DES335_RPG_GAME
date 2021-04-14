using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSkill : MonoBehaviour
{
    private Animator anim;
    private SpriteRenderer sprRender;

    private TunnelManager tunnelManager;

    //private PlayerStamina stamina;
    private PlayerStamina_2 playerStamina;

    [SerializeField]
    private int diggingStaminaCost;

    //private BoxCollider2D boxCol2D;

    public bool isDigging;
    public bool isUnderObject;

    public GameObject tunnel;

    // Start is called before the first frame update
    void Start()
    {
        tunnelManager = TunnelManager.instance;

        anim = GetComponent<Animator>();

        //stamina = GetComponent<PlayerStamina>();
        playerStamina = GetComponent<PlayerStamina_2>();

        //boxCol2D = GetComponent<BoxCollider2D>();
        sprRender = GetComponent<SpriteRenderer>();

        isDigging = false;
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetButtonDown("Digging"))
        {
            // If player is on the tunnel
            if (tunnel != null && tunnel.gameObject.GetComponent<Tunnel>().otherEnd && tunnel.gameObject.GetComponent<Tunnel>().otherEnd.GetComponent<Tunnel>().bActive)
            {
                //tunnel.gameObject.GetComponent<Tunnel>().PrepareTransport(gameObject);
                tunnel.gameObject.GetComponent<Tunnel>().Transport_2(gameObject);
            }
            //else if (stamina.bStaminaDrain == false && stamina.SpendStamina(diggingStaminaCost))

            // Player starts diggin
            else if (playerStamina.currPickaxeStamina >= 0 && playerStamina.bStaminaDrain == false)
            {
                Physics2D.IgnoreLayerCollision(12, 10, true);
                Physics2D.IgnoreLayerCollision(12, 11, true);
                Physics2D.IgnoreLayerCollision(12, 13, true);
                Physics2D.IgnoreLayerCollision(12, 14, true);
                Physics2D.IgnoreLayerCollision(12, 15, true);
                Physics2D.IgnoreLayerCollision(12, 16, true);

                sprRender.enabled = false;

                //stamina.bStaminaDrain = true;

                playerStamina.bStaminaDrain = true;
                isDigging = true;
            }
            else
            {
                Physics2D.IgnoreLayerCollision(12, 10, false);
                Physics2D.IgnoreLayerCollision(12, 11, false);
                Physics2D.IgnoreLayerCollision(12, 13, false);
                Physics2D.IgnoreLayerCollision(12, 14, false);
                Physics2D.IgnoreLayerCollision(12, 15, false);
                Physics2D.IgnoreLayerCollision(12, 16, false);

                if (isUnderObject && tunnel != null)
                    gameObject.transform.position = tunnel.transform.position;

                sprRender.enabled = true;

                //stamina.bStaminaDrain = false;
                playerStamina.bStaminaDrain = false;

                isDigging = false;
            }
        }
        //else if(stamina.bStaminaDrain == false)
        else if(playerStamina.bStaminaDrain == false || isDigging == false)
        {
            Physics2D.IgnoreLayerCollision(12, 10, false);
            Physics2D.IgnoreLayerCollision(12, 11, false);
            Physics2D.IgnoreLayerCollision(12, 13, false);
            Physics2D.IgnoreLayerCollision(12, 14, false);
            Physics2D.IgnoreLayerCollision(12, 15, false);
            Physics2D.IgnoreLayerCollision(12, 16, false);

            if (isUnderObject && tunnel != null)
            {
                gameObject.transform.position = tunnel.transform.position;
                tunnel.gameObject.GetComponent<Tunnel>().otherEnd = tunnel.gameObject;
                isUnderObject = false;
            }

            playerStamina.bStaminaDrain = false;
            sprRender.enabled = true;
            isDigging = false;
        }
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        //if (col.CompareTag("Tunnel") && !tunnel)
        //{
        //    Debug.Log("On Tunnel");
        //    tunnel = col.gameObject;
        //}
        if(col.CompareTag("Obstacle") && isDigging)
        {
            //Debug.Log("On Obstacle");
            isUnderObject = true;
        }
    }

    void OnTriggerStay2D(Collider2D col)
    {
        if (col.CompareTag("Tunnel") && isDigging == false)
        {
            // Debug.Log("On Tunnel");
            tunnel = col.gameObject;
        }
    }


    void OnTriggerExit2D(Collider2D col)
    {
        if (col.CompareTag("Tunnel") && isDigging == false)
        {
            // Debug.Log("Off Tunnel");
            tunnel = null;
        }
        else if (col.CompareTag("Obstacle") && isDigging)
        {
            //Debug.Log("Off Obstacle");
            isUnderObject = false;
        }
    }
}
