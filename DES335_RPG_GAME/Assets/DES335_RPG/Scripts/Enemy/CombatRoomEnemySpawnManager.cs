using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CombatRoomEnemySpawnManager : MonoBehaviour
{
    [SerializeField] private int enemyAmount;
    [SerializeField] private GameObject[] enemyPrefabs;

    [SerializeField] private GameObject[] spawnPoints;

    [SerializeField] private GameObject LockRoom;

    [SerializeField] List<GameObject> enemies;

    [HideInInspector] static public bool combatInProgress = false;
    
    [SerializeField] public int spawnAmount;

    // Start is called before the first frame update
    void Start()
    {
        //SpawnEnemy(enemyAmount);
    }

    void Update()
    {
        // Update the list
        for(int i = 0; i < enemies.Count;)
        {
            if (enemies[i] == null)
                enemies.RemoveAt(i);
            else
                ++i;
        }

        if (enemies.Count <= 0)
        {
            if (LockRoom != null)
                LockRoom.SetActive(false);

            combatInProgress = false;
        }
        else
        {
            if (LockRoom != null)
                LockRoom.SetActive(true);

            combatInProgress = true;
        }
    }

    public void SpawnEnemy(int spawnAmount, int spawnType = -1)
    {
         if (enemyPrefabs.Length > 0)
        {
            for (int i = 0; i < enemyAmount; ++i)
            {
                int randNo = spawnType;
                int randSP = Random.Range(0, spawnPoints.Length);

                if(randNo < 0)
                    Random.Range(0, enemyPrefabs.Length);
                

                enemies.Add(Instantiate(enemyPrefabs[randNo], spawnPoints[randSP].transform.position, Quaternion.identity));
            }
        }
    }

}
