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

    private Vector3 otherEnd;

    // Start is called before the first frame update
    void Start()
    {
        gameObject.AddComponent<BoxCollider2D>();
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
        gameObject.SetActive(true);
        tunnelID = id;
        bBlocked = false;
        fActiveTime = time;
        bActive = false;
        gameObject.GetComponent<Transform>().position = pos;
        gameObject.GetComponent<Renderer>().enabled = true;
    }

    // Activate Tunnel
    public void ActivateTunnel(Vector3 tunnelPos)
    {
        bActive = true;
        otherEnd = tunnelPos;
    }


    // Destory Tunnel 
    public void DestoryTunnel()
    {
        gameObject.GetComponent<Renderer>().enabled = false;
        bActive = false;
        bBlocked = false;
    }

    // Collided Enemy
    void TrapEnemy(GameObject enemy)
    {
        stuckedObject = enemy;
        bBlocked = true;
    }

    // Transport Object
    void Transport(GameObject obj)
    {
        obj.GetComponent<Transform>().position = otherEnd;
    }
}
