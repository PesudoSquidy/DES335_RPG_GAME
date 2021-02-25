using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStats : CharacterStats
{
    // Start is called before the first frame update
    void Start()
    {
        EquipmentManager.instance.onEquipmentChanged += OnEquipmentChanged;
    }

    // Update is called once per frame
    void OnEquipmentChanged(Equipment newItem, Equipment oldItem)
    {
        if (newItem != null)
        {
            armor.AddModifier(newItem.armourModifier);
            damage.AddModifier(newItem.armourModifier);
        }

        if(oldItem != null)
        {
            armor.RemoveModifier(newItem.armourModifier);
            damage.RemoveModifier(newItem.armourModifier);
        }
    }
}
