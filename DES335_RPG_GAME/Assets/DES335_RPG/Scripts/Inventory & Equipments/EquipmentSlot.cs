

using UnityEngine;
using UnityEngine.UI;

public class EquipmentSlot : MonoBehaviour
{
    public Equipment equipment;

    [SerializeField] private Image equipmentIcon;
    [SerializeField] private Image augmentIcon;
    
    //private Inventory inventory;

    private void Awake()
    {
        //inventory = GameObject.Find("GameManager").GetComponent<Inventory>();
    }

    public void AddEquipment(Equipment newEquipment)
    {
        equipment = newEquipment;

        equipmentIcon.sprite = equipment.icon;
        equipmentIcon.enabled = true;

        if (equipment.augment != null)
        {
            augmentIcon.sprite = equipment.augment.icon;
            augmentIcon.enabled = true;
        }
    }

    public void ClearSlot()
    {
        equipment = null;

        equipmentIcon.sprite = null;
        equipmentIcon.enabled = false;

        augmentIcon.sprite = null;
        augmentIcon.enabled = false;
    }
}
