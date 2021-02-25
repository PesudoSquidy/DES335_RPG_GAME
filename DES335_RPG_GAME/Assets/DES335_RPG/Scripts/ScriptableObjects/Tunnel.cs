using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tunnel : MonoBehaviour
{
    // Tunnel ID 
    private int tunnelID;


    private GameObject stuckedObject = null;

    private bool blocked = false;

    private float activetime;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        activetime -= 0.016f;

        if (activetime < 0)
            DestoryTunnel();
    }

    // Spawn Tunnel
    public void SpawnTunnel(int id, float time, Vector3 pos)
    {
        SetTunnelID(id);
        blocked = false;
        activetime = time;
        gameObject.GetComponent<Transform>().position = pos;
        gameObject.GetComponent<Renderer>().enabled = true;
    }

    // Destory Tunnel 
    public void DestoryTunnel()
    {
        gameObject.GetComponent<Renderer>().enabled = false;
    }

    // Set Tunnel ID
    void SetTunnelID(int id)
    {
        tunnelID = id;
    }

    // Collided Enemy
    void TrapEnemy(GameObject enemy)
    {
        stuckedObject = enemy;
        blocked = true;
    }
}
