using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// GUI in unity to create new items
[CreateAssetMenu(fileName = "New Equipment", menuName = "Inventory/Equipment")]


public class Equipment : Item
{
    public enum EquipmentSlot { Main_Weapon, Side_Weapon }

    public EquipmentSlot equipSlot;

    [HideInInspector] public int damageModifier;
    [HideInInspector] public int armourModifier;

    public Augment augment = null;

    public override void Use()
    {
        base.Use();

        EquipmentManager.instance.Equip(this);
        RemoveFromInventory();
        //Equip the item
        //Remove from inventory
    }
}

//public enum EquipmentSlot { Head, Chest, Legs, Weapon, Shield, Feet}
//public enum EquipmentSlot { Head, Chest, Legs, Weapon, Shield}
