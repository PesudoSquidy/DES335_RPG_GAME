using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Heart_UI_Manager : MonoBehaviour
{
    [SerializeField] private Heart_UI[] heart_UI;

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
        int heart_Index = (playerHealth.health / 2) - 1;
        int heart_Amount = playerHealth.health % 3;

        // Update the heart UI amount
        heart_UI[heart_Index].UpdateHeartAmount(heart_Amount);
    }
}
