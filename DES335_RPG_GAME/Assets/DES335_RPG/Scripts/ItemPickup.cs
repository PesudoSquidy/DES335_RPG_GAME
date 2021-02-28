using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemPickup : MonoBehaviour
{
    [SerializeField] private Item item;

    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.tag == "Player")
        {
            //Debug.Log("Player picked up: " + item.name);
            if (Inventory.instance.Add(item))
                Destroy(gameObject);
        }
    }
}
