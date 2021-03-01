using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CooldownUI : MonoBehaviour
{

    public Image imageCooldown;
    public float cooldown;
    bool isCooldown;

    [SerializeField]
    PlayerAttack playerAttack;

    [SerializeField]
    Item item;

    void Start()
    {
        imageCooldown.fillAmount = 0;
    }

    // Update is called once per frame
    void Update()
    {
        item = GetComponent<InventorySlot>().item;

        if(item != null)
        {
            if (item.name == "Bomb")
                cooldown = playerAttack.bombCooldown;
        }

        //if(Input.GetKeyDown(KeyCode.P))
        //{
        //    isCooldown = true;
        //}

        if (isCooldown)
        {
            imageCooldown.fillAmount -= 1 / cooldown * Time.deltaTime;
        }
    }

    public void Cooldown(float newCooldown)
    {
        cooldown = newCooldown;
    }
}
