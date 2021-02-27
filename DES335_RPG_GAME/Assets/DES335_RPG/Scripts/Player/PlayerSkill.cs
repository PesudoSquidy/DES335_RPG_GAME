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
                Debug.Log("Player digging");

                boxCol2D.enabled = false;
                stamina.bStaminaDrain = true;
            }
            else
            {
                boxCol2D.enabled = true;
                stamina.bStaminaDrain = false;
            }
        }
    }
}
