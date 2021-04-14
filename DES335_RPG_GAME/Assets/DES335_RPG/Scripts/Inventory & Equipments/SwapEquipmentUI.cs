using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.UI;

public class SwapEquipmentUI : MonoBehaviour
{

    private EquipmentManager eqManager;
    private Inventory inventory;

    public Equipment tempEQ;
    public Augment tempAugment;

    void Start()
    {
        eqManager = EquipmentManager.instance;
        inventory = Inventory.instance;
    }

    public void SwapMainEquipment()
    {
        // Swap Equipment
        if (eqManager.currEquipment[(int)Equipment.EquipmentSlot.Main_Weapon] != null)
        {
            if (tempEQ != null)
            {
                eqManager.EquipMainEquipment(tempEQ);
                //inventory.Remove(tempEQ);
            }
        }
    }

    public void SwapSideEquipment()
    {
        // Swap Equipment
        if (eqManager.currEquipment[(int)Equipment.EquipmentSlot.Side_Weapon] != null)
        {
            if (tempEQ != null)
            {
                eqManager.EquipSideEquipment(tempEQ);
                //inventory.Remove(tempEQ);
            }
        }
    }

    public void SwapMainAugment()
    {
        // Swap Equipment
        if (tempAugment != null)
        {
            eqManager.EquipMainAugment(tempAugment);
            //inventory.Remove(tempEQ);
        }
    }

    public void SwapSideAugment()
    {
        // Swap Equipment
        if (tempAugment != null)
        {
            eqManager.EquipSideAugment(tempAugment);
            //inventory.Remove(tempEQ);
        }
    }

    public void SetUI_Off(GameObject UI)
    {
        UI.SetActive(false);
    }
}
