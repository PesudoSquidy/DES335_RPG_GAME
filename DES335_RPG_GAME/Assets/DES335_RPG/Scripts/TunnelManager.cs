using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TunnelManager : MonoBehaviour
{

    [SerializeField] int maxPassages;

    private int IDs = 0;

    private GameObject player;

    [SerializeField] GameObject tunnel;
    [SerializeField] private float tunnelTime;
    GameObject[] inactiveTunnels;

    bool _currentflag = false;

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

                inactiveTunnels[IDs].GetComponent<Tunnel>().ActivateTunnel(inactiveTunnels[IDs+1].GetComponent<Transform>().position);
                inactiveTunnels[IDs+1].GetComponent<Tunnel>().ActivateTunnel(inactiveTunnels[IDs].GetComponent<Transform>().position);
                IDs += 2;
                if (IDs >= maxPassages * 2)
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
            inactiveTunnels[id].GetComponent<Tunnel>().SpawnTunnel(id, tunnelTime, player.GetComponent<Transform>().position);
    }

    void CreateEndTunnel(int id)
    {
        if(inactiveTunnels[id].GetComponent<Tunnel>() != null)
            inactiveTunnels[id+1].GetComponent<Tunnel>().SpawnTunnel(id, tunnelTime, player.GetComponent<Transform>().position);
    }
}
