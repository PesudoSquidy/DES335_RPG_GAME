using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStamina_2 : MonoBehaviour
{
    public bool bStaminaDrain;
    
    [SerializeField] private Pickaxe_Stamina_UI[] maxPickaxe;
    [SerializeField] public int maxStamina;
    
    private float currStamina;
    public int currPickaxeStamina;

    [SerializeField] private float staminaRegenRate;
    [SerializeField] private float staminaDrainRate;



    // Start is called before the first frame update
    void Start()
    {
        currPickaxeStamina = 0;
        currStamina = maxStamina;
        bStaminaDrain = false;

        for(int i = 0; i < maxPickaxe.Length; ++i)
        {
            maxPickaxe[i].slider.maxValue = maxStamina;
            maxPickaxe[i].slider.value = maxStamina;
        }

        currPickaxeStamina = maxPickaxe.Length;
    }

    // Update is called once per frame
    void Update()
    {
        if (bStaminaDrain == false)
        {
            //Check if the last pickaxe is full
            if (maxPickaxe[0].slider.value >= 100)
            {
                // Find the first full pickaxe
                for (int i = maxPickaxe.Length - 1; i >= 0; --i)
                {
                    if (maxPickaxe[i].slider.value >= 100)
                    {
                        currPickaxeStamina = i;
                        break;
                    }
                }
            }
            else
            {
                currPickaxeStamina = -1;
            }
        }
        else if(bStaminaDrain)
        {
            //Debug.Log("Deplete stamina: " + currPickaxeStamina);
            maxPickaxe[currPickaxeStamina].slider.value -= staminaDrainRate * Time.deltaTime;

            // No stamina left to drain
            if (maxPickaxe[currPickaxeStamina].slider.value <= 0)
                bStaminaDrain = false;
        }


        // Regen the stamina that requires to be regen
        for (int i = 0; i < maxPickaxe.Length; ++i)
        {
            if (i != currPickaxeStamina)
                maxPickaxe[i].slider.value += staminaRegenRate * Time.deltaTime;
        }
    }
}
