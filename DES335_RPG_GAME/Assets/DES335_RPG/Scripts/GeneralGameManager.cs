using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GeneralGameManager : MonoBehaviour
{

    public CombatRoomEnemySpawnManager combatRoomManager;

    public InventoryUI inventory_UI;

    // Game Status for other obj to use
    public bool ui_active;

    void Start()
    {
        ui_active = false;
    }


    // Update is called once per frame
    void Update()
    {
        // Respawn for combat testing
        if (Input.GetKeyDown(KeyCode.R))
        {
            if (combatRoomManager != null)
                combatRoomManager.SpawnEnemy(combatRoomManager.spawnAmount);
        }

        // Update inventory status
        ui_active = inventory_UI.ui_active;


        //// Turn on/off inventory 
        //if (Input.GetButtonDown("Inventory") && !CombatRoomEnemySpawnManager.combatInProgress)
        //{
        //    if (inventoryUI != null)
        //        inventoryUI.SetActive(!inventoryUI.activeSelf);
        //}
    }
}
