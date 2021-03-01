using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Destructibles : MonoBehaviour
{

    public int health;
    public GameObject destroyedVersion;
    
    public GameObject[] lootDrop;

    void OnTriggerEnter(Collider collider)
    {
        if (collider.gameObject.CompareTag("Weapon"))
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
        //Debug.Log("Drop LOOT!");

        // Spawn the destroyed version
        GameObject tempObj = Instantiate(destroyedVersion, transform.position, transform.rotation);
        //tempObj.GetComponent<Transform>().localScale = new Vector3(0.02f, 0.02f, 0.02f);

        // Remove the current object
        Destroy(gameObject);

        // Spawn items
        for(int i = 0; i < lootDrop.Length; ++i)
        {
            GameObject tempItem = Instantiate(lootDrop[i], transform.position, transform.rotation);

            if(tempItem.GetComponent<Rigidbody>() != null)
                tempItem.GetComponent<Rigidbody>().AddForceAtPosition(Vector3.up * 10f, transform.position, ForceMode.Impulse);
        }

        //Hide game object
        //gameObject.SetActive(false);    
    }
}
