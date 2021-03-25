using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Heart_HUD_Manager : MonoBehaviour
{
    [SerializeField] private Player_Heart_HUD[] heart_UI;

    private PlayerHealth playerHealth;

    // Start is called before the first frame update
    void Start()
    {
        playerHealth = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerHealth>();
    }

    // Update is called once per frame
    void Update()
    {
        // Calculate which heart UI to update
        if (playerHealth.health >= 0)
        {
            // Figures out which heart_UI to update
            int heart_Index = (int)Mathf.Ceil((float)playerHealth.health / 2) - 1;
            int heart_Amount = 0;

            // There is no more health left
            if (heart_Index < 0)
                heart_Index = 0;

            // Figures out which texture to use for the heart_UI
            if ((playerHealth.health % 2) == 0)
            {
                if (playerHealth.health > 0)
                {
                    // Full health for the current heart_UI
                    heart_Amount = 2;

                    // Update the previous heart_UI if there is
                    if (heart_Index + 1 < heart_UI.Length)
                    {
                        // Empty heart
                        heart_UI[heart_Index + 1].UpdateHeartAmount(0);
                    }
                }
                else
                    heart_Amount = playerHealth.health % 2;
            }
            else
                heart_Amount = playerHealth.health % 2;

            // Update the heart UI amount
            heart_UI[heart_Index].UpdateHeartAmount(heart_Amount);
        }
    }
}
