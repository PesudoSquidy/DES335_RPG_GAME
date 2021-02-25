using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tunnel : MonoBehaviour
{
    // Tunnel ID 
    private int tunnelID;

    private Sprite tunnelImage = null;

    private GameObject stuckedObject = null;

    private bool blocked = false;

    private float activetime;

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
        activetime -= 1/60;

        if (activetime < 0) 
            gameObject.GetComponent<Renderer>().enabled = false;
    }

    // Spawn Tunnel
    public void SpawnTunnel(int id, float time, Vector3 pos)
    {
        SetTunnelID(id);
        blocked = false;
        activetime = time;
        gameObject.GetComponent<Transform>().position = pos;
        gameObject.GetComponent<Renderer>().enabled = false;
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
