using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EquipmentManager : MonoBehaviour
{

    #region Singleton

    public static EquipmentManager instance;

    void Awake()
    {
        instance = this;


    }

    #endregion

    public Equipment[] currEquipment;

    public delegate void OnEquipmentChanged(Equipment newItem, Equipment oldItem);
    public OnEquipmentChanged onEquipmentChanged;

    Inventory inventory;

    public Equipment defaultMainEquipment;
    public Equipment defaultSideEquipment;

    void Start()
    {
        inventory = Inventory.instance;

        int numSlots = System.Enum.GetNames(typeof(Equipment.EquipmentSlot)).Length;
        currEquipment = new Equipment[numSlots];

        if (defaultMainEquipment != null)
            Equip(defaultMainEquipment);
        if (defaultSideEquipment != null)
            Equip(defaultSideEquipment);
    }

    public void Equip(Equipment newItem)
    {
        int slotIndex = (int)newItem.equipSlot;

        Equipment oldItem = null;

        if(currEquipment[slotIndex] != null)
        {
            oldItem = currEquipment[slotIndex];
            inventory.Add(oldItem);
        }

        currEquipment[slotIndex] = newItem;

        // Event call (Callback function)
        if (onEquipmentChanged != null)
            onEquipmentChanged.Invoke(newItem, oldItem);
    }

    public void Unequip(int slotIndex)
    {
        if(currEquipment[slotIndex] != null)
        {
            Equipment oldItem = currEquipment[slotIndex];
            inventory.Add(oldItem);

            currEquipment[slotIndex] = null;

            // Event call
            if (onEquipmentChanged != null)
                onEquipmentChanged.Invoke(null, oldItem);
        }
    }

    public void UnequipAll()
    {
        for(int i = 0; i < currEquipment.Length; ++i)
            Unequip(i);
    }

    void Update()
    {
        //if(Input.GetKeyDown(KeyCode.U))
        //    UnequipAll();
    }

    public Equipment mainEquipment()
    {
        if ((int)Equipment.EquipmentSlot.Main_Weapon < currEquipment.Length)
            return currEquipment[(int)Equipment.EquipmentSlot.Main_Weapon];
        else
            return null;
    }

    public void SwapMainEquipment()
    {
        Debug.Log("Swap Weapon");
        Equipment tempEquipment = currEquipment[(int)Equipment.EquipmentSlot.Main_Weapon];
        currEquipment[(int)Equipment.EquipmentSlot.Main_Weapon] = currEquipment[(int)Equipment.EquipmentSlot.Side_Weapon];
        currEquipment[(int)Equipment.EquipmentSlot.Side_Weapon] = tempEquipment;


        // Event call
        if (onEquipmentChanged != null)
            onEquipmentChanged.Invoke(null, null);
    }
}
