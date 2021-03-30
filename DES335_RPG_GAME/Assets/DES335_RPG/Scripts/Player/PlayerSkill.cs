using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSkill : MonoBehaviour
{

    private Animator anim;
    private SpriteRenderer sprRender;

    private TunnelManager tunnelManager;

    private PlayerStamina stamina;

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
        stamina = GetComponent<PlayerStamina>();
        //boxCol2D = GetComponent<BoxCollider2D>();
        sprRender = GetComponent<SpriteRenderer>();

        isDigging = false;
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetButtonDown("Digging"))
        {
            if (tunnel != null && tunnel.gameObject.GetComponent<Tunnel>().otherEnd && tunnel.gameObject.GetComponent<Tunnel>().otherEnd.GetComponent<Tunnel>().bActive)
            {
                Debug.Log("Player wants to teleport");
                //tunnel.gameObject.GetComponent<Tunnel>().PrepareTransport(gameObject);
                tunnel.gameObject.GetComponent<Tunnel>().Transport_2(gameObject);
            }
            else if (stamina.bStaminaDrain == false && stamina.SpendStamina(diggingStaminaCost))
            {
                sprRender.enabled = false;
                
                stamina.bStaminaDrain = true;
                isDigging = true;

                Physics2D.IgnoreLayerCollision(12, 11, true);
                Physics2D.IgnoreLayerCollision(12, 13, true);
            }
            else
            {
                if (isUnderObject && tunnel != null)
                    gameObject.transform.position = tunnel.transform.position;

                sprRender.enabled = true;

                stamina.bStaminaDrain = false;
                isDigging = false;

                Physics2D.IgnoreLayerCollision(12, 11, false);
                Physics2D.IgnoreLayerCollision(12, 13, false);
            }
        }
        else if(stamina.bStaminaDrain == false)
        {
            if (isUnderObject && tunnel != null)
            {
                gameObject.transform.position = tunnel.transform.position;
                tunnel.gameObject.GetComponent<Tunnel>().otherEnd = tunnel.gameObject;
                isUnderObject = false;
            }

            sprRender.enabled = true;

            Physics2D.IgnoreLayerCollision(12, 11, false);
            Physics2D.IgnoreLayerCollision(12, 13, false);
            isDigging = false;
        }
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.CompareTag("Tunnel") && !tunnel)
        {
            //Debug.Log("On Tunnel");
            tunnel = col.gameObject;
        }
        else if(col.CompareTag("Obstacle") && isDigging)
        {
            //Debug.Log("On Obstacle");
            isUnderObject = true;
        }
    }

    void OnTriggerExit2D(Collider2D col)
    {
        if (col.CompareTag("Tunnel") && isDigging == false)
        {
            //Debug.Log("Off Tunnel");
            tunnel = null;
        }
        else if (col.CompareTag("Obstacle") && isDigging)
        {
            //Debug.Log("Off Obstacle");
            isUnderObject = false;
        }
    }
}
