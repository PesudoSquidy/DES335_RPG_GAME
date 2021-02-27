using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tunnel : MonoBehaviour
{
    private GameObject stuckedObject = null;

    private bool bBlocked = false;

    [SerializeField]
    private float fActiveTime;

    public bool bActive = false;

    // Tunnel ID 
    public int tunnelID;

    private Vector3 otherEnd;

    private BoxCollider2D col2D;

    // Start is called before the first frame update
    void Start()
    {
        if(gameObject.GetComponent<BoxCollider2D>() !=null)
        {
            col2D = gameObject.GetComponent<BoxCollider2D>();
            col2D.enabled = false;
        }
    }

    // Update is called once per frame
    void Update() 
    {
        if (bActive)
        {
            fActiveTime -= 0.016f;

            // Destroy Tunnel
            if (fActiveTime < 0)
                DeactiveTunnel();

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

    // Activate Tunnel
    public void ActivateTunnel(Vector3 tunnelPos)
    {
        bActive = true;
        otherEnd = tunnelPos;
        gameObject.GetComponent<BoxCollider2D>().enabled = true;
    }


    // Deactive Tunnel 
    public void DeactiveTunnel()
    {
        gameObject.GetComponent<Renderer>().enabled = false;
        gameObject.GetComponent<BoxCollider2D>().enabled = false;
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
        if (obj.GetComponent<Transportable>() != null && obj.GetComponent<Transportable>().objTransported == 0)
        {
            obj.GetComponent<Transform>().position = otherEnd;
            FinishTransport(obj);
        }
    }

    void PrepareTransport(GameObject obj)
    {
        if (obj.GetComponent<Transportable>() == null)
            obj.AddComponent<Transportable>();
    }

    void FinishTransport(GameObject obj)
    {
        ++obj.GetComponent<Transportable>().objTransported;
        ++obj.GetComponent<Transportable>().objTransported;
    }

    void ResetTransport(GameObject obj)
    {
        if (obj.GetComponent<Transportable>() !=null && obj.GetComponent<Transportable>().objTransported > 0)
            --obj.GetComponent<Transportable>().objTransported;
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.tag == "Player")
        {
            PrepareTransport(col.gameObject);
            Transport(col.gameObject); 
        }
        else if(col.tag == "Bomb")
        {
            PrepareTransport(col.gameObject);
            Transport(col.gameObject);
        }
    }

    void OnTriggerExit2D(Collider2D col)
    {
        if (col.tag == "Player")
        {
            ResetTransport(col.gameObject);
        }
    }
}
