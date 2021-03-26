using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GeneralGameManager : MonoBehaviour
{

    public CombatRoomEnemySpawnManager combatRoomManager;

    //public GameObject inventoryUI;

    // Update is called once per frame
    void Update()
    {
        // Respawn for combat testing
        if (Input.GetKeyDown(KeyCode.R))
        {
            if (combatRoomManager != null)
                combatRoomManager.SpawnEnemy(combatRoomManager.spawnAmount);
        }

        //// Turn on/off inventory 
        //if (Input.GetButtonDown("Inventory") && !CombatRoomEnemySpawnManager.combatInProgress)
        //{
        //    if (inventoryUI != null)
        //        inventoryUI.SetActive(!inventoryUI.activeSelf);
        //}
    }
}
