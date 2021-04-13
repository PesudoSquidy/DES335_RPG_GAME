using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//using UnityEngine.UI;
//using UnityEngine.EventSystems;

public class InventoryUI : MonoBehaviour
{
    Inventory inventory;
    
    public Transform itemsParent;

    InventorySlot[] slots;

    public GameObject inventoryUI;

    public bool ui_active;

    private GameObject swapEquipmentUI;
    private GameObject swapAugmentUI;

    void Awake()
    {
        inventory = Inventory.instance;
        inventory.onItemChangedCallback += UpdateUI;

        // Do in update if slots are non static
        slots = itemsParent.GetComponentsInChildren<InventorySlot>();

        ui_active = false;

        swapEquipmentUI = GameObject.Find("SwapEquipmentUI");
        swapAugmentUI = GameObject.Find("SwapAugmentUI");
    }

    // Start is called before the first frame update
    void Start()
    {
        swapEquipmentUI.GetComponent<SwapEquipmentUI>().tempEQ = null;
        swapEquipmentUI.SetActive(false);

        swapAugmentUI.GetComponent<SwapEquipmentUI>().tempAugment = null;
        swapAugmentUI.SetActive(false);

        inventoryUI.SetActive(false);
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

        // Right mouse click
        if(Input.GetMouseButtonDown(1))
        {
            swapEquipmentUI.GetComponent<SwapEquipmentUI>().tempEQ = null;
            swapEquipmentUI.SetActive(false);

            swapAugmentUI.GetComponent<SwapEquipmentUI>().tempAugment = null;
            swapAugmentUI.SetActive(false);
        }
    }

    void UpdateUI()
    {
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
