using System.Collections;
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
}
