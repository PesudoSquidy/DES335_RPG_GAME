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

    // Start is called before the first frame update
    void Start()
    {
        tunnelManager = TunnelManager.instance;

        anim = GetComponent<Animator>();
        stamina = GetComponent<PlayerStamina>();
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Space))
        {
            Debug.Log("Player digging");

            if (stamina.bStaminaDrain == false && stamina.SpendStamina(diggingStaminaCost))
                stamina.bStaminaDrain = true;
            else
                stamina.bStaminaDrain = false;
        }
    }
}
