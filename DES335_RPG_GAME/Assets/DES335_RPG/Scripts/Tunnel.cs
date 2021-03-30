using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tunnel : MonoBehaviour
{
    private GameObject stuckedObject = null;

    public bool bBlocked = false;

    [SerializeField]
    public float fActiveTime;

    public bool bActive = false;

    // Tunnel ID 
    public int tunnelID;

    public GameObject otherEnd;

    private BoxCollider2D col2D;

    // Start is called before the first frame update
    void Start()
    {
        if(gameObject.GetComponent<BoxCollider2D>() !=null)
        {
            col2D = gameObject.GetComponent<BoxCollider2D>();
            col2D.enabled = false;
        }

        otherEnd = null;
    }

    // Update is called once per frame
    void Update() 
    {
        if (bActive)
        {
            fActiveTime -= Time.deltaTime;

            // Destroy Tunnel
            if (fActiveTime < 0)
                DeactiveTunnel();

            // Lock Enemy
            if (bBlocked && stuckedObject != null)
                stuckedObject.GetComponent<Transform>().position = gameObject.GetComponent<Transform>().position;

            if (otherEnd != null && otherEnd.GetComponent<Tunnel>() != null)
                if (otherEnd.GetComponent<Tunnel>().bActive == false)
                    otherEnd = null;
        }
    }

    // Spawn Tunnel
    public void SpawnTunnel(int id, float time, Vector3 pos)
    {
        tunnelID = id;
        bBlocked = false;
        fActiveTime = time;
        bActive = false;
        stuckedObject = null;
        gameObject.GetComponent<Transform>().position = pos;
        gameObject.GetComponent<Renderer>().enabled = true;
        gameObject.GetComponent<BoxCollider2D>().enabled = true;
    }

    // Activate Tunnel
    public void ActivateTunnel(GameObject theOtherTunnel)
    {
        bActive = true;
        otherEnd = theOtherTunnel;
    }


    // Deactive Tunnel 
    public void DeactiveTunnel()
    {
        gameObject.GetComponent<Renderer>().enabled = false;
        gameObject.GetComponent<BoxCollider2D>().enabled = false;
        bActive = false;
        bBlocked = false;
        otherEnd = null;

        if (stuckedObject != null)
            stuckedObject.GetComponent<Enemy_AI>().canMove = true;
    }

    // Collided Enemy
    void TrapEnemy(GameObject enemy)
    {
        stuckedObject = enemy;

        if (enemy != null)
        {
            if (enemy.GetComponent<Enemy_AI>() != null)
                enemy.GetComponent<Enemy_AI>().canMove = false;
        }

        bBlocked = true;
    }

    // Transport Object
    //public void Transport(GameObject obj)
    //{
    //    if (obj.GetComponent<Transportable>() != null && obj.GetComponent<Transportable>().objTransported == 0)
    //    {
    //        if(otherEnd != null && otherEnd.GetComponent<Tunnel>().bActive)
    //            obj.GetComponent<Transform>().position = otherEnd.transform.position;

    //        FinishTransport(obj);
    //    }
    //}

    public void Transport_2(GameObject obj)
    {
        Debug.Log("Teleport player to: " + otherEnd.transform.position);
        obj.GetComponent<Transform>().position = otherEnd.transform.position;

        //if (obj.GetComponent<Transportable>() != null)
        //{

        //    if (otherEnd != null)
        //    {
        //        Debug.Log("Teleport player");
        //        obj.GetComponent<Transform>().position = otherEnd.transform.position;
        //    }
        //}
    }

    public void PrepareTransport(GameObject obj)
    {
        if (obj.GetComponent<Transportable>() == null)
            obj.AddComponent<Transportable>();
    }

    //void FinishTransport(GameObject obj)
    //{
    //    ++obj.GetComponent<Transportable>().objTransported;
    //    ++obj.GetComponent<Transportable>().objTransported;
    //}

    //public void ResetTransport(GameObject obj)
    //{
    //    if (obj.GetComponent<Transportable>() !=null && obj.GetComponent<Transportable>().objTransported > 0)
    //        --obj.GetComponent<Transportable>().objTransported;
    //}

    void OnTriggerEnter2D(Collider2D col)
    {
        if (!bBlocked)
        {
            if (col.CompareTag("Player"))
            {
                //PrepareTransport(col.gameObject);
                //Transport(col.gameObject);
            }
            else if (col.CompareTag("Bomb"))
            {
                PrepareTransport(col.gameObject);
                //Transport(col.gameObject);
            }
        }

        if (!stuckedObject)
        {
            if (col.CompareTag("Enemy"))
                TrapEnemy(col.gameObject);
        }
    }

    //void OnTriggerExit2D(Collider2D col)
    //{
    //    if (col.CompareTag("Player"))
    //    {
    //        ResetTransport(col.gameObject);
    //    }
    //}
}
