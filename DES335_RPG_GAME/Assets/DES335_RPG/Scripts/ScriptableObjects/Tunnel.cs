using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tunnel : MonoBehaviour
{
    // Tunnel ID 
    private int tunnelID;

    public Sprite tunnelImage = null;

    private GameObject stuckedObject = null;

    private bool blocked = false;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Initialise Tunnels
    public void Initailise(Sprite image)
    {
        tunnelImage = image;
    }

    // Update is called once per frame
    void Update()
    {

    }

    // Spawn Tunnel
    public void SpawnTunnel(int id)
    {
        SetTunnelID(id);
        blocked = false;
    }

    // Destory Tunnel 
    public void DestoryTunnel()
    {
        
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
