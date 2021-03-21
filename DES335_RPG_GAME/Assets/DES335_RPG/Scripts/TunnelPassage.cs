using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TunnelPassage : MonoBehaviour
{
    [SerializeField]
    public float fActiveTime;

    public bool bActive = false;

    bool intersect = false;

    int tunnelPassageID = 0;

    private TunnelManager tunnelManager;

    void Start()
    {
        tunnelManager = TunnelManager.instance;
    }

    // Update is called once per frame
    void Update()
    {
        if (bActive)
        {
            fActiveTime -= Time.deltaTime;

            // Destroy Tunnel
            if (fActiveTime < 0)
                Destroy(gameObject);
        }
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.CompareTag("Player") && col.GetComponent<PlayerSkill>().isDigging && tunnelManager.GetID() == tunnelPassageID && intersect)
        {
            Debug.Log("Hello: ");
            intersect = false;
        }
    }

    void OnTriggerExit2D(Collider2D col)
    {
        if (col.CompareTag("Player"))
        {
            intersect = true;
        }
    }

    public void SetTunnelPassageID(int id)
    {
        tunnelPassageID = id;
    }
}
