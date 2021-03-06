using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Inventory : MonoBehaviour
{
    [SerializeField]
    private int space = 0;

    
    public List<Item> items = new List<Item>();

    // Not going for efficiceny but for more safety & understandable to the reader
    public Dictionary<string, int> itemsCount = new Dictionary<string, int>();
    


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
    public Item testEquipment;
    //public Item testEquipment2;

    void Start()
    {
        if (testEquipment != null)
        {
            Add(testEquipment);

            //Event call
            if (onItemChangedCallback != null)
                onItemChangedCallback.Invoke();
        }

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
            return false;
        }
        else
        {
            if (itemsCount.ContainsKey(item.name))
            {
                if(!item.isEquipment)
                    ++itemsCount[item.name];
            }
            else
            {
                items.Add(item);
                itemsCount.Add(item.name, 1);
            }

            if(onItemChangedCallback != null)
                onItemChangedCallback.Invoke();

            return true;
        }
    }

    public void Remove(Item item)
    {
        items.Remove(item);

        if (itemsCount.ContainsKey(item.name))
        {
            if (--itemsCount[item.name] == 0)
                itemsCount.Remove(item.name);
        }

        if (onItemChangedCallback != null)
            onItemChangedCallback.Invoke();
    }
}
