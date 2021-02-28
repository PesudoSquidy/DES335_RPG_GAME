using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSkill : MonoBehaviour
{

    private Animator anim;

    private TunnelManager tunnelManager;

    private PlayerStamina stamina;

    [SerializeField]
    private int diggingStaminaCost;

    private BoxCollider2D boxCol2D;

    public bool isDigging;

    // Start is called before the first frame update
    void Start()
    {
        tunnelManager = TunnelManager.instance;

        anim = GetComponent<Animator>();
        stamina = GetComponent<PlayerStamina>();
        boxCol2D = GetComponent<BoxCollider2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Space))
        {
            if (stamina.bStaminaDrain == false && stamina.SpendStamina(diggingStaminaCost))
            {
                boxCol2D.enabled = false;
                stamina.bStaminaDrain = true;
                isDigging = true;
            }
            else
            {
                boxCol2D.enabled = true;
                stamina.bStaminaDrain = false;
                isDigging = false;
            }
        }
    }
}
