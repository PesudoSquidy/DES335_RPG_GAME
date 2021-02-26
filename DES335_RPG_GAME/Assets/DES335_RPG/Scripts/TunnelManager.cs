using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TunnelManager : MonoBehaviour
{

    [SerializeField] int maxTunnels;

    private int IDs = 0;

    private GameObject player;

    [SerializeField] GameObject tunnel;

    GameObject[] inactiveTunnels;

    bool _currentflag = false;

    // Start is called before the first frame update
    void Start()
    {
        inactiveTunnels = new GameObject[maxTunnels];

        for (int i = 0; i < maxTunnels; ++i)
            inactiveTunnels[i] = Instantiate(tunnel);

        player = GameObject.FindGameObjectWithTag("Player");
    }

    // Update is called once per frame
    void Update()
    {
        // Check if Space bar is pressed
        if (_currentflag != StaminaBar._stamina.bStaminaDrain)
        {
            if (_currentflag)
            {
                CreateEndTunnel(IDs);
                for (int i = 0; i < maxTunnels; ++i)
                    if (inactiveTunnels[i].GetComponent<Tunnel>().tunnelID == IDs)
                        inactiveTunnels[i].GetComponent<Tunnel>().bActive = true;
                ++IDs;
                if (IDs >= maxTunnels/2)
                    IDs = 0;
            }
            else
                CreateStartTunnel(IDs);

            _currentflag = StaminaBar._stamina.bStaminaDrain;
        }
    }

    void CreateStartTunnel(int id)
    {
        if(inactiveTunnels[id].GetComponent<Tunnel>() != null)
            inactiveTunnels[id].GetComponent<Tunnel>().SpawnTunnel(id, 10, player.GetComponent<Transform>().position);
    }

    void CreateEndTunnel(int id)
    {
        if(inactiveTunnels[id].GetComponent<Tunnel>() != null)
            inactiveTunnels[id+1].GetComponent<Tunnel>().SpawnTunnel(id, 10, player.GetComponent<Transform>().position);
    }
}
