using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Destructibles : MonoBehaviour
{

    public int health;

    void OnTriggerEnter(Collider collider)
    {
        if (collider.gameObject.tag == "Weapon")
        {
            --health;

            if (health <= 0)
                DropLoot();
        }


        Debug.Log(collider.gameObject.name);
        Debug.Log(collider.gameObject.tag);
    }

    void DropLoot()
    {
        Debug.Log("Drop LOOT!");

        //Hide game object
        gameObject.SetActive(false);    
    }
}
