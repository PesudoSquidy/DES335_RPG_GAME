using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TunnelManager : MonoBehaviour
{

    #region Singleton

    public static TunnelManager instance;

    void Awake()
    {
        instance = this;
    }

    #endregion

    [SerializeField] int maxPassages;

    private int IDs = 0;

    private GameObject player;

    [SerializeField] GameObject tunnel;
    [SerializeField] private float tunnelTime;
    GameObject[] inactiveTunnels;

    bool _currentflag = false;

    [SerializeField] GameObject tunnel_Passage;

    Queue<GameObject> tunnelPassageHandler;
    int[] tunnelPassageID;

    GameObject tunnelPassageEnd;

    [SerializeField] PlayerSkill playerSkill;

    // Start is called before the first frame update
    void Start()
    {
        inactiveTunnels = new GameObject[maxPassages * 2];

        for (int i = 0; i < maxPassages * 2; ++i)
        {
            inactiveTunnels[i] = Instantiate(tunnel);
            inactiveTunnels[i].GetComponent<Tunnel>().DeactiveTunnel();
        }

        player = GameObject.FindGameObjectWithTag("Player");
        playerSkill = player.GetComponent<PlayerSkill>();


        tunnelPassageHandler = new Queue<GameObject>();
        tunnelPassageID = new int[maxPassages * 2];
    }

    // Update is called once per frame
    void Update()
    {
        // Check if Space bar is pressed
        if (_currentflag !=  playerSkill.isDigging)//StaminaBar._stamina.bStaminaDrain)
        {
            if (_currentflag)
            {
                if (playerSkill.isUnderObject == false)
                {
                    CreateEndTunnel(IDs);

                    inactiveTunnels[IDs].GetComponent<Tunnel>().ActivateTunnel(inactiveTunnels[IDs + 1]);
                    inactiveTunnels[IDs + 1].GetComponent<Tunnel>().ActivateTunnel(inactiveTunnels[IDs]);
                }
                else
                    inactiveTunnels[IDs].GetComponent<Tunnel>().bActive = true;

                // CancelInvoke("SpawnTunnelPassage");

                    // Activate all tunnel passage
                for (int i = 0; i < tunnelPassageID[IDs]; ++i)
                {
                    TunnelPassage tempScript = tunnelPassageHandler.Dequeue().GetComponent<TunnelPassage>();
                    tempScript.fActiveTime = inactiveTunnels[IDs].GetComponent<Tunnel>().fActiveTime + (i * 0.08f);
                    tempScript.bActive = true;
                }

                inactiveTunnels[IDs + 1].GetComponent<Tunnel>().fActiveTime += tunnelPassageID[IDs] * 0.08f;
                tunnelPassageID[IDs] = 0;

                IDs += 2;
                if (IDs >= maxPassages * 2)
                    IDs = 0;
            }
            else
            {
                CreateStartTunnel(IDs);
                // InvokeRepeating("SpawnTunnelPassage", 0.1f, 0.1f);
            }

            //_currentflag = StaminaBar._stamina.bStaminaDrain;
            _currentflag = playerSkill.isDigging;
        }
        for (int i = 0; i < maxPassages * 2; i += 2)
        {
            if (inactiveTunnels[i].GetComponent<Tunnel>().bActive)
            {
                if (inactiveTunnels[i].GetComponent<Tunnel>().bBlocked || inactiveTunnels[i + 1].GetComponent<Tunnel>().bBlocked)
                {
                    inactiveTunnels[i].GetComponent<Tunnel>().bBlocked = true;
                    inactiveTunnels[i+1].GetComponent<Tunnel>().bBlocked = true;
                }
            }
        }

        if (_currentflag)
        {
            if (Vector3.Distance(player.GetComponent<Transform>().position, tunnelPassageEnd.GetComponent<Transform>().position) > 1)
                SpawnTunnelPassage();
        }


    }

    void SpawnTunnelPassage()
    {
        if (tunnel_Passage != null)
        {
            tunnelPassageEnd = Instantiate(tunnel_Passage, player.transform.position, Quaternion.identity);
            tunnelPassageHandler.Enqueue(tunnelPassageEnd);
            ++(tunnelPassageID[IDs]);
        }
    }
    
    void CreateStartTunnel(int id)
    {
        if(inactiveTunnels[id].GetComponent<Tunnel>() != null)
            inactiveTunnels[id].GetComponent<Tunnel>().SpawnTunnel(id, tunnelTime, player.GetComponent<Transform>().position);

        tunnelPassageEnd = inactiveTunnels[id];
    }

    void CreateEndTunnel(int id)
    {
        if(inactiveTunnels[id].GetComponent<Tunnel>() != null)
            inactiveTunnels[id+1].GetComponent<Tunnel>().SpawnTunnel(id, tunnelTime, player.GetComponent<Transform>().position);
    }
}
