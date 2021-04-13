using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EquipmentManagerUI : MonoBehaviour
{
    
    EquipmentManager equipmentManager;

    public Transform equipmentParent;
    //InventorySlot[] slots;

    EquipmentSlot[] slots;

    public GameObject equipmentUI;

    #region Singleton

    public static EquipmentManagerUI instance;

    void Awake()
    {
        instance = this;

        equipmentManager = EquipmentManager.instance;

        // Add new callback fn
        equipmentManager.onEquipmentChanged += UpdateEquipmentUI;

        //slots = equipmentParent.GetComponentsInChildren<InventorySlot>();
        slots = equipmentParent.GetComponentsInChildren<EquipmentSlot>();
    }

    #endregion

    // Start is called before the first frame update
    void Start()
    {
      
    }

    void Update()
    {
        //if (Input.GetButtonDown("SwapEquipment"))
        //{
        //    //equipmentUI.SetActive(!equipmentUI.activeSelf);

        //    // Swap Equipment
        //    if (equipmentManager.currEquipment[(int)Equipment.EquipmentSlot.Main_Weapon] != null && equipmentManager.currEquipment[(int)Equipment.EquipmentSlot.Side_Weapon] != null)
        //    {
        //        equipmentManager.SwapMainEquipment();
        //    }
        //}
    }

    public void UpdateEquipmentUI(Equipment newItem = null, Equipment oldItem = null)
    {
        for (int i = 0; i < slots.Length; ++i)
        {
            if (i < equipmentManager.currEquipment.Length)
            {
                if (equipmentManager.currEquipment[i] != null)
                    //slots[i].AddItem((Item)equipmentManager.currEquipment[i]);
                    slots[i].AddEquipment(equipmentManager.currEquipment[i]);
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
