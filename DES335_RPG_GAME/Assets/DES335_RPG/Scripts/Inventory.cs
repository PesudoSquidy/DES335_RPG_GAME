using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Inventory : MonoBehaviour
{
    [SerializeField]
    private int space = 0;

    [SerializeField]
    public List<Item> items = new List<Item>();

    public delegate void OnItemChanged();
    public OnItemChanged onItemChangedCallback;

    #region Singleton

    public static Inventory instance;
    void Awake()
    {
        if(instance != null)
        {
            Debug.LogWarning("More than one instance of Inventory found!");
            return;
        }

        instance = this;
    }
    #endregion

    //Testing Purpose
    //public Item testEquipment;
    //public Item testEquipment2;

    void Start()
    {
        //if (testEquipment != null)
        //{
        //    items.Add(testEquipment);

        //    //Event call
        //    if (onItemChangedCallback != null)
        //        onItemChangedCallback.Invoke();
        //}

        //if (testEquipment2 != null)
        //{
        //    items.Add(testEquipment2);

        //    //Event call
        //    if (onItemChangedCallback != null)
        //        onItemChangedCallback.Invoke();
        //}
    }


    public bool Add(Item item)
    {
        if (items.Count >= space)
        {
            Debug.Log("Inventory not enough space");
            return false;
        }
        else
        {
            items.Add(item);

            if(onItemChangedCallback != null)
                onItemChangedCallback.Invoke();

            return true;
        }
    }

    public void Remove(Item item)
    {
        items.Remove(item);

        if (onItemChangedCallback != null)
            onItemChangedCallback.Invoke();
    }
}
