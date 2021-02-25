using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TunnelManager : MonoBehaviour
{

    [SerializeField] int maxTunnels;

    [SerializeField] int MaxDiggingMeter;

    [SerializeField] int DiggingMinimum;

    [SerializeField] int DiggingCost;

    [SerializeField] int DiggingDecrement;

    [SerializeField] int DiggingIncrement;

    private int DiggingMeter;

    private bool diggingFlag;

    private int IDs = 0;

    private GameObject player;

    [SerializeField] GameObject tunnel;

    GameObject[] inactiveTunnels;


    // Start is called before the first frame update
    void Start()
    {
        inactiveTunnels = new GameObject[maxTunnels];

        for (int i = 0; i < maxTunnels; ++i)
        {
            inactiveTunnels[i] = Instantiate(tunnel);
        }

        DiggingMeter = MaxDiggingMeter;
        diggingFlag = false;

        player = GameObject.FindGameObjectWithTag("Player");
    }

    // Update is called once per frame
    void Update()
    {
        // Check if Space bar is pressed
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Debug.Log("Space Pressed");
            // Check if player is digging 
            if (diggingFlag)
            {
                // Stop Digging

                diggingFlag = false;
            }
            else
            {
                // Check for minimum amount for digging
                if (DiggingMeter > DiggingMinimum)
                {
                    // Start Digging
                    diggingFlag = true;
                    DiggingMeter -= DiggingCost;
                    inactiveTunnels[0].GetComponent<Tunnel>().SpawnTunnel(IDs, 5.0f, player.GetComponent<Transform>().position);
                    Debug.Log("Create Tunnel");
                }
            }
        }

        // Decrease/Increase Digging Meter
        if (diggingFlag)
        {
            DiggingMeter -= DiggingDecrement;

            // Check if Digging Meter Runs Out
            if (DiggingMeter < 0)
                diggingFlag = false;
        }
        else
        {
            if (DiggingMeter < MaxDiggingMeter)
                DiggingMeter += DiggingIncrement;
            else
                DiggingMeter = MaxDiggingMeter;
        }
    }
}
