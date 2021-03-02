using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CombatRoomEnemySpawnManager : MonoBehaviour
{
    [SerializeField] private int enemyAmount;
    [SerializeField] private GameObject[] enemyPrefabs;

    [SerializeField] private GameObject[] spawnPoints;

    [SerializeField] private GameObject LockRoom;

    GameObject[] enemies;

    // Start is called before the first frame update
    void Start()
    {
        if (enemyPrefabs.Length > 0)
        {
            enemies = new GameObject[enemyAmount];
            for (int i = 0; i < enemyAmount; ++i)
            {
                int randNo = Random.Range(0, enemyPrefabs.Length);
                int randSP = Random.Range(0, spawnPoints.Length);

                enemies[i] = Instantiate(enemyPrefabs[randNo], spawnPoints[randSP].transform.position, Quaternion.identity);
            }
        }
    }

    void Update()
    {
        for (int i = 0; i < enemyAmount; ++i)
        {
            if (enemies[i] != null)
                return;
        }

        LockRoom.SetActive(false);
    }
}
