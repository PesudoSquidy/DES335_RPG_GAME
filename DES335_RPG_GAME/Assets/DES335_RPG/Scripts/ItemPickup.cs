using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemPickup : MonoBehaviour
{
    [SerializeField] private Item item;


    private GameObject player;


    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
    }

    void OnCollisionEnter(Collision col)
    {
        if(col.gameObject == player)
        {
            Debug.Log("Player picked up: " + item.name);
            Destroy(gameObject);
        }
    }
}
