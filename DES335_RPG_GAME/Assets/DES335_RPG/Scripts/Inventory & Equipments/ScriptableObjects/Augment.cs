using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// GUI in unity to create new items
[CreateAssetMenu(fileName = "New Augment", menuName = "Inventory/Augment")]

public class Augment : Item
{
    public enum AugmentStatus { Burn, Freeze}

    public AugmentStatus augmentStatus;


    public override void Use()
    {
        Equipment tempMainEquipment = EquipmentManager.instance.MainEquipment();

        if (tempMainEquipment != null)
        {
            if (tempMainEquipment.augment == null)
            {
                tempMainEquipment.augment = this;

                //Remove from inventory
                RemoveFromInventory();
            }
            else
            {
                Augment tempAugment = tempMainEquipment.augment;
                tempMainEquipment.augment = this;

                // Swap in Inventory
                Inventory.instance.Add(tempAugment);
                RemoveFromInventory();
            }

            // Event call (Callback function)
            EquipmentManagerUI.instance.UpdateEquipmentUI(null, null);
        }
    }
}
