using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_Heart_HUD_Manager : MonoBehaviour
{
    [SerializeField] private Enemy_Heart_HUD[] heart_HUD;

    public EnemyHealth enemyHealth;

    private int prevEnemyHealth;

    [SerializeField] private float HUD_Timer;
    private float countdownTimer;

    // Parent Gameobject
    [SerializeField] public Transform parentObj;

    // Start is called before the first frame update
    void Start()
    {
        countdownTimer = 0.0f;

        if (enemyHealth != null)
            prevEnemyHealth = enemyHealth.health;
        
        parentObj = transform.parent.transform;
    }

    // Update is called once per frame
    void Update()
    {
        // Prevent flipping of images
        if (parentObj != null)
            transform.localScale = new Vector3(parentObj.localScale.x / Mathf.Abs(parentObj.localScale.x), parentObj.localScale.y / Mathf.Abs(parentObj.localScale.y), parentObj.localScale.z / Mathf.Abs(parentObj.localScale.z));

        // Timer is place here to start countdown from here
        if (countdownTimer > 0)
        {
            countdownTimer -= Time.deltaTime;

            // Set the heart_HUD active
            for (int i = 0; i < transform.childCount; ++i)
                transform.GetChild(i).gameObject.SetActive(true);
        }
        // Check to show the  - Only happens when there is no Timer running;
        else
        {
            // Set the heart_HUD active
           for(int i = 0; i < transform.childCount; ++i)
                transform.GetChild(i).gameObject.SetActive(false);
        }

        if (prevEnemyHealth != enemyHealth.health)
        {
            // Update the prevEnemyHealth
            prevEnemyHealth = enemyHealth.health;

            // Start the countdown timer to turn it off 
            countdownTimer = HUD_Timer;

            // Calculate which heart UI to update
            if (enemyHealth.health >= 0)
            {
                // Figures out which heart_HUD to update
                int heart_Index = (int)Mathf.Ceil((float)enemyHealth.health / 2) - 1;
                int heart_Amount = 0;

                // There is no more health left
                if (heart_Index < 0)
                    heart_Index = 0;

                // Figures out which texture to use for the heart_HUD
                if ((enemyHealth.health % 2) == 0)
                {
                    if (enemyHealth.health > 0)
                    {
                        // Full health for the current heart_HUD    
                        heart_Amount = 2;

                        // Update the previous heart_HUD if there is
                        if (heart_Index + 1 < heart_HUD.Length)
                        {
                            // Empty heart
                            heart_HUD[heart_Index + 1].UpdateHeartAmount(0);
                        }
                    }
                    else
                        heart_Amount = enemyHealth.health % 2;
                }
                else
                    heart_Amount = enemyHealth.health % 2;

                // Update the heart UI amount
                if (heart_Index < heart_HUD.Length)
                    heart_HUD[heart_Index].UpdateHeartAmount(heart_Amount);
            }
        }
    } 
}
