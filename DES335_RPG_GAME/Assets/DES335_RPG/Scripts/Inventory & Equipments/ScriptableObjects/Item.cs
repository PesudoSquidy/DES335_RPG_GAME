﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;


// GUI in unity to create new items
[CreateAssetMenu(fileName = "New Item", menuName = "Inventory/Item")]


// Blueprint for items
public class Item : ScriptableObject
{
    new public string name = "New Item";
    public Sprite icon = null;
    public GameObject prefab = null;
    public bool isEquipment = false;
    public bool isAugment = false;
    public float coolDown;

    public virtual void Use()
    {
        // Use the item
        // Something might happen
        // Debug.Log("Using: " + name);
    }

    public void RemoveFromInventory()
    {
        Inventory.instance.Remove(this);
    }

    public void AddToInventory()
    {
        Inventory.instance.Add(this);
    }
}
