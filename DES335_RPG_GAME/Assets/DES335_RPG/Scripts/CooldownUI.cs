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

        imageCooldown.fillAmount = 0;
    }

    // Update is called once per frame
    void Update()
    {
        //item = inventorySlot.item;

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

            if (playerAttack.equipmentCooldown > 0)
            {
                if (equipment.name == "Bomb")
                {
                    if (playerAttack.equipmentCooldown == equipment.coolDown)
                        imageCooldown.fillAmount = 1;
                    else
                    {
                        imageCooldown.fillAmount = playerAttack.equipmentCooldown / cooldown;
                        //imageCooldown.fillAmount -= 1 / cooldown * Time.deltaTime;
                    }
                }
                else if (equipment.name == "Boomerang")
                {
                    if (playerAttack.equipmentCooldown == equipment.coolDown)
                        imageCooldown.fillAmount = 1;
                    else
                    {
                        imageCooldown.fillAmount = playerAttack.equipmentCooldown / cooldown;
                    }
                }
            }
            else
                imageCooldown.fillAmount = 0;
        }
    }

    public void Cooldown(float newCooldown)
    {
        cooldown = newCooldown;
    }
}
