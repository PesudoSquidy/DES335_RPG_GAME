using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleManager : MonoBehaviour
{
    [SerializeField]
    GameObject[] obstaclesObj;

    PlayerSkill playerSkill;

    bool isTrigger;

    // Start is called before the first frame update
    void Start()
    {
        obstaclesObj = GameObject.FindGameObjectsWithTag("Obstacle");

        playerSkill = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerSkill>();

        isTrigger = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (playerSkill != null && playerSkill.isDigging)
            SetCollidersTrigger(true);
        else
            SetCollidersTrigger(false);
    }


    void SetCollidersTrigger(bool newTrigger)
    {
        if (isTrigger == newTrigger)
            return;
        else
        {
            isTrigger = newTrigger;

            //Set all obstacle object to isTrigger
            for (int i = 0; i < obstaclesObj.Length; ++i)
                obstaclesObj[i].GetComponent<BoxCollider2D>().isTrigger = isTrigger;
        }
    }
}
