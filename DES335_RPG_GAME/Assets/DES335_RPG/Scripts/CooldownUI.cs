using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CooldownUI : MonoBehaviour
{

    public Image imageCooldown;
    public float cooldown;
    bool isCooldown;

    [SerializeField] PlayerAttack playerAttack;

    //[SerializeField] InventorySlot inventorySlot;

    [SerializeField] EquipmentSlot equipmentSlot;

    //private Item item;

    private Equipment equipment;
    
    void Start()
    {
        if(playerAttack == null)
            playerAttack = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerAttack>();

        if (equipmentSlot != null)
            equipment = equipmentSlot.equipment;

        // There is no cooldown at the start
        imageCooldown.fillAmount = 0;
    }

    // Update is called once per frame
    void Update()
    {
        equipment = equipmentSlot.equipment;

        //if (item != null)
        if (equipment != null)
        {
            //cooldown = item.coolDown;
            cooldown = equipment.coolDown;

            //if(Input.GetKeyDown(KeyCode.P))
            //{
            //    isCooldown = true;
            //}

            // In sync with the cooldown of the player weapon
            if (equipment.name == playerAttack.eq_Name)
            {
                if (playerAttack.eq_CD > 0)
                    imageCooldown.fillAmount = playerAttack.eq_CD / cooldown;
                else
                    imageCooldown.fillAmount = 0;
            }
            else if (equipment.name == playerAttack.eq_Name_2)
            {
                if (playerAttack.eq_CD_2 > 0)
                    imageCooldown.fillAmount = playerAttack.eq_CD_2 / cooldown;
                else
                    imageCooldown.fillAmount = 0;
            }


            //if (playerAttack.equipmentCooldown > 0)
            //{
            //    if (equipment.name == "Bomb")
            //    {
            //        if (playerAttack.equipmentCooldown == equipment.coolDown)
            //            imageCooldown.fillAmount = 1;
            //        else
            //        {
            //            imageCooldown.fillAmount = playerAttack.equipmentCooldown / cooldown;
            //            //imageCooldown.fillAmount -= 1 / cooldown * Time.deltaTime;
            //        }
            //    }
            //    else if (equipment.name == "Boomerang")
            //    {
            //        if (playerAttack.equipmentCooldown == equipment.coolDown)
            //            imageCooldown.fillAmount = 1;
            //        else
            //        {
            //            imageCooldown.fillAmount = playerAttack.equipmentCooldown / cooldown;
            //        }
            //    }
            //}
            //else
              //  imageCooldown.fillAmount = 0;
        }
    }

    public void Cooldown(float newCooldown)
    {
        cooldown = newCooldown;
    }
}
