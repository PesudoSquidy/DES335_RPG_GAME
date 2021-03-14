using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GeneralGameManager : MonoBehaviour
{

    public CombatRoomEnemySpawnManager combatRoomManager;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            if (combatRoomManager != null)
                combatRoomManager.SpawnEnemy();
        }
    }
}
