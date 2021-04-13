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

    public delegate void OnEquipmentChanged(Equipment newItem = null, Equipment oldItem = null);
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

        // Searches if there is an empty space in equipment slot
        for (int i = 0; i < System.Enum.GetNames(typeof(Equipment.EquipmentSlot)).Length; ++i)
        {
            // Find the next null equipment slot
            if (currEquipment[i] == null)
            {
                currEquipment[i] = newItem;
                
                // Event call (Callback function)
                if (onEquipmentChanged != null)
                    onEquipmentChanged.Invoke(newItem, oldItem);

                return;
            }
        }

        // There is no empty slot.
        if (currEquipment[slotIndex] != null)
        {
            oldItem = currEquipment[slotIndex];
            inventory.Add(oldItem);
        }

        currEquipment[slotIndex] = newItem;

        // Event call (Callback function)
        if (onEquipmentChanged != null)
            onEquipmentChanged.Invoke(newItem, oldItem);
    }

    public void EquipMainEquipment(Equipment newItem)
    {
        Equipment oldItem = null;

        // Searches if there is an empty space in equipment slot
        for (int i = 0; i < System.Enum.GetNames(typeof(Equipment.EquipmentSlot)).Length; ++i)
        {
            // Find the next null equipment slot
            if (currEquipment[i] == null)
            {
                currEquipment[i] = newItem;

                // Event call (Callback function)
                if (onEquipmentChanged != null)
                    onEquipmentChanged.Invoke(newItem, oldItem);

                return;
            }
        }

        // There is no empty slot.
        if (currEquipment[(int)Equipment.EquipmentSlot.Main_Weapon] != null)
        {
            oldItem = currEquipment[(int)Equipment.EquipmentSlot.Main_Weapon];
            inventory.Add(oldItem);
        }

        currEquipment[(int)Equipment.EquipmentSlot.Main_Weapon] = newItem;

        // Event call (Callback function)
        if (onEquipmentChanged != null)
            onEquipmentChanged.Invoke(newItem, oldItem);
    }

    public void EquipSideEquipment(Equipment newItem)
    {
        Equipment oldItem = null;

        // Searches if there is an empty space in equipment slot
        for (int i = 0; i < System.Enum.GetNames(typeof(Equipment.EquipmentSlot)).Length; ++i)
        {
            // Find the next null equipment slot
            if (currEquipment[i] == null)
            {
                currEquipment[i] = newItem;

                // Event call (Callback function)
                if (onEquipmentChanged != null)
                    onEquipmentChanged.Invoke(newItem, oldItem);

                return;
            }
        }

        // There is no empty slot.
        if (currEquipment[(int)Equipment.EquipmentSlot.Side_Weapon] != null)
        {
            oldItem = currEquipment[(int)Equipment.EquipmentSlot.Side_Weapon];
            inventory.Add(oldItem);
        }

        currEquipment[(int)Equipment.EquipmentSlot.Side_Weapon] = newItem;

        // Event call (Callback function)
        if (onEquipmentChanged != null)
            onEquipmentChanged.Invoke(newItem, oldItem);
    }

    public void EquipMainAugment(Augment newItem)
    {
        Augment oldItem = null;

        // There is no empty slot.
        if (currEquipment[(int)Equipment.EquipmentSlot.Main_Weapon].augment != null)
        {
            oldItem = currEquipment[(int)Equipment.EquipmentSlot.Main_Weapon].augment;
            inventory.Add(oldItem);
        }

        currEquipment[(int)Equipment.EquipmentSlot.Main_Weapon].augment = newItem;

        // Event call (Callback function)
        if (onEquipmentChanged != null)
            onEquipmentChanged.Invoke();
    }

    public void EquipSideAugment(Augment newItem)
    {
        Augment oldItem = null;

        // There is no empty slot.
        if (currEquipment[(int)Equipment.EquipmentSlot.Side_Weapon].augment != null)
        {
            oldItem = currEquipment[(int)Equipment.EquipmentSlot.Side_Weapon].augment;
            inventory.Add(oldItem);
        }

        currEquipment[(int)Equipment.EquipmentSlot.Side_Weapon].augment = newItem;

        // Event call (Callback function)
        if (onEquipmentChanged != null)
            onEquipmentChanged.Invoke();
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

    public Equipment MainEquipment()
    {
        //if ((int)Equipment.EquipmentSlot.Main_Weapon < currEquipment.Length)
        return currEquipment[(int)Equipment.EquipmentSlot.Main_Weapon];
    }

    public Equipment SideEquipment()
    {
        return currEquipment[(int)Equipment.EquipmentSlot.Side_Weapon];
    }

    public void SwapMainEquipment()
    {
        Equipment tempEquipment = currEquipment[(int)Equipment.EquipmentSlot.Main_Weapon];
        currEquipment[(int)Equipment.EquipmentSlot.Main_Weapon] = currEquipment[(int)Equipment.EquipmentSlot.Side_Weapon];
        currEquipment[(int)Equipment.EquipmentSlot.Side_Weapon] = tempEquipment;


        // Event call
        if (onEquipmentChanged != null)
            onEquipmentChanged.Invoke(null, null);
    }
}
