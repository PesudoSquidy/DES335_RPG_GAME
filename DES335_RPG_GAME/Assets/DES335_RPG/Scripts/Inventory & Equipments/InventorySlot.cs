
using UnityEngine;
using UnityEngine.UI;

using UnityEngine.EventSystems;

public class InventorySlot : MonoBehaviour, IDropHandler
{

    public Item item;
    
    [SerializeField] private Image icon;

    [SerializeField] private Button removeButton;

    [SerializeField] private Text itemCountText;

    [SerializeField] private Button itemButton;

    private GameObject swapEquipmentUI;
    private GameObject swapAugmentUI;

    private Inventory inventory;

    private void Awake()
    {
        inventory = GameObject.Find("GameManager").GetComponent<Inventory>();

        swapEquipmentUI = GameObject.Find("SwapEquipmentUI");
        swapAugmentUI = GameObject.Find("SwapAugmentUI");
    }

    public void ShowSwapEquipmentUI()
    {
        if(item is Equipment)
        {
            swapEquipmentUI.SetActive(true);
            swapEquipmentUI.transform.position = Input.mousePosition;
            swapEquipmentUI.GetComponent<SwapEquipmentUI>().tempEQ = (Equipment)item;
        }
        else
        {
            swapEquipmentUI.GetComponent<SwapEquipmentUI>().tempEQ = null;
            swapEquipmentUI.SetActive(false);
        }

        if(item is Augment)
        {
            swapAugmentUI.SetActive(true);
            swapAugmentUI.transform.position = Input.mousePosition;
            swapAugmentUI.GetComponent<SwapEquipmentUI>().tempAugment = (Augment)item;
        }
        else
        {
            swapAugmentUI.GetComponent<SwapEquipmentUI>().tempAugment = null;
            swapAugmentUI.SetActive(false);
        }
    }

    public void AddItem(Item newItem, int newItemCount)
    {
        item = newItem;

        icon.sprite = item.icon;
        icon.enabled = true;

        removeButton.interactable = true;

        if (itemButton != null)
            itemButton.interactable = true;

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

        if(itemButton != null)
            itemButton.interactable = true;

        if (itemCountText != null)
            itemCountText.enabled = false;
    }

    public void ClearSlot()
    {
        item = null;

        icon.sprite = null;
        icon.enabled = false;

        removeButton.interactable = false;

        if(itemButton != null)
            itemButton.interactable = false;

        if (itemCountText != null)
            itemCountText.enabled = false;
    }

    public void OnDrop(PointerEventData eventData)
    {
        //Debug.Log("On Drop: " + gameObject.name);

        if(eventData.pointerDrag != null)
        {
            //eventData.pointerDrag.GetComponent<RectTransform>().anchoredPosition = GetComponent<RectTransform>().anchoredPosition;

            // Reset position
            eventData.pointerDrag.GetComponent<RectTransform>().anchoredPosition = eventData.pointerDrag.GetComponent<DragDrop>().startPos;

            // Swap item
            Item newItem = eventData.pointerDrag.transform.GetComponentInParent<InventorySlot>().item;

            if(newItem != null && item != null)
                inventory.SwapItem(newItem, item);
        }

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
