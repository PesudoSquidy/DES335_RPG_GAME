using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GeneralGameManager : MonoBehaviour
{

    public CombatRoomEnemySpawnManager combatRoomManager;
    public InventoryUI inventory_UI;

    public PlayerHealth playerHP;

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
        else if(Input.GetKeyDown(KeyCode.Alpha1))
        {
            if (combatRoomManager != null)
                combatRoomManager.SpawnEnemy(4, 0);
        }
        else if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            if (combatRoomManager != null)
                combatRoomManager.SpawnEnemy(4, 1);
        }
        else if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            if (combatRoomManager != null)
                combatRoomManager.SpawnEnemy(4, 2);
        }
        else if (Input.GetKeyDown(KeyCode.Alpha4))
        {
            if (combatRoomManager != null)
                combatRoomManager.SpawnEnemy(1, 3);
        }
        else if(Input.GetKeyDown(KeyCode.F))
        {
            playerHP.health = 6;
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
