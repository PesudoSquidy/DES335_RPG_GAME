using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LootDrop : MonoBehaviour
{
    [SerializeField]
    private List<GameObject> lootDrop;

    [SerializeField] [Range(0.0f, 10.0f)]
    private int dropRate;

    [SerializeField] [Range(0, 10)]
    private int dropAmount;

    public void DropLoot()
    {
        if (dropAmount <= 0)
            return;
        else
        {
            for(int i = 0; i < dropAmount; ++i)
            {
                int randNo = Random.Range(0, dropRate);
                float randPosX = Random.Range(0.0f, 0.5f) + gameObject.transform.position.x;
                float randPosY = Random.Range(0.0f, 0.5f) + gameObject.transform.position.y;

                Vector3 randPos = new Vector3(randPosX, randPosY, gameObject.transform.position.z);

                if(randNo > lootDrop.Count)
                    randNo %= lootDrop.Count;

                Instantiate(lootDrop[randNo], randPos, gameObject.transform.rotation);
            }
        }
    }
}
