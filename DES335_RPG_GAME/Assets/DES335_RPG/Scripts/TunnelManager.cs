using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TunnelManager : MonoBehaviour
{

    Tunnel[] unactiveTunnels;

    Tunnel[] activeTunnels;

    public int maxTunnels;

    public Sprite TunnelImage;

    [SerializeField] int MaxDiggingMeter;

    private int DiggingMeter;

    [SerializeField] int DiggingMinimum;

    [SerializeField] int DiggingCost;

    [SerializeField] int DiggingDecrement;

    [SerializeField] int DiggingIncrement;

    private bool diggingFlag;

    private int IDs = 0;

    // Start is called before the first frame update
    void Start()
    {
        for (int i = 0; i < maxTunnels; ++i)
            unactiveTunnels[i].Initailise(TunnelImage);

        DiggingMeter = MaxDiggingMeter;
    }

    // Update is called once per frame
    void Update()
    {
        // Check if Space bar is pressed
        if (Input.GetKeyDown(KeyCode.Space))
        {
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
