using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

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
            {
                PolygonTester test = PolygonTester.instance;
                if(test.iID == tunnelPassageID)
                    PolygonTester.instance.DestoryMesh();
                Destroy(gameObject);
            }
        }
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.CompareTag("Player") && col.GetComponent<PlayerSkill>().isDigging && tunnelManager.GetID() == tunnelPassageID && intersect)
        {
            PolygonTester test = PolygonTester.instance;
            Queue<GameObject> queue = tunnelManager.GetQueue();

            var list = queue.ToList();

            while(list.Count() > 0)
            {
                if (gameObject.GetComponent<Transform>().position == list[0].GetComponent<Transform>().position)
                    break;
                list.RemoveAt(0);
            }

            // Create Vector2 vertices
            Vector2[] vertices2D = new Vector2[list.Count()];

            for (int i = 0; i < list.Count(); ++i)
            {
                vertices2D[i] = list[i].GetComponent<Transform>().position;
            }

            test.GenerateMesh(vertices2D, tunnelPassageID);
            //Debug.Log("Generate Mesh: ");
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
