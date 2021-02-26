using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tunnel : MonoBehaviour
{
    private GameObject stuckedObject = null;

    private bool bBlocked = false;

    private float fActiveTime;

    public bool bActive = false;

    // Tunnel ID 
    public int tunnelID;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (bActive)
        {
            fActiveTime -= 0.016f;

            // Destroy Tunnel
            if (fActiveTime < 0)
                DestoryTunnel();

            // Lock Enemy
            if (bBlocked)
                stuckedObject.GetComponent<Transform>().position = gameObject.GetComponent<Transform>().position;
        }
    }

    // Spawn Tunnel
    public void SpawnTunnel(int id, float time, Vector3 pos)
    {
        tunnelID = id;
        bBlocked = false;
        fActiveTime = time;
        bActive = false;
        gameObject.GetComponent<Transform>().position = pos;
        gameObject.GetComponent<Renderer>().enabled = true;
    }

    // Destory Tunnel 
    public void DestoryTunnel()
    {
        gameObject.GetComponent<Renderer>().enabled = false;
    }

    // Collided Enemy
    void TrapEnemy(GameObject enemy)
    {
        stuckedObject = enemy;
        bBlocked = true;
    }
}
