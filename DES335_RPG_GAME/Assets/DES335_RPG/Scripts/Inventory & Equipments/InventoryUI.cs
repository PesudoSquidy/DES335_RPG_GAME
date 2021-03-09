using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryUI : MonoBehaviour
{
    Inventory inventory;
    
    public Transform itemsParent;

    InventorySlot[] slots;

    public GameObject inventoryUI;

    void Awake()
    {
        inventory = Inventory.instance;
        inventory.onItemChangedCallback += UpdateUI;

        // Do in update if slots are non static
        slots = itemsParent.GetComponentsInChildren<InventorySlot>();
    }

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetButtonDown("Inventory"))
        {
            inventoryUI.SetActive(!inventoryUI.activeSelf);
        }
    }

    void UpdateUI()
    {
        //Debug.Log("Update Inventory UI");
        for(int i = 0; i < slots.Length; ++i)
        {
            if(i < inventory.items.Count)
            {
                if (inventory.items[i].isEquipment)
                    slots[i].AddItem(inventory.items[i]);
                else
                    slots[i].AddItem(inventory.items[i], inventory.itemsCount[inventory.items[i].name]);
            }
            else
            {
                slots[i].ClearSlot();
            }
        }
    }
}
