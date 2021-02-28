using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TunnelPassage : MonoBehaviour
{
    [SerializeField]
    public float fActiveTime;

    public bool bActive = false;

    // Update is called once per frame
    void Update()
    {
        if (bActive)
        {
            fActiveTime -= Time.deltaTime;

            // Destroy Tunnel
            if (fActiveTime < 0)
                Destroy(gameObject);
        }
    }

}
