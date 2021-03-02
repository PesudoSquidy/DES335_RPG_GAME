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

    private BoxCollider2D boxCol2D;

    public bool isDigging;
    public bool isUnderObject;

    public GameObject tunnel;

    // Start is called before the first frame update
    void Start()
    {
        tunnelManager = TunnelManager.instance;

        anim = GetComponent<Animator>();
        stamina = GetComponent<PlayerStamina>();
        boxCol2D = GetComponent<BoxCollider2D>();
        sprRender = GetComponent<SpriteRenderer>();

        isDigging = false;
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Space))
        {
            if (stamina.bStaminaDrain == false && stamina.SpendStamina(diggingStaminaCost))
            {
                sprRender.enabled = false;
                //boxCol2D.enabled = false;
                //boxCol2D.isTrigger = true;

                Physics2D.IgnoreLayerCollision(11, 12, true);

                stamina.bStaminaDrain = true;
                isDigging = true;
            }
            else
            {
                if (isUnderObject && tunnel != null)
                    gameObject.transform.position = tunnel.transform.position;

                sprRender.enabled = true;
                //boxCol2D.enabled = true;
                //boxCol2D.isTrigger = false;

                stamina.bStaminaDrain = false;
                isDigging = false;

                Physics2D.IgnoreLayerCollision(11, 12, false);
            }
        }
        else if(stamina.bStaminaDrain == false)
        {
            sprRender.enabled = true;
            boxCol2D.enabled = true;

            isDigging = false;
        }


        if (Input.GetKeyDown(KeyCode.K))
        {
            if (tunnel != null && tunnel.gameObject.GetComponent<Tunnel>().otherEnd.GetComponent<Tunnel>().bActive)
            {
                tunnel.gameObject.GetComponent<Tunnel>().PrepareTransport(gameObject);
                //tunnel.gameObject.GetComponent<Tunnel>().Transport(gameObject);
                tunnel.gameObject.GetComponent<Tunnel>().Transport_2(gameObject);
            }
        }
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.CompareTag("Tunnel"))
        {
            //Debug.Log("On Tunnel");
            tunnel = col.gameObject;
        }
        else if(col.CompareTag("Obstacle") && isDigging)
        {
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
            isUnderObject = false;
        }
    }
}
