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
    public Item testEquipment2;
    public Item testEquipment3;

    public Item nullItem;

    void Start()
    {
        if (testEquipment != null)
            Add(testEquipment);

        if (testEquipment2 != null)
            Add(testEquipment2);

        if (testEquipment3 != null)
            Add(testEquipment3);

        // Fill inventory with null item
        //for(int i = 0; i < space; ++i)
        //{
        //    if (i > items.Count)
        //        Add(nullItem);
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
        if (itemsCount.ContainsKey(item.name))
        {
            if (--itemsCount[item.name] == 0)
            {
                items.Remove(item);
                itemsCount.Remove(item.name);
            }
        }

        if (onItemChangedCallback != null)
            onItemChangedCallback.Invoke();
    }

    public void SwapItem(Item newItem, Item oldItem)
    {
        int newItemPos = 0;
        int oldItemPos = 0;

        Item tempItem = null;

        // Find the position of the item
        for(int i = 0; i < items.Count; ++i)
        {
            if (items[i].name == newItem.name)
                newItemPos = i;
            else if (items[i].name == oldItem.name)
                oldItemPos = i;
        }

        tempItem = items[newItemPos];
        items[newItemPos] = items[oldItemPos];
        items[oldItemPos] = tempItem;

        if (onItemChangedCallback != null)
            onItemChangedCallback.Invoke();
    }
}
