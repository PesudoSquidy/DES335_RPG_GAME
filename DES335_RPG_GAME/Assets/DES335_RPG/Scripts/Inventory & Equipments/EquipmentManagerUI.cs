using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EquipmentManagerUI : MonoBehaviour
{
    
    EquipmentManager equipmentManager;

    public Transform equipmentParent;
    InventorySlot[] slots;

    public GameObject equipmentUI;

    void Awake()
    {
        equipmentManager = EquipmentManager.instance;

        // Add new callback fn
        equipmentManager.onEquipmentChanged += UpdateEquipmentUI;

        slots = equipmentParent.GetComponentsInChildren<InventorySlot>();
    }

    // Start is called before the first frame update
    void Start()
    {

    }

    void Update()
    { 
        if(Input.GetButtonDown("SwapEquipment"))
        {
            //equipmentUI.SetActive(!equipmentUI.activeSelf);

            // Swap Equipment
            if(equipmentManager.currEquipment[(int)Equipment.EquipmentSlot.Main_Weapon] != null  && equipmentManager.currEquipment[(int)Equipment.EquipmentSlot.Side_Weapon] != null)
            {
                equipmentManager.SwapMainEquipment();
            }
        }
    }

    void UpdateEquipmentUI(Equipment newItem, Equipment oldItem)
    {
        for (int i = 0; i < slots.Length; ++i)
        {
            if (i < equipmentManager.currEquipment.Length)
            {
                if (equipmentManager.currEquipment[i] != null)
                    slots[i].AddItem((Item)equipmentManager.currEquipment[i]);
                else
                    slots[i].ClearSlot();
            }
            else
            {
                slots[i].ClearSlot();
            }
        }
    }
}
