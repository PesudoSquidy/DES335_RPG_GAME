using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryUI : MonoBehaviour
{
    Inventory inventory;
    
    public Transform itemsParent;

    InventorySlot[] slots;

    public GameObject inventoryUI;

    public bool ui_active;

    void Awake()
    {
        inventory = Inventory.instance;
        inventory.onItemChangedCallback += UpdateUI;

        // Do in update if slots are non static
        slots = itemsParent.GetComponentsInChildren<InventorySlot>();

        ui_active = false;
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
            if (inventoryUI != null && !CombatRoomEnemySpawnManager.combatInProgress)
            {
                ui_active = !ui_active;
                inventoryUI.SetActive(!inventoryUI.activeSelf);
            }
        }

        if (inventoryUI != null && CombatRoomEnemySpawnManager.combatInProgress)
        {
            ui_active = false;
            inventoryUI.SetActive(false);
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
