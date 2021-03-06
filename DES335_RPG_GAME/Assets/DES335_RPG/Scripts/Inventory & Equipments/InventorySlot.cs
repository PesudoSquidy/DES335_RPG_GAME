
using UnityEngine;
using UnityEngine.UI;

public class InventorySlot : MonoBehaviour
{

    public Item item;
    
    [SerializeField]
    private Image icon;

    [SerializeField]
    private Button removeButton;

    [SerializeField]
    private Text itemCountText;

    public void AddItem(Item newItem, int newItemCount)
    {
        item = newItem;

        icon.sprite = item.icon;
        icon.enabled = true;
        removeButton.interactable = true;

        if (itemCountText != null)
        {
            itemCountText.enabled = true;
            itemCountText.text = newItemCount.ToString();
        }
    }

    public void AddItem(Item newItem)
    {
        item = newItem;

        icon.sprite = item.icon;
        icon.enabled = true;
        removeButton.interactable = true;

        if (itemCountText != null)
            itemCountText.enabled = false;
    }

    public void ClearSlot()
    {
        item = null;

        icon.sprite = null;
        icon.enabled = false;

        removeButton.interactable = false;

        if(itemCountText != null)
            itemCountText.enabled = false;
    }

    public void OnRemoveButton()
    {
        Inventory.instance.Remove(item);
    }

    public void UseItem()
    {
        if(item != null)
        {
            item.Use();
        }
    }
}
