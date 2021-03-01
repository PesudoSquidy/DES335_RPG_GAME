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
    public bool onTunnel;

    private GameObject tunnel;
    // Start is called before the first frame update
    void Start()
    {
        tunnelManager = TunnelManager.instance;

        anim = GetComponent<Animator>();
        stamina = GetComponent<PlayerStamina>();
        boxCol2D = GetComponent<BoxCollider2D>();
        sprRender = GetComponent<SpriteRenderer>();

        onTunnel = false;
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
                boxCol2D.enabled = false;

                stamina.bStaminaDrain = true;
                isDigging = true;
            }
            else
            {
                sprRender.enabled = true;
                boxCol2D.enabled = true;

                stamina.bStaminaDrain = false;
                isDigging = false;
            }
        }
        else if(stamina.bStaminaDrain == false)
        {
            boxCol2D.enabled = true;

            isDigging = false;
        }


        if (Input.GetKeyDown(KeyCode.K))
        {
            if (tunnel != null)
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
    }

    void OnTriggerExit2D(Collider2D col)
    {
        if (col.CompareTag("Tunnel"))
        {
            //Debug.Log("Off Tunnel");
            tunnel = null;
        }
    }
}
