using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// GUI in unity to create new items
[CreateAssetMenu(fileName = "New Equipment", menuName = "Inventory/Equipment")]


public class Equipment : ScriptableObject
{
    new public string name = "New Equipment";
    public Sprite icon = null;

    public GameObject prefab = null;
}
